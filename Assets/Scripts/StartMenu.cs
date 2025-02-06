using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class StartMenu : MonoBehaviour
{
    public AudioSource startSound;
    public GameObject upgradesPanel;
    public GameObject pressText;
    private int counter = 0;

    public Camera mainCamera;
    public float zoomedInSize = 4f;
    public float zoomSpeed = 2f;
    private bool isZooming = false;

    void Start()
    {
        Camera.main.aspect = 4f / 5f; // Set to 4:5 ratio
    }

    void Update()
    {
        // First X press - open panel
        if (Input.GetKeyDown(KeyCode.X)) // GetKeyDown, so that sound doesn't spam repeat when X is pressed
        {
            if (counter == 0){
                upgradesPanel.SetActive(true); //Enable upgrades panel
                pressText.SetActive(false); // Disable 'press x' text
                counter++;
            }
            else if (!isZooming){ // If the camera is not already zooming, call the function
                startSound.Play();
                StartCoroutine(ZoomIn());
                // if (startSound != null){
                //     startSound.Play();  // Play start sound
                //     StartCoroutine(LoadLevelAfterSound()); // Load scene after the sound finishes
                // }    
            }
        }
    }

    // IEnumerator LoadLevelAfterSound()
    // {
    //     yield return new WaitForSeconds(startSound.clip.length); // Wait for sound to finish
    //     SceneManager.LoadSceneAsync("Level1"); // Load scene after delay
    // }

    IEnumerator ZoomIn(){
        isZooming = true;
        float startSize = mainCamera.orthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < 1f) // Runs for 1 second
        {
            mainCamera.orthographicSize = Mathf.Lerp(startSize, zoomedInSize, elapsedTime);
            elapsedTime += Time.deltaTime * zoomSpeed;
            yield return null;
        }
    }
}
