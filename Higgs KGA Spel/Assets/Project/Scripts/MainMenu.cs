using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Selection menue");
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