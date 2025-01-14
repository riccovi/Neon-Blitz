using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;

    public Bullet bullet;
    Vector2 direction;

    public bool autoShoot = false;
    public float shootInterval = 0.5f; // Shooting every x seconds
    public float shootDelay = 0f; // Wait in seconds before it starts shooting
    float shootTimer = 0f;
    float delayTimer = 0f;

    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) // Only runs further code if gun isActive
        {
            return;
        }

        direction = (transform.localRotation*Vector2.up).normalized;

        if (autoShoot)
        {
            if (delayTimer >= shootDelay)
            {
                if (shootTimer >= shootInterval)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity); // go = gameobject
        // Checks direction of gun to shoot bullet in right directoin
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }
}
