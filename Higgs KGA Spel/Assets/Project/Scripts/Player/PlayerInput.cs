using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //To store PlayerActions script and player in a local variable
    private GameObject player;
    private PlayerActions playerActionsScript;

    // Ints:
    public int MoveDirection = 0;
    public int ChargeTimer = 0;

    private int chargeTime = 180;
    private int fadeTimer;

    // Bools:
    public bool HasPressedJump;
    public bool Interact;

    private bool canExit = false;
    private bool canUnpause = false;
    private bool canRestart = false;

    // Use this for initialization
    private void Awake ()
    {
        fadeTimer = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
    }

    private void Start()
    {
        GetComponent<PlayerActions>().enabled = true;
    }

    // Update is called once per frame
    private void Update ()
    {
        CheckPlayerInput();
    }

    private void CheckPlayerInput()
    {
        MoveDirection = 0;

        if (playerActionsScript.Health > 0 && !playerActionsScript.CanExit)
        {
            if (Input.GetKey(KeyCode.A))
            {
                MoveDirection = -1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                MoveDirection = 1;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerActionsScript.Doge();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerActionsScript.Doge();
            }

            if (Input.GetKeyUp(KeyCode.I) && ChargeTimer < chargeTime)
            {
                playerActionsScript.shoot();
                ChargeTimer = 0;
            }

            if (Input.GetKey(KeyCode.I))
            {
                ChargeTimer++;

                if (ChargeTimer == chargeTime * 1 / 3)
                {
                    playerActionsScript.ShotCount = 1;
                    playerActionsScript.Charging = true;
                }
                if (ChargeTimer == chargeTime * 2 / 3)
                {
                    playerActionsScript.ShotCount = 2;
                }
                if (ChargeTimer == chargeTime * 3 / 3)
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
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            HasPressedJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerActionsScript.CanExit || playerActionsScript.CanRestart)
            {
                playerActionsScript.VictoryScreen.SetActive(false);
                playerActionsScript.DeathScreen.SetActive(false);
                Gamemanager.RestartGame();
            }           
        }              

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerActionsScript.CanExit)
            {
                playerActionsScript.VictoryScreen.SetActive(false);
                Gamemanager.LastLevel++;               
                Gamemanager.SavePlayer(playerActionsScript);
                Gamemanager.ExitLevel();
            }
            else if (playerActionsScript.CanUnpause)
            {
                StartCoroutine(playerActionsScript.Unpause());
            }
            else if (playerActionsScript.Health < 1 && playerActionsScript.CanRestart)
            {
                playerActionsScript.DeathScreen.SetActive(false);
                Gamemanager.ExitLevel();
                Gamemanager.SavePlayer(playerActionsScript);
            }
            else if (playerActionsScript.Health > 0 && !playerActionsScript.CanUnpause)
            {
                StartCoroutine(playerActionsScript.Pause());
            }            
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (playerActionsScript.CanUnpause)
            {
                playerActionsScript.PauseScreen.SetActive(false);
                Gamemanager.ExitLevel();
                Gamemanager.SavePlayer(playerActionsScript);
                Audiomanager.StopMusic();
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
