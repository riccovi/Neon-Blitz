using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Vector3 originalPosition;
    private float shakeMagnitude = 0.3f; // Intensity of shake
    private float shakeDuration = 0.2f; // Duration of shake

    private void Start()
    {
        originalPosition = transform.position; // Save camera's initial position 
    }

    public void Shake(float magnitude, float duration)
    {
        shakeMagnitude = magnitude;
        shakeDuration = duration;
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f; // Tracking elapsed time

        while (elapsed < shakeDuration)
        {
            // Generating random offsets to the camera position
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
            
            // Increment elapsed time
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition; // Reset camera position when shake is done
    }
}
