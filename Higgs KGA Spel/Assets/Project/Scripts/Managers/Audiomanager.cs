using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager Instance;

    private static AudioSource currentMusic;

    public static bool MusicOn;

    private void Awake()
    {
        MakeSingelton();
    }

    private void MakeSingelton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
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
        if (!MusicOn)
            return;

        currentMusic.Stop();
    }

    public static void FadeOut(float fadeTime)
    {
        if (MusicOn)
        {
            Instance.StartCoroutine(Utility.FadeOut(null, currentMusic, fadeTime, currentMusic.volume));
        }      
    }
}
