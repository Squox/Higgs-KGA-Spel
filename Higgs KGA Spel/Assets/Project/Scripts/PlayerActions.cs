using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{


    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform shootingpoint;



    //To store PlayerInput script and player in a local variable
    private GameObject player;

    private PlayerInput playerInputScript;


    private Rigidbody2D playerRB;

    //Floats:
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    //Ints:
    private int jumpsLeft;
    public int Jumps = 1;

    //Bools:
    public bool IsFacingRight = true;

    //variables used to check if player is on ground
    private bool isOnGround;
    [SerializeField] private float checkRadius; 
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerInputScript = player.GetComponent<PlayerInput>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        CheckFacingDirection();
        if (isOnGround)
        {
            jumpsLeft = Jumps;
        }
    }

    private void FixedUpdate ()
    {
        // Check if player is on ground
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
        // Moving the player on the x axies
        playerRB.velocity = new Vector2(playerInputScript.MoveDirection * speed * Time.fixedDeltaTime, playerRB.velocity.y);

        Jumping();

        ChangeGravityScale();

    }


    private void Jumping()
    {
        if (playerInputScript.HasPressedJump == true && jumpsLeft > 0)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce * Time.fixedDeltaTime);
            playerInputScript.HasPressedJump = false;
            jumpsLeft--;
        }

        if (playerInputScript.HasPressedJump == true && isOnGround == true)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce * Time.fixedDeltaTime);
            playerInputScript.HasPressedJump = false;
        }
        else
        {
            playerInputScript.HasPressedJump = false;
        }

    }

    private void CheckFacingDirection()
    {
        if (playerInputScript.MoveDirection == -1 && IsFacingRight == true)
        {
            FlipPlayer();
        }else
        if (playerInputScript.MoveDirection == 1 && IsFacingRight ==false)
        {
            FlipPlayer();
        }
    }

    private void ChangeGravityScale()
    {
        if (playerRB.velocity.y < 0.1f)
        {
            playerRB.gravityScale = fallMultiplier;
        }

        else if (playerRB.velocity.y > 0.1f && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.W))
        {
            playerRB.gravityScale = lowJumpMultiplier;
        }

        else
        {
            playerRB.gravityScale = 1f;
        }
    }

    public void shoot()
    {
        Instantiate(bulletprefab, shootingpoint.position, shootingpoint.rotation);
    }

    private void FlipPlayer()
    {
        IsFacingRight = !IsFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
