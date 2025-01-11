using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFall : MonoBehaviour
{
    public float fallSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.y -= fallSpeed*Time.fixedDeltaTime; // Move downwards
        if (pos.y < -1) {Destroy(gameObject);} // If off screen, destroy object
        transform.position = pos;
    }
}
