using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // Objects
    public Transform soundsList;
    public AudioSource backgroundMusic;
    public Scrollbar musicSetting;
    public Scrollbar soundEffectsSetting;
    public Button musicMuteButton;
    public Button soundEffectsMuteButton;
    public RectTransform emptyMusicContent;
    public RectTransform emptySoundEffectsContent;

    // Variables
    private List<AudioSource> sounds = new List<AudioSource>();
    private static List<float> soundsDefaultVolume = new List<float>();
    private static float backgroundMusicDefaultVolume;
    private static bool oneTime = true;
    public static float musicValue = 1;
    public static float soundEffectsValue = 1;
    private static bool musicMute = false;
    private static bool soundEffectsMute = false;

    // Start is called before the first frame update
    void Start()
    {
        // Listeners
        musicMuteButton.onClick.AddListener(delegate { muteMusic(); });
        soundEffectsMuteButton.onClick.AddListener(delegate { muteSoundEffects(); });
        musicSetting.onValueChanged.AddListener(delegate { changeMusicVolume(); });
        soundEffectsSetting.onValueChanged.AddListener(delegate { changeSoundEffectsVolume(); });

        // Sets the initial mute sprites
        musicMuteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/MainMenu/" + (musicMute ? "Mute" : "Unmute"));
        soundEffectsMuteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/MainMenu/" + (soundEffectsMute ? "Mute" : "Unmute"));

        // Sets the scrollbar position
        emptyMusicContent.localPosition = new Vector3(0, (emptyMusicContent.rect.height / 2) - (emptyMusicContent.rect.height * (musicValue < 0 ? musicValue * -1 : musicValue)), 0);
        emptySoundEffectsContent.localPosition = new Vector3(0, (emptySoundEffectsContent.rect.height / 2) - (emptySoundEffectsContent.rect.height * (soundEffectsValue < 0 ? soundEffectsValue * -1 : soundEffectsValue)), 0);

        // Sets a list of sounds
        foreach (Transform audio in soundsList)
        {
            sounds.Add(audio.GetComponent<AudioSource>());

            if (oneTime) // Gets the default sound effect volume once
            {
                soundsDefaultVolume.Add(audio.GetComponent<AudioSource>().volume);
            }
        }

        if (oneTime) // Gets the default music volume once
        {
            backgroundMusicDefaultVolume = backgroundMusic.volume;

            oneTime = false;
        }

        // Sets the sound effects volume
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].volume = soundsDefaultVolume[i] * (soundEffectsMute ? 0 : soundEffectsValue);
        }

        // Sets the music volume
        backgroundMusic.volume = backgroundMusicDefaultVolume * (musicMute ? 0 : musicValue);
    }

    // Changes the mute status of the music settings
    public void muteMusic()
    {
        musicMute = musicMute ? false : true;
        musicMuteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/MainMenu/" + (musicMute ? "Mute" : "Unmute"));
        changeMusicVolume();
    }

    // Changes the mute status of the sound effects settings
    public void muteSoundEffects()
    {
        soundEffectsMute = soundEffectsMute ? false : true;
        soundEffectsMuteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/MainMenu/" + (soundEffectsMute ? "Mute" : "Unmute"));
        changeSoundEffectsVolume();
    }

    // Changes music volume when the scrollbar is changed
    public void changeMusicVolume()
    {
        musicValue = musicMute ? musicSetting.value * -1 : musicSetting.value;
        backgroundMusic.volume = backgroundMusicDefaultVolume * musicValue;
    }

    // Changes sound effects volume when the scrollbar is changed
    public void changeSoundEffectsVolume()
    {
        soundEffectsValue = soundEffectsMute ? soundEffectsSetting.value * -1 : soundEffectsSetting.value;

        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].volume = soundsDefaultVolume[i] * soundEffectsValue;
        }

        backgroundMusic.volume = backgroundMusicDefaultVolume * musicValue;
    }
}
