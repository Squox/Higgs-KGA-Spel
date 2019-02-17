﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //To store PlayerActions script and player in a local variable
    private GameObject player;
    private PlayerActions playerActionsScript;
    private Gamemanager GamemanagerScript;
    private UIManager uiManagerScript;

    // Ints:
    public int MoveDirection = 0;
    public int ChargeTimer = 0;

    private int chargeTime = 180;

    // Bools:
    public bool HasPressedJump;
    public bool Interact;

    private bool canExit = false;
    private bool canUnpause = false;

    // Use this for initialization
    private void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
    }

    private void Start()
    {
        GamemanagerScript = FindObjectOfType<Gamemanager>();
        GetComponent<PlayerActions>().enabled = true;
    }

    // Update is called once per frame
    private void Update ()
    {
        if (playerActionsScript.Exit)
        {
            canExit = true;
        }
        if (playerActionsScript.Paused)
        {
            canUnpause = true;
        }

        CheckPlayerInput();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(canExit || playerActionsScript.Health < 1)
            {
                GamemanagerScript.RestartGame();
                canExit = false;
            }           
        }

        if (Input.GetKeyUp(KeyCode.I) && ChargeTimer < chargeTime)
        {
            playerActionsScript.shoot();
            ChargeTimer = 0;
        }

        if (Input.GetKey(KeyCode.I))
        {
            ChargeTimer++;           

            if (ChargeTimer == chargeTime * 1/3)
            {
                playerActionsScript.PowerShot = true;
                playerActionsScript.ShotCount = 1;                
            }
            if (ChargeTimer == chargeTime * 2/3)
            {
                playerActionsScript.ShotCount = 2;
            }
            if (ChargeTimer == chargeTime * 3/3)
            {
                playerActionsScript.ShotCount = 3;
            }
        }

        if (Input.GetKeyUp(KeyCode.I) && ChargeTimer > chargeTime)
        {
            playerActionsScript.PowerShot = false;
            ChargeTimer = 0;
            playerActionsScript.EmpoweredShot();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerActionsScript.Doge();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerActionsScript.Doge();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canExit)
            {
                GamemanagerScript.LastLevel++;
                GamemanagerScript.ExitLevel();
            }
            else if (canUnpause)
            {
                playerActionsScript.Unpause = true;
                canUnpause = false;
            }
            else if (playerActionsScript.Health < 1)
            {
                GamemanagerScript.ExitLevel();
            }
            else if (playerActionsScript.Health > 0 && !playerActionsScript.Paused)
            {
                playerActionsScript.Paused = true;
            }            
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            Interact = false;
        }
    }
}
