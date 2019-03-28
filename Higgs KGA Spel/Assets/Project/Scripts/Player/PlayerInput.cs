using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //To store PlayerActions script and player in a local variable
    private GameObject player;
    private PlayerController playerController;

    private Coroutine powerShot;

    // Ints:
    public float MoveDirection = 0;
    public int ChargeTimer = 0;

    private float chargeTime = 3f;

    public static bool Interact;
    public static bool OnLadder;

    // Use this for initialization
    private void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        GetComponent<PlayerController>().enabled = true;
    }

    // Update is called once per frame
    private void Update ()
    {
        CheckPlayerInput();
    }

    private void CheckPlayerInput()
    {
        PlayerPhysics.MoveDirectionX = 0;
        PlayerPhysics.MoveDirectionY = 0;

        if (PlayerController.Health > 0 && !PlayerController.CanExit)
        {
            PlayerPhysics.MoveDirectionX = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                PlayerController.IsDoged = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                PlayerController.IsDoged = false;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {               
                powerShot = StartCoroutine(playerController.EmpoweredShot(chargeTime));
            }

            if (Input.GetKeyUp(KeyCode.I) && !FireEmpowered())
            {
                if (powerShot != null)
                {
                    PlayerController.PowerShot = false;
                    PlayerController.ShotCount = 0;
                    StopCoroutine(powerShot);
                }
                
                StartCoroutine(playerController.Shoot());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPhysics.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerController.CanExit || PlayerController.CanRestart)
            {
                playerController.VictoryScreen.SetActive(false);
                playerController.DeathScreen.SetActive(false);
                Gamemanager.RestartGame();
            }           
        }              

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerController.CanExit)
            {
                playerController.VictoryScreen.SetActive(false);
                Gamemanager.LastLevel++;               
                Gamemanager.SavePlayer(playerController);
                Gamemanager.ExitLevel();
            }
            else if (PlayerController.CanUnpause)
            {
                StartCoroutine(playerController.Unpause());
            }
            else if (PlayerController.Health < 1 && PlayerController.CanRestart)
            {
                playerController.DeathScreen.SetActive(false);
                Gamemanager.ExitLevel();
                Gamemanager.SavePlayer(playerController);
            }
            else if (PlayerController.Health > 0 && !PlayerController.CanUnpause)
            {
                StartCoroutine(playerController.Pause());
            }            
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (PlayerController.CanUnpause)
            {
                playerController.PauseScreen.SetActive(false);
                Gamemanager.ExitLevel();
                Gamemanager.SavePlayer(playerController);
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

        if (OnLadder)
        {
            PlayerPhysics.MoveDirectionY = Input.GetAxisRaw("Vertical");
        }
    }

    public static bool FireEmpowered()
    {
        if (!Input.GetKey(KeyCode.I))
        {
            if (PlayerController.ShotCount >= 3)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}
