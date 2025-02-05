using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioHandler : MonoBehaviour
{
    private void Start()
    {
        Camera.main.aspect = 4f / 5f; // Set to 4:5 ratio
     }
}
