using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;

    private AudioSource lvlMusic;
    private AudioSource currentMusic;

    public bool PlayerIsDead;

    private float fadeTime = 60f;
    private float startVolume = 0f;

    private int fadeTimer = 0;

    public bool MusicOn = true;

    private bool playing = false;

    private void Awake()
    {
        playing = false;

        fadeTimer = 0;

        MakeSingelton();

        if (MusicOn)
        {
            currentMusic = GetComponent<AudioSource>();
        }         
    }

    private void Update()
    {
        if (currentMusic != null)
        {
            Debug.Log(currentMusic);
        }

        Debug.Log(playing);

        if (currentMusic != null && MusicOn)
        {
            if (PlayerIsDead)
            {
                if (fadeTimer < 1)
                {
                    startVolume = currentMusic.volume;
                }

                FadeOut(currentMusic, startVolume, fadeTime);
            }
            else
            {
                if (startVolume > currentMusic.volume)
                {
                    currentMusic.volume = startVolume;
                }
            }
        }

        if (!MusicOn)
        {
            currentMusic.Stop();
            playing = false;
        }
        else if(MusicOn && !PlayerIsDead)
        {
            if (!playing && currentMusic.clip != null)
            {
                currentMusic.Play(0);
                currentMusic.volume = 0.025f;
                playing = true;
            }           
        }
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

    public void FadeOut(AudioSource audioSource, float startVolume, float fadeTime)
    {
        if (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume / fadeTime;
            fadeTimer++;
        }
        else
        {
            currentMusic.Stop();
            playing = false;
        }
    }
}
