﻿using UnityEngine;
using UnityEngine.UI;

public class Level1Script : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    [SerializeField] private AudioSource audioSource;

    private GameObject door;
    private GameObject player;

    public bool RatAlive = true;

    private float musicVolume = 0.5f;

    // Use this for initialization
    void Start ()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        player = GameObject.FindGameObjectWithTag("Player");

        LevelSetup.SetUpLevel(1, loadingScreen, slider, progressText, audioSource, musicVolume);
        Gamemanager.SavePlayer(player.GetComponent<PlayerController>(), Gamemanager.LastLevel);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (!RatAlive)
        {
            if (PlayerInput.Interact && PlayerController.StandingByDoor)
            {
                Destroy(door);
                PlayerController.StandingByDoor = true;
                RatAlive = true;
            }
        }
	}
}