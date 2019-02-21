using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2Script : MonoBehaviour
{
    private Audiomanager audiomanagerScript;
    private Gamemanager gamemanagerScript;
    private GameObject player;
    private Transform playerTF;

    private float checkRange = 1f;

    [SerializeField] private GameObject checkPoint0;
    [SerializeField] private GameObject checkPoint1;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTF = player.GetComponent<Transform>();
        audiomanagerScript = FindObjectOfType<Audiomanager>();
        gamemanagerScript = FindObjectOfType<Gamemanager>();
        audiomanagerScript.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;

        if (gamemanagerScript.LastCheckpointPosition == new Vector3(0,0,0))
        {
            gamemanagerScript.LastCheckpointPosition = checkPoint0.transform.position;
            gamemanagerScript.CheckPointCounter = 0;
        }
        else
        {
            playerTF.position = gamemanagerScript.LastCheckpointPosition;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerTF != null && checkPoint0 != null && checkPoint1 != null)
        {
            if (isInRange(playerTF, checkPoint0.transform, checkRange) && gamemanagerScript.CheckPointCounter < 1)
            {
                gamemanagerScript.LastCheckpointPosition = checkPoint0.transform.position;
                gamemanagerScript.CheckPointCounter = 0;
            }
            else if (isInRange(playerTF, checkPoint1.transform, checkRange) && gamemanagerScript.CheckPointCounter < 2)
            {
                gamemanagerScript.LastCheckpointPosition = checkPoint1.transform.position;
                checkPoint1.GetComponent<SpriteRenderer>().color = Color.green;
                gamemanagerScript.CheckPointCounter = 1;
            }
        }       
    }

    private bool isInRange(Transform transform1, Transform transform2, float range)
    {
        if (Mathf.Abs(transform1.position.x - transform2.position.x) < range && Mathf.Abs(transform1.position.y - transform2.position.y) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
