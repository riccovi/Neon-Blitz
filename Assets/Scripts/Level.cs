using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Level : MonoBehaviour
{
    public static Level instance;

    uint numEnemies = 0; // Enemy counter
    bool startNextLvl = false;
    float nextLvlTimer = 1; // Wait in seconds before next level
    string[] levels = {"Level1","Level2"};
    int currLvl = 1;

    int score = 0;
    TMP_Text scoreText;
    public TMP_Text highScoreText;

    public GameObject gameOverPanel;
    private bool isGameOver = false;

    private void Awake()
    {   
        if (instance == null)
        {
          instance = this;
          DontDestroyOnLoad(gameObject);
          scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.aspect = 4f / 5f; // Set to 4:5 ratio
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            return;
        }

        if (startNextLvl)
        {
            if (nextLvlTimer <= 0)
            {
                currLvl++;
                if (currLvl <= levels.Length)
                {
                    string sceneName = levels[currLvl-1];
                    SceneManager.LoadSceneAsync(sceneName);
                }
                else
                {
                    Debug.Log("GAME OVER");
                }
                nextLvlTimer = 1;
                startNextLvl = false;
            }
            else
            {
                nextLvlTimer -= Time.deltaTime;
            }
        }

        if (isGameOver && Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 1; // 'Unpause' the game
            SceneManager.LoadScene(0); //Load title screen
            isGameOver = false;
            score = 0;
            scoreText.text = "0";
            Destroy(gameObject);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void TriggerGameOver()
    {
        Time.timeScale = 0; // 'Pause' the game
        gameOverPanel.SetActive(true); // Display the GameOver Panel
        isGameOver = true;
        ResetGameState();
    }

    private void ResetGameState()
    {
        numEnemies = 0;
        startNextLvl = false;
        nextLvlTimer = 1;
        currLvl = 1;
    }

    public void HighScoreUpdate()
    {
        if(PlayerPrefs.HasKey("SavedHighScore")) // Does a highscore exist
        {
            // If current score is bigger than the saved highscore
            if(score > PlayerPrefs.GetInt("SavedHighScore")) 
            {
                // Set the saved highscore to the current score
                PlayerPrefs.SetInt("SavedHighScore", score); 
            }
        }
        else
        {
            PlayerPrefs.SetInt("SavedHighScore", score);
        }
        //Display the saved highscore visually
        highScoreText.text = PlayerPrefs.GetInt("SavedHighScore").ToString(); 
    }
    
    public void AddEnemy(){numEnemies++;}
    public void RemoveEnemy()
    {
        numEnemies--;
        if (numEnemies == 0)
        {
            startNextLvl = true;
        }
    }
}
