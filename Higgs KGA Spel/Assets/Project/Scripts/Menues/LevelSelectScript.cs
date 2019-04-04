using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    public void Exit()
    {
        StartCoroutine(Gamemanager.LoadAsyncronously("Selection menue", loadingScreen, slider, progressText));
    }

    public void LoadFirstLevel()
    {
        if (Gamemanager.HighestLevel >= 1)
            StartCoroutine(Gamemanager.LoadAsyncronously("First Level", loadingScreen, slider, progressText));
    }
    public void LoadSecondLevel()
    {
        if (Gamemanager.HighestLevel >= 2)
            StartCoroutine(Gamemanager.LoadAsyncronously("Second Level", loadingScreen, slider, progressText));
    }
    public void LoadThirdLevel()
    {
        if (Gamemanager.HighestLevel >= 3)
            StartCoroutine(Gamemanager.LoadAsyncronously("Third Level", loadingScreen, slider, progressText));
    }
    public void LoadFourthLevel()
    {
        if (Gamemanager.HighestLevel >= 4)
            StartCoroutine(Gamemanager.LoadAsyncronously("Fourth Level", loadingScreen, slider, progressText));
    }
}
