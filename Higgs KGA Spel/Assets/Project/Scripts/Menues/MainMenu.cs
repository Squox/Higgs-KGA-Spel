using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    private void Start()
    {
        if(Gamemanager.HighestLevel > 0)
            Gamemanager.LoadPlayer();
    }

    public void PlayGame()
    {
        if (Gamemanager.HighestLevel == 0)
        {
            StartCoroutine(Gamemanager.LoadAsyncronously("First Level", loadingScreen, slider, progressText));
        }
        else
        {
            StartCoroutine(Gamemanager.LoadAsyncronously("Selection menue", loadingScreen, slider, progressText));
        }       
    }

    public void QuitGame()
    {
        Application.Quit();
    } 
    
    public void Settings()
    {
        StartCoroutine(Gamemanager.LoadAsyncronously("Settings menue", loadingScreen, slider, progressText));
    }

    
}