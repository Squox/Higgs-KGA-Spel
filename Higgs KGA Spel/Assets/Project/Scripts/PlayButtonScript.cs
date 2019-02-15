using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    private Gamemanager GamemanagerScript;

    // Use this for initialization
    void Start ()
    {
        GamemanagerScript = FindObjectOfType<Gamemanager>();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void Continue()
    {
        SceneManager.LoadScene("First Level");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Start menu");
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("Level select");
    }
}
