using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Exit()
    {
        SceneManager.LoadScene("Selection menue");
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("First Level");
    }
    public void LoadSecondLevel()
    {
        SceneManager.LoadScene("Second Level");
    }
    public void LoadThirdLevel()
    {
        SceneManager.LoadScene("Third Level");
    }
    public void LoadFourthLevel()
    {
        SceneManager.LoadScene("Fourth Level");
    }
}
