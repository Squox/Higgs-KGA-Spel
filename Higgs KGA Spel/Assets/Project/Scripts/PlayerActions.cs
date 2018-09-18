using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    private GameObject player;
    private PlayerMovement MovePlayer;


	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MovePlayer = player.GetComponent<PlayerMovement>();
	}
	
	void FixedUpdate ()
    {
        GetComponent<PlayerMovement>();
        PlayerInput();       
    }


    public void PlayerInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer.PlayerMoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer.PlayerMoveRight();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKey(KeyCode.S))
        {

        }
        if (Input.GetKey(KeyCode.Space))
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        if (Input.GetKey(KeyCode.Tab))
        {

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

        }
    }


}
