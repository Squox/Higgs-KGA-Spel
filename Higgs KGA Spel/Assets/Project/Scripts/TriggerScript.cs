using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    private Gamemanager GamemanagerScript;

    // Use this for initialization
    void Start () {
        GamemanagerScript = FindObjectOfType<Gamemanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GamemanagerScript.ExitLevel();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
