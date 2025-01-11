using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(0,1); // Move bullet in y axis
    public float speed = 7;
    public Vector2 velocity;

    public bool isEnemy = true;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4); // Bullet disappears after x seconds
        //DontDestroyOnLoad(gameObject); // Bullets stay when new level transitions
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction*speed;
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos += velocity*Time.fixedDeltaTime;
        transform.position = pos;
    }
}
