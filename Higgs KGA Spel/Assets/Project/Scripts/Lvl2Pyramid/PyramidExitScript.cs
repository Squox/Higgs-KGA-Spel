using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PyramidExitScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCam;

    private float camSizeOutsidePyraid = 3f;

    private float sizeTransitionSpeed = 20f; //Higher is slower

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerCam.m_Lens.OrthographicSize != camSizeOutsidePyraid)
            StartCoroutine(camTransition());
    }

    private IEnumerator camTransition()
    {
        float startSize = playerCam.m_Lens.OrthographicSize;

        if (startSize - camSizeOutsidePyraid < 0)
        {
            while (playerCam.m_Lens.OrthographicSize < camSizeOutsidePyraid)
            {
                playerCam.m_Lens.OrthographicSize += 1 / sizeTransitionSpeed;

                yield return null;
            }
        }
        else
        {
            while (playerCam.m_Lens.OrthographicSize > camSizeOutsidePyraid)
            {
                playerCam.m_Lens.OrthographicSize -= 1 / sizeTransitionSpeed;

                yield return null;
            }
        }      

        playerCam.m_Lens.OrthographicSize = camSizeOutsidePyraid;
    }
}
