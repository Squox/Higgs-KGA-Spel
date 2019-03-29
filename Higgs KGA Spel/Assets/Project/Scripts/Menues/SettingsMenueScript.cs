using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenueScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    public void ToggleMusic()
    {
        Audiomanager.MusicOn = !Audiomanager.MusicOn;
    }

    public void Exit()
    {
        StartCoroutine(Gamemanager.LoadAsyncronously("Start menu", loadingScreen, slider, progressText));
    }

    public void DeleteSaves()
    {

    }
}
