
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    // enable other scripts to access this script
    public static AudioManager instance;

    // reference to the background music audio source
    public AudioSource backgroundMusic;

    // reference to the level victory music audio source
    public AudioSource victory;


    // list of sound effects
    public AudioSource[] soundEffects;



    private void Awake()
    {
        instance = this;
    }



    // stop playing the background music
    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }


    // play the level 'victory' music
    public void PlayLevelVictory()
    {
        // stop playing the background music first
        StopBackgroundMusic();

        victory.Play();
    }


    // play the required sound effect
    public void PlaySFX(int sfxNumber)
    {
        // if the sound effect is already playing, stop playing sound effect
        soundEffects[sfxNumber].Stop();

        soundEffects[sfxNumber].Play();
    }


    // stop playing the required sound effect
    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }


} // end of class
