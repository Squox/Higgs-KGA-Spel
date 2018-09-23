using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //To store PlayerInput script and player in a local variable
    private GameObject Player;
    private PlayerInput PlayerInputScript;


    private Rigidbody2D PlayerRB;

    //Floats:
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce;

    //Ints:
    private int JumpsLeft;
    [SerializeField] private int Jumps;

    //Bools:
    private bool IsFacingRight = true;

    //variables used to check if player is on ground
    private bool IsOnGround;
    [SerializeField] private float CheckRadius;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private Transform GroundCheck;

    private void Start ()
    {
        PlayerRB = FindObjectOfType<Rigidbody2D>();

        Player = GameObject.FindGameObjectWithTag("Player");

        PlayerInputScript = Player.GetComponent<PlayerInput>();
	}

    private void Update()
    {
        CheckFacingDirection();
        if (IsOnGround)
        {
            JumpsLeft = Jumps;
        }
    }

    private void FixedUpdate ()
    {
        // Check if player is on ground
        IsOnGround = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);
        
        // Moving the player on the x axies
        PlayerRB.velocity = new Vector2(PlayerInputScript.MoveDirection * speed * Time.fixedDeltaTime, PlayerRB.velocity.y);

        Jumping();
	}


    private void Jumping()
    {
        if (PlayerInputScript.HasPressedJump == true && JumpsLeft > 0)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, JumpForce * Time.fixedDeltaTime);
            PlayerInputScript.HasPressedJump = false;
            JumpsLeft--;
        }

        if (PlayerInputScript.HasPressedJump == true && IsOnGround)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, JumpForce * Time.fixedDeltaTime);
            PlayerInputScript.HasPressedJump = false;
        }
        else
        {
            PlayerInputScript.HasPressedJump = false;
        }

    }

    private void CheckFacingDirection()
    {
        if (PlayerInputScript.MoveDirection == -1 && IsFacingRight)
        {
            FlipPlayer();
        }else
        if (PlayerInputScript.MoveDirection == 1 && !IsFacingRight)
        {
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        IsFacingRight = !IsFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
