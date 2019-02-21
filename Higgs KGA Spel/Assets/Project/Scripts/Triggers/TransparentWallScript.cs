using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWallScript : MonoBehaviour
{
    private int counter;
    private int countTime = 2;
    private bool playerInTrigger = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!playerInTrigger)
        {
            counter++;
            if (counter > countTime)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
            playerInTrigger = true;
            GetComponent<SpriteRenderer>().sortingLayerName = "BackgroundElements";
            counter = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInTrigger = false;
        }
    }
}
