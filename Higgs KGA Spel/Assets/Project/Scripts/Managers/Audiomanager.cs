using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;

    private static AudioSource currentMusic;

    public static bool MusicOn = true;

    private static float startVolume = 0f;

    private void Awake()
    {
        MakeSingelton();
    }

    private void MakeSingelton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }       
    }

    public static void PlayMusic(AudioSource audioSource, float volume)
    {
        if (MusicOn)
        {
            audioSource.Play(0);
            audioSource.volume = volume;

            currentMusic = audioSource;
        }
    }

    public static void StopMusic()
    {
        currentMusic.Stop();
    }

    public static IEnumerator FadeOut(float fadeTime)
    {
        if (MusicOn)
        {
            float startVolume = currentMusic.volume;

            while (currentMusic.volume > 0)
            {
                currentMusic.volume -= startVolume / fadeTime;

                yield return null;
            }

            currentMusic.Stop();

            currentMusic.volume = startVolume;
        }      
    }
}
