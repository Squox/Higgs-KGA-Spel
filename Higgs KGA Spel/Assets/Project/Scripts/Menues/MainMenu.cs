using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Gamemanager gamemanagerScript;

    private void Awake()
    {
        gamemanagerScript = FindObjectOfType<Gamemanager>();
    }

    public void PlayGame()
    {
        if (gamemanagerScript.HighestLevel == 0)
        {
            SceneManager.LoadScene("First Level");
        }
        else
        {
            SceneManager.LoadScene("Selection menue");
        }       
    }

    public void QuitGame()
    {
        Application.Quit();
    } 
    
    public void Settings()
    {
        SceneManager.LoadScene("Settings menue");
    }
}