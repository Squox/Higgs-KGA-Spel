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
        if (GamemanagerScript.LastLevel == 1)
        {
            SceneManager.LoadScene("First Level");
        }
        else if (GamemanagerScript.LastLevel == 2)
        {
            SceneManager.LoadScene("Second Level");
        }
        else if (GamemanagerScript.LastLevel == 3)
        {
            SceneManager.LoadScene("Third Level");
        }
        else if (GamemanagerScript.LastLevel == 4)
        {
            SceneManager.LoadScene("Fourth Level");
        }
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
