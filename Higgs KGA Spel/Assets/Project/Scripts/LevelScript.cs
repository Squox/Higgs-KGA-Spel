using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    private GameObject bossRat;
    private GameObject door;
    private GameObject player;
    private BossRatScript bossRatScript;
    private PlayerActions playerActionsScript;

    public bool RatAlive = true;

    // Use this for initialization
    void Start ()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        bossRat = GameObject.FindGameObjectWithTag("Rat");
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
        bossRatScript = bossRat.GetComponent<BossRatScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(bossRatScript.Health < 1)
        {
            RatAlive = false;
            Destroy(door);
        }
	}
}
