using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PyramidEntranceScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCam;

    private float camSizeInPyraid = 4f;

    private float sizeTransitionSpeed = 30f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerCam.m_Lens.OrthographicSize != camSizeInPyraid)
            StartCoroutine(camTransition());
    }

    private IEnumerator camTransition()
    {
        float startSize = playerCam.m_Lens.OrthographicSize;

        if (startSize - camSizeInPyraid < 0)
        {
            while (playerCam.m_Lens.OrthographicSize < camSizeInPyraid)
            {
                playerCam.m_Lens.OrthographicSize += 1 / sizeTransitionSpeed;

                yield return null;
            }
        }
        else
        {
            while (playerCam.m_Lens.OrthographicSize > camSizeInPyraid)
            {
                playerCam.m_Lens.OrthographicSize -= 1 / sizeTransitionSpeed;

                yield return null;
            }
        }

        playerCam.m_Lens.OrthographicSize = camSizeInPyraid;
    }
}
