using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject enemyExplosion;
    
    bool canBeDestroyed = false;
    public int scoreVal = 50;
    public int health = 1;
    public float topBorder = 11f;

    public bool isBoss = false;
    public int explosionCount = 10;
    public float explosionArea = 5f;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Level.instance.AddEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        // Once on screen, allow enemy to be destroyed
        if (transform.position.y < topBorder && !canBeDestroyed ) 
        {
            canBeDestroyed = true;
            // Guns only shoot when on screen
            Gun[] guns = transform.GetComponentsInChildren<Gun>(); 
            foreach (Gun gun in guns)
            {
                gun.isActive = true;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed){ return;} //Don't destroy enemy if off screen
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                TakeDamage(1); // Reduce health by 1 when hit by bullet
                if (health > 0){ // If enemy is not dead, play enemy hit sound, otherwise it will be enemy death sound
                    audioManager.PlaySFX(audioManager.enemyHit); 
                }
                Destroy(bullet.gameObject); // Remove bullet
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Level.instance.AddScore(scoreVal); // Add score
        Destroy(gameObject); // Destroy enemy
        if (isBoss)
        {
            TriggerBossExplosion();
            TriggerScreenShake();
        }
        else{
            Instantiate(enemyExplosion, transform.position, Quaternion.identity); // Single explosion animation
        }
        audioManager.PlaySFX(audioManager.enemyDeath); // Play audio
    }

    private void TriggerBossExplosion()
    {
        // Spawn multiple explosions at random positions around the boss
        for (int i = 0; i < explosionCount; i++)
        {
            Vector2 randomPos = (Vector2)transform.position + new Vector2(Random.Range(-explosionArea, explosionArea), Random.Range(-explosionArea, explosionArea));
            Instantiate(enemyExplosion, randomPos, Quaternion.identity);
        }
    }

    private void TriggerScreenShake()
    {
        ScreenShake screenShake = Camera.main.GetComponent<ScreenShake>();
        if (screenShake != null){screenShake.Shake(0.5f,1f);} //intensity, duration
    }

    private void OnDestroy()
    {
       Level.instance.RemoveEnemy();
    }
}
