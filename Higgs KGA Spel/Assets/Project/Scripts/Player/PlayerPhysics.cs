﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private static float fallMultiplier = 2.5f;
    private static float lowJumpMultiplier = 2f;
    private static float moveSpeed = 330f;
    private static float climbSpeed = 150f;
    private static float jumpForce = 330f;
    private static float swimSpeed = 200f;

    public static float MoveDirectionX = 0;
    public static float MoveDirectionY = 0;

    private static bool jumping = false;

    private static Rigidbody2D rb;

    public static Transform PlayerTransform;

    public static bool InWater = false;
    [SerializeField] private float waterCheckRadius;
    [SerializeField] private LayerMask whatIsWater;
    [SerializeField] private Transform waterCheck;

    private static bool isOnGround;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    // Use this for initialization
    void Awake ()
    {
        PlayerTransform = transform;

        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        PlayerTransform = transform;

        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        InWater = Physics2D.OverlapCircle(waterCheck.position, waterCheckRadius, whatIsWater);

        move();
    }

    private void move()
    {
        if (PlayerInput.OnLadder && !jumping)
        {
            if (MoveDirectionY != 0)
                rb.velocity = new Vector2(0, climbSpeed * MoveDirectionY * Time.fixedDeltaTime);             
            else if (MoveDirectionX != 0)
                rb.velocity = new Vector2(climbSpeed * MoveDirectionX * Time.fixedDeltaTime, 0);          
            else 
                rb.velocity = new Vector2(0, 0);
        }
        else
            rb.velocity = new Vector2(MoveDirectionX * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);

        if (MoveDirectionX != 0 && !PlayerInput.OnLadder && !PlayerController.IsDoged && !InWater && !PlayerController.CeilingAbove && isOnGround)
            GetComponent<Animator>().SetBool("Moving", true);
        else
            GetComponent<Animator>().SetBool("Moving", false);
    }

    public static void ChangeGravityScale()
    {
        if (PlayerInput.OnLadder && !jumping)
            rb.gravityScale = 0;
        else if (rb.velocity.y < 0.1f)
        {
            rb.gravityScale = fallMultiplier;
            if (!Input.GetKey(KeyCode.Space))
                jumping = false;
        }           
        else if (rb.velocity.y > 0.1f && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.W))
            rb.gravityScale = lowJumpMultiplier;
        else
            rb.gravityScale = 1f;
    }

    public static void Swim()
    {
        rb.velocity = new Vector2(rb.velocity.x, swimSpeed * Time.fixedDeltaTime);
    }

    public static void Jump()
    {
        if (isOnGround || PlayerInput.OnLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            jumping = true;
        }          
    }

    public static void ChangeSpeedAndJumpForce(bool charging, bool doged)
    {
        if (charging)
        {
            moveSpeed = 250f;
            jumpForce = 250f;

            if (doged || InWater)
            {
                moveSpeed = 160f;
                jumpForce = 160f;
            }
        }
        else if (doged && !InWater)
        {
            if (!charging)
            {
                moveSpeed = 250f;
                jumpForce = 250f;
            }
        }
        else if (InWater)
        {
            moveSpeed = 160f;
            jumpForce = 250f;
        }
        else
        {
            moveSpeed = 330f;
            jumpForce = 330f;
        }
    }

    public static void OrientPlayer()
    {
        if (MoveDirectionX < 0)
        {
            rb.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (MoveDirectionX > 0)
        {
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
