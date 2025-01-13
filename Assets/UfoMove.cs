using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoMove : MonoBehaviour
{
    float sinCenterX;
    public float fallSpeed = 1f; 
    public float amplitude = 1f; // width of wave
    public float frequency = 3f; // horizontal speed of wave
    public bool inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        sinCenterX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.y -= fallSpeed*Time.fixedDeltaTime; // Move downwards
        if (pos.y < -1) {Destroy(gameObject);} // If off screen, destroy
        // Create sin pattern of movement
        float sin = Mathf.Sin(pos.y*frequency) * amplitude;
        if(inverted){sin *= -1;}
        pos.x = sinCenterX + sin ;

        transform.position = pos;
    }
}
