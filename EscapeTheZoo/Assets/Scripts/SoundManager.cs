using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // Objects
    public Transform soundsList;
    public AudioSource backgroundMusic;

    // Variables
    private List<AudioSource> sounds = new List<AudioSource>();
    private static List<float> soundsDefaultVolume = new List<float>();
    private static float backgroundMusicDefaultVolume;
    private static List<string> sceneList = new List<string>();
    private static List<int> soundsListPosition = new List<int>();
    private static int audioCount = 0;

    // Awake is called before the first frame update, and before start
    void Awake()
    {
        bool notLoadedSceneBefore = !(sceneList.IndexOf(SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name) >= 0);

        // Sets the current sounds list position
        if (notLoadedSceneBefore)
        {
            soundsListPosition.Add(audioCount);

        }

        // Sets the list of sounds
        foreach (Transform audio in soundsList)
        {
            sounds.Add(audio.GetComponent<AudioSource>());

            if (notLoadedSceneBefore)
            {
                soundsDefaultVolume.Add(audio.GetComponent<AudioSource>().volume);
                audioCount++;
            }
        }

        // Sets the sounds list position, background music default volume, and adds the current scene to the scene list
        if (notLoadedSceneBefore)
        {
            backgroundMusicDefaultVolume = backgroundMusic.volume;
            sceneList.Add(SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name);
        }

        // Sets the sound effects volume based on the sound effects setting
        for (int i = 0; i < sounds.Count; i++)
        {
            sounds[i].volume = soundsDefaultVolume[i + soundsListPosition[sceneList.IndexOf(SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name)]] * SettingsManager.soundEffectsValue;
        }

        // Sets the musics volume based on the music setting
        backgroundMusic.volume = backgroundMusicDefaultVolume * SettingsManager.musicValue;
    }
}
