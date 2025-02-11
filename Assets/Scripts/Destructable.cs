using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject enemyExplosion;
    
    bool canBeDestroyed = false;
    public int scoreVal = 50;
    public int health = 1;

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
        if (transform.position.y < 11f && !canBeDestroyed ) 
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
        Instantiate(enemyExplosion, transform.position, Quaternion.identity); // Explosion animation
        audioManager.PlaySFX(audioManager.enemyDeath); // Play audio
    }

    private void OnDestroy()
    {
       Level.instance.RemoveEnemy();
    }
}
