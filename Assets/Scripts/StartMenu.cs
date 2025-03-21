using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class StartMenu : MonoBehaviour
{
    public AudioSource startSound;
    public GameObject upgradesPanel;
    public GameObject creditsPanel;
    public GameObject pressText;
    private int counter = 0;

    //Camera Zoom In
    private float zoom;
    private float zoomMultipler = 4f;
    private float minZoom = 40f;
    private float maxZoom = 160f;
    private float velocity = 0f;
    private float smoothTime = 0.7f;
    private bool isZooming = false;
    [SerializeField] private Camera cam;

    //Achievements
    public GameObject achievementsPanel;
    public GameObject achievement1;
    public GameObject achievement2;
    public GameObject achievement3;

    void Start()
    {
       Camera.main.aspect = 4f / 5f; // Set to 4:5 ratio
       zoom = cam.orthographicSize;
    }

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.X)) // GetKeyDown, so that sound doesn't spam repeat when X is pressed
        {
            // First X press - open panel
            if (counter == 0)
            {
                upgradesPanel.SetActive(true); //Enable upgrades panel
                creditsPanel.SetActive(false);
                achievementsPanel.SetActive(false);
                pressText.SetActive(false); // Disable 'press x' text
                counter++;
            // Second X press - zoom in
            } else if (!isZooming)
            {
                if (startSound != null){startSound.Play();} // Play start sound 
                upgradesPanel.SetActive(false); // Disable upgrades panel
                creditsPanel.SetActive(false);
                achievementsPanel.SetActive(false);
                isZooming = true;
            }    
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            counter = 0;
            upgradesPanel.SetActive(false);
            achievementsPanel.SetActive(false);
            creditsPanel.SetActive(!creditsPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            counter = 0;
            upgradesPanel.SetActive(false);
            creditsPanel.SetActive(false);
            achievementsPanel.SetActive(!achievementsPanel.activeSelf);
            if (PlayerPrefs.GetInt("Achievement1", 0) == 1)
            {
                achievement1.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Achievement2", 0) == 1)
            {
                achievement2.SetActive(true);
            }  
            if (PlayerPrefs.GetInt("Achievement3", 0) == 1)
            {
                achievement3.SetActive(true);
            }        
        }


        if (isZooming) 
        {
                zoom -= zoomMultipler; 
                zoom = Mathf.Clamp(zoom, minZoom, maxZoom); //Ensures zoom stays within min and max limits
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime); // Smoothly transition camera's zoom level
                cam.transform.position += new Vector3(0,0.14f,0); // Moving the camera slightly upwards
                if (cam.orthographicSize < minZoom+10){SceneManager.LoadSceneAsync("Level1");} // When zoomed in, load level 1
        }
    }

}






