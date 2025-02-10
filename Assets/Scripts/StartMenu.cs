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

    //Camera Zoom In
    private float zoom;
    private float zoomMultipler = 4f;
    private float minZoom = 40f;
    private float maxZoom = 160f;
    private float velocity = 0f;
    private float smoothTime = 1f;
    private bool isZooming = false;
    [SerializeField] private Camera cam;

    void Start()
    {
       // Camera.main.aspect = 4f / 5f; // Set to 4:5 ratio
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
                pressText.SetActive(false); // Disable 'press x' text
                counter++;
            // Second X press - zoom in
            } else if (!isZooming)
            {
                if (startSound != null){startSound.Play();} // Play start sound 
                upgradesPanel.SetActive(false);
                isZooming = true;
                //StartCoroutine(ZoomAndLoad());
            }    
        }

        if (isZooming)
        {
                zoom -= zoomMultipler;
                zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
                cam.transform.position += new Vector3(0,0.15f,0);
                if (cam.orthographicSize < minZoom+10){SceneManager.LoadSceneAsync("Level1");}
        }
    }

    IEnumerator ZoomAndLoad()
    {
        isZooming = true;
        zoom -= zoomMultipler;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);

        float elapsedTime = 0f;
        float startZoom = cam.orthographicSize;

        while (elapsedTime < smoothTime)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;  
        }

        cam.orthographicSize = zoom;
        SceneManager.LoadSceneAsync("Level1");

        //cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }
}






