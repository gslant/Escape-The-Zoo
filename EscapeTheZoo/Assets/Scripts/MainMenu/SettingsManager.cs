using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Objects
    public Transform soundsList;
    public AudioSource backgroundMusic;
    public Button settingsCloseButton;
    public Scrollbar musicSetting;
    public Scrollbar soundEffectsSetting;

    // Variables
    private List<AudioSource> sounds = new List<AudioSource>();
    private List<float> soundsDefaultVolume = new List<float>();
    private float backgroundMusicDefaultVolume;
    private bool oneTime = true;
    public static float musicValue = 1;
    public static float soundEffectsValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        settingsCloseButton.onClick.AddListener(delegate { setSettings(); });

        if (oneTime)
        {
            foreach (Transform audio in soundsList)
            {
                sounds.Add(audio.GetComponent<AudioSource>());
                soundsDefaultVolume.Add(audio.GetComponent<AudioSource>().volume);
            }

            backgroundMusicDefaultVolume = backgroundMusic.volume;

            oneTime = false;
        }
    }

    // Sets the current settings values
    public void setSettings()
    {
        musicValue = musicSetting.value;
        soundEffectsValue = soundEffectsSetting.value;
        setMainSceneAudio();
    }

    // Changes the main scene's audio volume based on the settings
    public void setMainSceneAudio()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].volume = soundsDefaultVolume[i] * SettingsManager.soundEffectsValue;
        }

        backgroundMusic.volume = backgroundMusicDefaultVolume * SettingsManager.musicValue;
    }
}
