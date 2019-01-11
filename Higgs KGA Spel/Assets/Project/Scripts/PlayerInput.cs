﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //To store PlayerActions script and player in a local variable
    private GameObject player;
    private PlayerActions playerActionsScript;

    // Ints:
    public int MoveDirection = 0;

    // Bools:
    public bool HasPressedJump;

	// Use this for initialization
	private void Start ()
    {
		
	}
	
	// Update is called once per frame
	private void Update ()
    {

        if (Input.GetKey(KeyCode.G))
        {
            FindObjectOfType<Gamemanager>().RestartGame();
        }
        CheckPlayerInput();

        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
    }

   

    private void CheckPlayerInput()
    {
        MoveDirection = 0;

        if (Input.GetKey(KeyCode.A))
        {
            MoveDirection = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveDirection = 1;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            HasPressedJump = true;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            playerActionsScript.shoot();
        }    
    }
}
