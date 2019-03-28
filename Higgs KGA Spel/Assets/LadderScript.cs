using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private bool playerOnLadder;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOnLadder = true;
            PlayerInput.OnLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOnLadder = false;
            StartCoroutine(stepOffLadder());
        }
    }

    private IEnumerator stepOffLadder()
    {
        yield return null;
        yield return null;
        if (playerOnLadder)
            yield break;
        else
            PlayerInput.OnLadder = false;
    }
}
