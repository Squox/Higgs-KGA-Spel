using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //To store PlayerActions script and player in a local variable
    private GameObject player;
    private PlayerActions playerActionsScript;
    private Gamemanager GamemanagerScript;
    // Ints:
    public int MoveDirection = 0;
    public int ChargeTimer = 0;

    private int chargeTime = 180;

    // Bools:
    public bool HasPressedJump;

	// Use this for initialization
	private void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
    }

    private void Start()
    {
        GamemanagerScript = FindObjectOfType<Gamemanager>();
    }

    // Update is called once per frame
    private void Update ()
    {
        if (Input.GetKey(KeyCode.G))
        {
            GamemanagerScript.KillPlayer();
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
                playerActionsScript.ShotCount = 1;

                GamemanagerScript.ManageShots();
            }
            if (ChargeTimer == chargeTime * 2/3)
            {
                playerActionsScript.ShotCount = 2;

                GamemanagerScript.ManageShots();
            }
            if (ChargeTimer == chargeTime * 3/3)
            {
                playerActionsScript.ShotCount = 3;

                GamemanagerScript.ManageShots();
            }
        }

        if (Input.GetKeyUp(KeyCode.I) && ChargeTimer > chargeTime)
        {
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

        if (GamemanagerScript.IsDead == false && GamemanagerScript.Paused == false && Input.GetKeyDown(KeyCode.Escape))
        {
            GamemanagerScript.PauseGame();
        }
    }
}
