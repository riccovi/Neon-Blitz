using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    
    float speed = 4; // 4 pixels per frame
    bool left;
    bool right;
    bool up;
    bool down;

    Gun[] guns; // Array of guns (powerups = more guns)
    bool shoot;
    float fireCooldown = 0.5f; // Time in seconds between bullets
    float nextFireTime = 0f; // Tracks when next bullet can be fired

    GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        shield = transform.Find("Shield").gameObject;
        //DeactivateShield(); // Ship does not have shield upon startup 
        guns = transform.GetComponentsInChildren<Gun>(); 
        foreach(Gun gun in guns)
        {
            gun.isActive = true;
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
            foreach(Gun gun in guns)
            {
                gun.Shoot(); // Shoot through every current gun
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

    // Shield
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

    //Collision 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.isEnemy)
        {
            if (HasShield()) 
            {
                DeactivateShield(); 
            }
            else  
            {
                Destroy(gameObject); 
            }
            Destroy(bullet.gameObject);
            return;
        }
        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
            if (bullet.isEnemy)
            {
                if (HasShield()) 
                {
                    DeactivateShield(); 
                }
                else  
                {
                    Destroy(gameObject); 
                }
                Destroy(destructable.gameObject); 
            }
    }
}
