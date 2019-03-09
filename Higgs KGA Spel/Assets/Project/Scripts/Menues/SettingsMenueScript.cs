using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenueScript : MonoBehaviour
{
    private Audiomanager audiomanagerScript;
    private Gamemanager gamemanagerScript;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    // Use this for initialization
    void Start ()
    {
        audiomanagerScript = FindObjectOfType<Audiomanager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleMusic()
    {
        audiomanagerScript.MusicOn = !audiomanagerScript.MusicOn;
    }

    public void Exit()
    {
        StartCoroutine(gamemanagerScript.LoadAsyncronously("Start menu", loadingScreen, slider, progressText));
    }
}
