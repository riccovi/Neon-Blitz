using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class StartMenu : MonoBehaviour
{
    public AudioSource startSound;
    public GameObject upgradesPanel;
    public GameObject pressText;

    private float counter = 0;
    void Update()
    {
        // First X press - open panel
        if (Input.GetKeyDown(KeyCode.X)) // GeyKeyDown, so that sound doesn't spam repeat when X is pressed
        {
            upgradesPanel.SetActive(true); //Enable upgrades panel
            pressText.SetActive(false); // Disable 'press x' text
            counter++;
        }

        // Next X press - start game
        if (Input.GetKey(KeyCode.X) && counter > 0){
            if (startSound != null){startSound.Play();} // Play start sound
            SceneManager.LoadSceneAsync("Level1"); // Start level 1
        }
    }
}
