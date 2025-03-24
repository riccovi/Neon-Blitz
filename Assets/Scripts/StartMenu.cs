using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public AudioSource startSound;
    public GameObject upgradesPanel;
    public GameObject creditsPanel;
    public GameObject leaderboardPanel;
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

    public TMP_Text highScoreText;
    public TMP_Text highScoreText2;
    public TMP_Text highScoreText3;
    public TMP_Text highScoreText4;
    public TMP_Text highScoreText5;
    private string hiscore;

    void Start()
    {
       Camera.main.aspect = 4f / 5f; // Set to 4:5 ratio
       zoom = cam.orthographicSize;
       hiscore = PlayerPrefs.GetInt("SavedHighScore").ToString();
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
                leaderboardPanel.SetActive(false);
                //pressText.SetActive(false); // Disable 'press x' text
                counter++;
            // Second X press - zoom in
            } else if (!isZooming)
            {
                if (startSound != null){startSound.Play();} // Play start sound 
                upgradesPanel.SetActive(false); // Disable upgrades panel
                creditsPanel.SetActive(false);
                achievementsPanel.SetActive(false);
                leaderboardPanel.SetActive(false);
                isZooming = true;
            }    
        }

        // Credits Panel
        if (Input.GetKeyDown(KeyCode.C))
        {
            counter = 0;
            upgradesPanel.SetActive(false);
            achievementsPanel.SetActive(false);
            leaderboardPanel.SetActive(false);
            creditsPanel.SetActive(!creditsPanel.activeSelf);
        }

        // Leaderboard Panel
        if (Input.GetKeyDown(KeyCode.L))
        {
            counter = 0;
            upgradesPanel.SetActive(false);
            achievementsPanel.SetActive(false);
            creditsPanel.SetActive(false);
            leaderboardPanel.SetActive(!leaderboardPanel.activeSelf);
            //Getting high score
            highScoreText.text = hiscore; 
            highScoreText2.text = hiscore; 
            highScoreText3.text = hiscore; 
            highScoreText4.text = hiscore; 
            highScoreText5.text = hiscore; 
            //Positioning user on leaderboard logic
            if(PlayerPrefs.GetInt("SavedHighScore") >= 5200){ // 1st Position
                leaderboardPanel.transform.Find("1stPosition/ToddText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("1stPosition/5200Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("1stPosition/YouText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("1stPosition/hiscoreText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("2ndPosition/EmmaText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("2ndPosition/4150Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("2ndPosition/ToddText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("2ndPosition/5200Text").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("3rdPosition/BenText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("3rdPosition/3100Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("3rdPosition/EmmaText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("3rdPosition/4150Text").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("4thPosition/SamText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("4thPosition/2050Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("4thPosition/BenText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("4thPosition/3100Text").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("5thPosition/RookieText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/1000Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/SamText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("5thPosition/2050Text").gameObject.SetActive(true);
            }
            else if(PlayerPrefs.GetInt("SavedHighScore") >= 4150){ // 2nd Position
                leaderboardPanel.transform.Find("2ndPosition/EmmaText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("2ndPosition/4150Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("2ndPosition/YouText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("2ndPosition/hiscoreText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("3rdPosition/BenText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("3rdPosition/3100Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("3rdPosition/EmmaText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("3rdPosition/4150Text").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("4thPosition/SamText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("4thPosition/2050Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("4thPosition/BenText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("4thPosition/3100Text").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("5thPosition/RookieText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/1000Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/SamText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("5thPosition/2050Text").gameObject.SetActive(true);
            }
            else if(PlayerPrefs.GetInt("SavedHighScore") >= 3100){ // 3rd Position
                leaderboardPanel.transform.Find("3rdPosition/BenText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("3rdPosition/3100Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("3rdPosition/YouText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("3rdPosition/hiscoreText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("4thPosition/SamText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("4thPosition/2050Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("4thPosition/BenText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("4thPosition/3100Text").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("5thPosition/RookieText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/1000Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/SamText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("5thPosition/2050Text").gameObject.SetActive(true);
            }
            else if(PlayerPrefs.GetInt("SavedHighScore") >= 2050){ // 4th Position
                leaderboardPanel.transform.Find("4thPosition/SamText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("4thPosition/2050Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("4thPosition/YouText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("4thPosition/hiscoreText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("5thPosition/RookieText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/1000Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/SamText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("5thPosition/2050Text").gameObject.SetActive(true);
            }
            else if(PlayerPrefs.GetInt("SavedHighScore") >= 1000){ // 5th Position
                leaderboardPanel.transform.Find("5thPosition/RookieText").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/1000Text").gameObject.SetActive(false);
                leaderboardPanel.transform.Find("5thPosition/YouText").gameObject.SetActive(true);
                leaderboardPanel.transform.Find("5thPosition/hiscoreText").gameObject.SetActive(true);
            }
            
        }

        // Achivements Panel
        if (Input.GetKeyDown(KeyCode.A))
        {
            counter = 0;
            upgradesPanel.SetActive(false);
            creditsPanel.SetActive(false);
            leaderboardPanel.SetActive(false);
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






