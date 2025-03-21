using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progressBarPlayer : MonoBehaviour
{
    public float pixelSpeed = 2f; // Pixels per second
    private float stopY = 7f;
    private float unitSpeed;

    void Start()
    {
        float pixelsPerUnit = (2f * Camera.main.orthographicSize) / Screen.height;
        unitSpeed = pixelSpeed * pixelsPerUnit;
    }

    void Update()
    {
        if (transform.position.y < stopY)
        {
            transform.position += new Vector3(0, unitSpeed * Time.deltaTime, 0);
        }

    }
}
