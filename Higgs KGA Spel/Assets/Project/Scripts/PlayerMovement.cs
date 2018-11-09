using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //To store PlayerInput script and player in a local variable
    private GameObject Player;
    private DeathWall DeathWallScript;

    private GameObject DeathWall;
    private PlayerInput PlayerInputScript;


    private Rigidbody2D PlayerRB;

    //Floats:
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    //Ints:
    private int JumpsLeft;
    [SerializeField] private int Jumps;

    //Bools:
    public bool IsFacingRight = true;

    //variables used to check if player is on ground
    private bool IsOnGround;
    [SerializeField] private float CheckRadius;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private Transform GroundCheck;

    private void Start ()
    {
        PlayerRB = GetComponent<Rigidbody2D>();

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerInputScript = Player.GetComponent<PlayerInput>();

        DeathWall = GameObject.FindGameObjectWithTag("DeathWall");
        DeathWallScript = DeathWall.GetComponent<DeathWall>();
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

        ChangeGravityScale();

    }


    private void Jumping()
    {
        if (PlayerInputScript.HasPressedJump == true && JumpsLeft > 0)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, JumpForce * Time.fixedDeltaTime);
            PlayerInputScript.HasPressedJump = false;
            JumpsLeft--;
        }

        if (PlayerInputScript.HasPressedJump == true && IsOnGround == true)
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
        if (PlayerInputScript.MoveDirection == -1 && IsFacingRight == true)
        {
            FlipPlayer();
        }else
        if (PlayerInputScript.MoveDirection == 1 && IsFacingRight ==false)
        {
            FlipPlayer();
        }
    }

    private void ChangeGravityScale()
    {
        if (PlayerRB.velocity.y < 0.1f)
        {
            PlayerRB.gravityScale = fallMultiplier;
        }

        else if (PlayerRB.velocity.y > 0.1f && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.W))
        {
            PlayerRB.gravityScale = lowJumpMultiplier;
        }

        else
        {
            PlayerRB.gravityScale = 1f;
        }
    }

    private void FlipPlayer()
    {
        IsFacingRight = !IsFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
