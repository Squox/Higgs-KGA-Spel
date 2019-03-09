using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Gamemanager gamemanagerScript;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    private void Awake()
    {
        gamemanagerScript = FindObjectOfType<Gamemanager>();
    }

    public void PlayGame()
    {
        if (gamemanagerScript.HighestLevel == 0)
        {
            StartCoroutine(gamemanagerScript.LoadAsyncronously("First Level", loadingScreen, slider, progressText));
        }
        else
        {
            StartCoroutine(gamemanagerScript.LoadAsyncronously("Selection menue", loadingScreen, slider, progressText));
        }       
    }

    public void QuitGame()
    {
        Application.Quit();
    } 
    
    public void Settings()
    {
        StartCoroutine(gamemanagerScript.LoadAsyncronously("Settings menue", loadingScreen, slider, progressText));
    }

    
}