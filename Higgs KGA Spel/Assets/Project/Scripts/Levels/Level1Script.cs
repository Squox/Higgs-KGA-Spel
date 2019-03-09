using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Script : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    private Gamemanager gamemanagerScript;

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
        gamemanagerScript = FindObjectOfType<Gamemanager>();

        gamemanagerScript.LoadingScreen = loadingScreen;
        gamemanagerScript.Slider = slider;
        gamemanagerScript.ProgressText = progressText;

        gamemanagerScript.LastLevel = 1;

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