using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource; 
    [SerializeField] AudioSource sfxSource; 

    public AudioClip levelMusic;
    public AudioClip powerup1; // Shield powerup
    public AudioClip powerup2; // Speed powerup
    public AudioClip powerup3; // DestroyAllDestructables powerup 
    public AudioClip powerup4; // Gun powerup
    public AudioClip powerdown; 
    public AudioClip enemyDeath;
    public AudioClip shipDeath; 

    private void Start()
    {
        musicSource.clip = levelMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
