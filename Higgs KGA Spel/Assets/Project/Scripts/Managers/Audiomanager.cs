using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;

    public AudioSource CurrentMusic;

    public bool MusicOn;

    private float startVolume = 0f;

    private void Awake()
    {
        MakeSingelton();

        CurrentMusic = GetComponent<AudioSource>(); 
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

    public void PlayMusic(float volume)
    {      
        CurrentMusic.Play(0);
        CurrentMusic.volume = volume;
    }

    public void StopMusic()
    {
        CurrentMusic.Stop();
    }

    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume / fadeTime;

            yield return null;
        }

        CurrentMusic.Stop();
    }
}
