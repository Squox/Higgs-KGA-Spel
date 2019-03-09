using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour
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
	void Update () {
		
	}

    public void Exit()
    {
        StartCoroutine(gamemanagerScript.LoadAsyncronously("Selection menue", loadingScreen, slider, progressText));
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(gamemanagerScript.LoadAsyncronously("First Level", loadingScreen, slider, progressText));
    }
    public void LoadSecondLevel()
    {
        StartCoroutine(gamemanagerScript.LoadAsyncronously("Second Level", loadingScreen, slider, progressText));
    }
    public void LoadThirdLevel()
    {
        StartCoroutine(gamemanagerScript.LoadAsyncronously("Third Level", loadingScreen, slider, progressText));
    }
    public void LoadFourthLevel()
    {
        StartCoroutine(gamemanagerScript.LoadAsyncronously("Fourth Level", loadingScreen, slider, progressText));
    }
}
