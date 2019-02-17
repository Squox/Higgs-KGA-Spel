using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2Script : MonoBehaviour
{
    private Audiomanager audiomanagerScript;

    // Use this for initialization
    void Start ()
    {
        audiomanagerScript = FindObjectOfType<Audiomanager>();
        audiomanagerScript.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
