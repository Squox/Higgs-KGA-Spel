using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PyramidEntranceScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCam;

    private float camSizeInPyraid = 4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerCam.m_Lens.OrthographicSize = camSizeInPyraid;
    }
}
