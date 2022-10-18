using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Objects
    public Transform soundsList;
    public AudioSource backgroundMusic;

    // Variables
    private List<AudioSource> sounds = new List<AudioSource>();
    private List<float> soundsDefaultVolume = new List<float>();
    private float backgroundMusicDefaultVolume;
    private bool oneTime = true;

    // Start is called before the first frame update
    void Start()
    {
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

        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].volume = soundsDefaultVolume[i] * SettingsManager.soundEffectsValue;
        }

        backgroundMusic.volume = backgroundMusicDefaultVolume * SettingsManager.musicValue;
    }
}
