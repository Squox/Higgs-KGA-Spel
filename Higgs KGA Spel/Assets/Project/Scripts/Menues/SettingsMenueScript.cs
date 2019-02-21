using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenueScript : MonoBehaviour
{
    private Audiomanager audiomanagerScript;

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
        SceneManager.LoadScene("Start menu");
    }
}
