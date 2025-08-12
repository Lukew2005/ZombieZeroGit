
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;


public class OptionsMenu : MonoBehaviour
{
    // reference to the audio mixer
    public AudioMixer audioMixer;

    // reference to the volume sliders
    public Slider masterVolumeSlider;

    public Slider musicVolumeSlider;

    public Slider sfxVolumeSlider;

    // reference to the volume value labels
    public TMP_Text masterVolumeLevel;

    public TMP_Text musicVolumeLevel;

    public TMP_Text sfxVolumeLevel;

    // volume slider level offset
    public float volumeSliderLevelOffset = 80f;



    
    void Start()
    {
        GetVolumeLevels();
    }


    private void GetVolumeLevels()
    {
        // if the master volume has already been saved
        if (PlayerPrefs.HasKey("Master Volume"))
        {
            // get the master volume value and set it
            audioMixer.SetFloat("Master Volume", PlayerPrefs.GetFloat("Master Volume"));

            // update the master volume slider
            masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume");
        }

        // if the music volume has already been saved
        if (PlayerPrefs.HasKey("Music Volume"))
        {
            // get the music volume value and set it
            audioMixer.SetFloat("Music Volume", PlayerPrefs.GetFloat("Music Volume"));

            // update the music volume slider
            musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume");
        }

        // if the sfx volume has already been saved
        if (PlayerPrefs.HasKey("SFX Volume"))
        {
            // get the sfx volume value and set it
            audioMixer.SetFloat("SFX Volume", PlayerPrefs.GetFloat("SFX Volume"));

            // update the sfx volume slider
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFX Volume");
        }

        // update the volume level labels
        masterVolumeLevel.text = (masterVolumeSlider.value + volumeSliderLevelOffset).ToString() + "%";

        musicVolumeLevel.text = (musicVolumeSlider.value + volumeSliderLevelOffset).ToString() + "%";

        sfxVolumeLevel.text = (sfxVolumeSlider.value + volumeSliderLevelOffset).ToString() + "%";
    }


    // set volume levels
    // master volume
    public void SetMasterVolume()
    {
        // update the master volume level label
        masterVolumeLevel.text = (masterVolumeSlider.value + volumeSliderLevelOffset).ToString() + "%";

        // set the master volume level slider
        audioMixer.SetFloat("Master Volume", masterVolumeSlider.value);

        // and save the value
        PlayerPrefs.SetFloat("Master Volume", masterVolumeSlider.value);
    }


    // music volume
    public void SetMusicVolume()
    {
        // update the music volume level label
        musicVolumeLevel.text = (musicVolumeSlider.value + volumeSliderLevelOffset).ToString() + "%";

        // set the music volume level slider
        audioMixer.SetFloat("Music Volume", musicVolumeSlider.value);

        // and save the value
        PlayerPrefs.SetFloat("Music Volume", musicVolumeSlider.value);
    }


    // sfx volume
    public void SetSFXVolume()
    {
        // update the sfx volume level label
        sfxVolumeLevel.text = (sfxVolumeSlider.value + volumeSliderLevelOffset).ToString() + "%";

        // set the sfx volume level slider
        audioMixer.SetFloat("SFX Volume", sfxVolumeSlider.value);

        // and save the value
        PlayerPrefs.SetFloat("SFX Volume", sfxVolumeSlider.value);
    }


} // end if class
