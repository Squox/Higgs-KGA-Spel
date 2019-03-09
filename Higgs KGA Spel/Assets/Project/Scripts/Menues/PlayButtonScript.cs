using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{
    private Gamemanager gamemanagerScript;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    // Use this for initialization
    void Start ()
    {
        gamemanagerScript = FindObjectOfType<Gamemanager>();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void Continue()
    {
        if (gamemanagerScript.LastLevel == 1)
        {
            StartCoroutine(gamemanagerScript.LoadAsyncronously("First Level", loadingScreen, slider, progressText));
        }
        else if (gamemanagerScript.LastLevel == 2)
        {
            StartCoroutine(gamemanagerScript.LoadAsyncronously("Second Level", loadingScreen, slider, progressText));
        }
        else if (gamemanagerScript.LastLevel == 3)
        {
            StartCoroutine(gamemanagerScript.LoadAsyncronously("Third Level", loadingScreen, slider, progressText));
        }
        else if (gamemanagerScript.LastLevel == 4)
        {
            StartCoroutine(gamemanagerScript.LoadAsyncronously("Fourth Level", loadingScreen, slider, progressText));
        }
    }

    public void Exit()
    {
        StartCoroutine(gamemanagerScript.LoadAsyncronously("Start menu", loadingScreen, slider, progressText));
    }

    public void SelectLevel()
    {
        StartCoroutine(gamemanagerScript.LoadAsyncronously("Level select", loadingScreen, slider, progressText));
    }
}
