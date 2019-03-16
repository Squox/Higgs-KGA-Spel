using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Script : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    [SerializeField] private AudioSource audioSource;

    private GameObject door;
    private GameObject player;   
    private PlayerActions playerActionsScript;
    private PlayerInput playerInputScript;

    public bool RatAlive = true;

    private float musicVolume = 0.5f;

    // Use this for initialization
    void Start ()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
        playerInputScript = player.GetComponent<PlayerInput>();

        Gamemanager.LoadingScreen = loadingScreen;
        Gamemanager.Slider = slider;
        Gamemanager.ProgressText = progressText;

        Gamemanager.LastLevel = 1;

        Audiomanager.PlayMusic(audioSource, musicVolume);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (!RatAlive)
        {
            if (playerInputScript.Interact && playerActionsScript.StandingByDoor)
            {
                Destroy(door);
                playerActionsScript.StandingByDoor = true;
                RatAlive = true;
            }
        }
	}
}