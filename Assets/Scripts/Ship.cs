using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    
    float speed = 3; // 3 pixels per frame
    bool left;
    bool right;
    bool up;
    bool down;

    Gun[] guns; // Array of guns (powerups = more guns)
    bool shoot;
    float fireCooldown = 0.5f; // Time in seconds between bullets
    float nextFireTime = 0f; // Tracks when next bullet can be fired

    GameObject shield;
    int powerUpGunLevel = 0;

    AudioManager audioManager;
    public Level level;

    public SpriteRenderer spriteRenderer;
    public Sprite gun1Sprite;  
    public Sprite gun2Sprite;
    public Sprite gun3Sprite; 
    public Sprite gun4Sprite; 

    public ParticleSystem speedTrail;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        speedTrail.Stop();

        shield = transform.Find("Shield").gameObject;
        DeactivateShield(); // Ship does not have shield upon startup 
        guns = transform.GetComponentsInChildren<Gun>(); 
        foreach(Gun gun in guns)
        {
            gun.isActive = true;
            if (gun.powerUpLevelRequirement != 0)
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard input - no alternate keys
        left = Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.RightArrow);
        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);
        shoot = Input.GetKey(KeyCode.X);
        
        // Preventing rapid fire
        if (shoot && Time.time >= nextFireTime)
        {
            shoot = false;
            foreach(Gun gun in guns)
            {
                if (gun.gameObject.activeSelf)
                {
                    gun.Shoot(); // Shoot through every current gun
                }
            }
        nextFireTime = Time.time + fireCooldown;
        } 
    }

    private void FixedUpdate()
    {
        //Movement
        Vector2 pos = transform.position; 
        float amount = speed*Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;
        // No else statements - allowing ship to move in 8 directions
        if(left){move.x -= amount;}
        if(right){move.x += amount;}
        if(up){move.y += amount;}
        if(down){move.y -= amount;}

        // Preventing diagonal movement being faster
        float magnitude = Mathf.Sqrt(move.x*move.x + move.y*move.y);
        if (magnitude>amount){
            float ratio = amount/magnitude;
            move *= ratio;
        }
        //Debug.Log(Magnitude);

        pos += move;
        // Movement boundaries
        if (pos.x <= 0.5f){pos.x = 0.5f;} // Don't go any further then these coordinates
        if (pos.x >= 7.5f){pos.x = 7.5f;}
        if (pos.y >= 9.5f){pos.y = 9.5f;}
        if (pos.y <= 0.5f){pos.y = 0.5f;}

        transform.position = pos;
    }

    // Shield PowerUp
    void ActivateShield()
    {
        shield.SetActive(true);
    }
    void DeactivateShield()
    {
        shield.SetActive(false);
    }
    bool HasShield()
    {
        return shield.activeSelf; // Check if shield is active
    }

    // Gun PowerUp
    void AddGuns()
    {   
        if (powerUpGunLevel < 3) // Ensures max gun level is 3
        {
            powerUpGunLevel++;
            foreach(Gun gun in guns)
            {
                if (gun.powerUpLevelRequirement == powerUpGunLevel)
                {
                    gun.gameObject.SetActive(true);
                }
            }
            UpdateGunSprite();
        }
    }
    void LoseGuns()
    {
        if (powerUpGunLevel > 0) // Ensures you always have the base gun level (level 0)
        { 
            powerUpGunLevel--;
            foreach(Gun gun in guns)
            {
                if (gun.powerUpLevelRequirement == powerUpGunLevel+1)
                {
                    gun.gameObject.SetActive(false);
                }
            }
            UpdateGunSprite();
        }
    }

    //Change Ship sprite based on gun level
    private void UpdateGunSprite()
    {
        switch (powerUpGunLevel)
        {
            case 0:
                spriteRenderer.sprite = gun1Sprite;
                break;
            case 1:
                spriteRenderer.sprite = gun2Sprite;
                break;
            case 2:
                spriteRenderer.sprite = gun3Sprite;
                break;
            case 3:
                spriteRenderer.sprite = gun4Sprite;
                break;
        }
    }

    // Speed PowerUp
    void AddSpeed()
    {
        if (speed < 5){  // Limit max speed to 5
            speed += 2f;
            speedTrail.Play();
        }
    }
    void LoseSpeed()
    {
        if (speed > 3){ // Limit min speed to 3 (default speed)
            speed -= 2f;
            speedTrail.Stop();
        }
    }

    // Destroy All On Screen PowerUp
    void DestroyAllOnScreen()
    {
        Destructable[] destructables = FindObjectsOfType<Destructable>(); // Find all enemies
        foreach (Destructable destructable in destructables)
        {
            if(destructable.transform.position.y < 11f) // If enemies are on screen
            {
                Instantiate(destructable.enemyExplosion, destructable.transform.position, Quaternion.identity); //Explosion animation for each destructable
                Destroy(destructable.gameObject); // Destroy Enemy
                //Level.instance.AddScore(destructable.scoreVal); // Add their score
            }
        }
        Bullet[] bullets = FindObjectsOfType<Bullet>(); // Find all bullets
        foreach (Bullet bullet in bullets)
        {
            Destroy(bullet.gameObject); // Destroy all bullets
        } // Don't have to check if bullet is on screen, as enemies only start shooting once on screen
    }

    void GameOver()
    {   
        //Audio
        audioManager.StopMusic();
        audioManager.PlaySFX(audioManager.shipDeath);
        // Destroy all
        DestroyAllOnScreen();
        // Including PowerUps
        PowerUp[] powerUps = FindObjectsOfType<PowerUp>();
        foreach (PowerUp powerUp in powerUps){Destroy(powerUp.gameObject);}
        // Including Explosions
        enemyExplosion[] explosions = FindObjectsOfType<enemyExplosion>();
        foreach (enemyExplosion explosion in explosions){Destroy(explosion.gameObject);}
        // Level
        level.HighScoreUpdate();    
        Level.instance.TriggerGameOver();
        Destroy(gameObject); 
    }

    //Collision 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Bullet collision
        Bullet bullet = collision.GetComponent<Bullet>(); 
        if (bullet != null && bullet.isEnemy)
        {
            if (HasShield()) 
            {
                DeactivateShield(); 
            }
            else  
            {
                GameOver();
            }
            Destroy(bullet.gameObject);
        }
        // Enemy collision
        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)// && bullet.isEnemy)
        {
            if (HasShield()) 
            {
                DeactivateShield(); 
            }
            else  
            {
                GameOver();
            }
            Destroy(destructable.gameObject); 
        }
        // PowerUp collision
        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp)
        {
            if (powerUp.activateShield)
            {
                ActivateShield();
                audioManager.PlaySFX(audioManager.powerup1);
            }
            if (powerUp.addGuns)
            {
                AddGuns();
                audioManager.PlaySFX(audioManager.powerup4);
            }
            if (powerUp.loseGuns)
            {
                LoseGuns();
                audioManager.PlaySFX(audioManager.powerdown);
            }
            if (powerUp.addSpeed)
            {
                AddSpeed();
                audioManager.PlaySFX(audioManager.powerup2);
            }
            if (powerUp.loseSpeed)
            {
                LoseSpeed();
                audioManager.PlaySFX(audioManager.powerdown);
            }
            if (powerUp.destroyAllOnScreen)
            {
                DestroyAllOnScreen();
                audioManager.PlaySFX(audioManager.powerup3);

                // Trigger screen shake
                ScreenShake screenShake = Camera.main.GetComponent<ScreenShake>();
                if (screenShake != null){screenShake.Shake(0.3f,0.2f);} //intensity, duration
            }
            if (powerUp.powerPoints)
            {
                audioManager.PlaySFX(audioManager.powerup5);
            }
            Level.instance.AddScore(powerUp.scoreVal);
            Destroy(powerUp.gameObject);
        }
    }
}
