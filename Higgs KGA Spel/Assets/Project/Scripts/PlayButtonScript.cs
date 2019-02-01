using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
		
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
