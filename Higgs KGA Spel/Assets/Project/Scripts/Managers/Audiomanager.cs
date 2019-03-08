using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;

    private Gamemanager gamemanagerScript;

    private AudioSource lvlMusic;

    public AudioSource CurrentMusic;

    public bool PlayerIsDead;
    public bool MusicOn;
    public bool PlayerHasWon = false;

    private float fadeTime = 60f;
    private float startVolume = 0f;

    private int fadeTimer = 0;  

    private bool playing = false;

    private void Awake()
    {
        MakeSingelton();

        CurrentMusic = GetComponent<AudioSource>();
        gamemanagerScript = FindObjectOfType<Gamemanager>();

        playing = false;

        fadeTimer = 0;           
    }

    private void Update()
    {
        if (MusicOn)
        {
            if (PlayerIsDead)
            {
                if (fadeTimer < 1)
                {
                    startVolume = CurrentMusic.volume;
                }

                FadeOut(CurrentMusic, startVolume, fadeTime);
            }
            else
            {
                if (startVolume > CurrentMusic.volume)
                {
                    CurrentMusic.volume = startVolume;
                }
            }
        }

        if (!MusicOn || !gamemanagerScript.InLevel)
        {
            StopMusic();
        }
        else if (MusicOn && !PlayerIsDead && gamemanagerScript.InLevel)
        {
            if (!playing)
            {
                CurrentMusic.Play(0);
                CurrentMusic.volume = 0.025f;
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

    public void StopMusic()
    {
        playing = false;
        CurrentMusic.Stop();
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
            CurrentMusic.Stop();
            playing = false;
        }
    }
}
