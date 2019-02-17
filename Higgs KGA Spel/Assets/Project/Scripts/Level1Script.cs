using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Script : MonoBehaviour
{
    private GameObject bossRat;
    private GameObject door;
    private GameObject player;   
    private BossRatScript bossRatScript;
    private PlayerActions playerActionsScript;
    private PlayerInput playerInputScript;
    private Audiomanager audiomanagerScript;

    public bool RatAlive = true;

    // Use this for initialization
    void Start ()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        bossRat = GameObject.FindGameObjectWithTag("Rat");
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
        playerInputScript = player.GetComponent<PlayerInput>();
        bossRatScript = bossRat.GetComponent<BossRatScript>();
        audiomanagerScript = FindObjectOfType<Audiomanager>();

        audiomanagerScript.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (bossRatScript.Health < 1)
        {
            if (playerInputScript.Interact && playerActionsScript.StandingByDoor)
            {
                Destroy(door);
                playerActionsScript.StandingByDoor = true;
            }
        }
	}
}