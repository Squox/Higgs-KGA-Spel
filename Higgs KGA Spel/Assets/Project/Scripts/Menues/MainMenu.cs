using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    private void Start()
    {
        //if (Gamemanager.HighestLevel > 0)
        //    Gamemanager.LoadPlayer();
    }

    public void PlayGame()
    {
        if (Gamemanager.HighestLevel == 0)
        {
            StartCoroutine(Gamemanager.LoadAsyncronously("First Level", loadingScreen, slider, progressText));
        }
        else
        {
            Gamemanager.LoadScene("Selection menue");
        }       
    }

    public void QuitGame()
    {
        Application.Quit();
    } 
    
    public void Settings()
    {
        Gamemanager.LoadScene("Settings menue");
    }   
}