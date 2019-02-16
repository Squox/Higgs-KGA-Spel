using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private GameObject ratPrefab;
    [SerializeField] private Transform ratShootingPoint;

    private GameObject bossRat;
    private GameObject door;
    private GameObject player;   
    private BossRatScript bossRatScript;
    private PlayerActions playerActionsScript;
    private PlayerInput playerInputScript;   

    public bool RatAlive = true;
    private bool hasInstantiatedRats = false;

    // Use this for initialization
    void Start ()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        bossRat = GameObject.FindGameObjectWithTag("Rat");
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
        playerInputScript = player.GetComponent<PlayerInput>();
        bossRatScript = bossRat.GetComponent<BossRatScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(bossRatScript.Health < 1)
        {
            RatAlive = false;

            if (playerInputScript.Interact && playerActionsScript.StandingByDoor)
            {
                Destroy(door);
                playerActionsScript.StandingByDoor = true;
            }
        }
	}
}