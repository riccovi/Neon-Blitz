using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    bool canBeDestroyed = false;
    public int scoreVal = 50;

    // Start is called before the first frame update
    void Start()
    {
        Level.instance.AddEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 11f && !canBeDestroyed ) // Once on screen, allow enemy to be destroyed
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
                Level.instance.AddScore(scoreVal);
                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
       Level.instance.RemoveEnemy();
    }
}
