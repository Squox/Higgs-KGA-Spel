using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    public void Continue()
    {
        Debug.Log("hEY");

        if (Gamemanager.LastLevel == 1)
        {
            StartCoroutine(Gamemanager.LoadAsyncronously("First Level", loadingScreen, slider, progressText));
        }
        else if (Gamemanager.LastLevel == 2)
        {
            StartCoroutine(Gamemanager.LoadAsyncronously("Second Level", loadingScreen, slider, progressText));
        }
        else if (Gamemanager.LastLevel == 3)
        {
            StartCoroutine(Gamemanager.LoadAsyncronously("Third Level", loadingScreen, slider, progressText));
        }
        else if (Gamemanager.LastLevel == 4)
        {
            StartCoroutine(Gamemanager.LoadAsyncronously("Fourth Level", loadingScreen, slider, progressText));
        }
    }

    public void Exit()
    {
        Debug.Log("hEY");
        StartCoroutine(Gamemanager.LoadAsyncronously("Start menu", loadingScreen, slider, progressText));
    }

    public void SelectLevel()
    {
        Debug.Log("hEY");
        StartCoroutine(Gamemanager.LoadAsyncronously("Level select", loadingScreen, slider, progressText));
    }
}
