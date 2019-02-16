using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform idleShootingpoint;
    [SerializeField] private Transform dogedShootingpoint;

    private Gamemanager GamemanagerScript;
    private Transform currentShootingpoint;

    [SerializeField] public BoxCollider2D Idle;
    [SerializeField] public BoxCollider2D Doged;

    //To store PlayerInput script and player in a local variable
    private GameObject player;
    private PlayerInput playerInputScript;
    private Rigidbody2D playerRB;

    //Floats:   
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    private float speed = 330f;
    private float jumpForce = 330f;

    //Ints:
    [SerializeField] private int fireRate;

    private int jumpsLeft = 0;
    private int invulnerabilityTimer;
    private int burstBuffer = 0;
    private int shotBuffer = 0;
    private int timeSinceShot = 0;

    public int Health = 3;    
    public int Jumps = 1;
    public int ShotCount = 0;

    //Bools:
    [SerializeField] private bool burstFire;

    public bool IsFacingRight = true;
    public bool HasBeenHit = false;
    public bool PowerShot = false;
    public bool Exit = false;
    public bool StandingByDoor = false;

    private bool isDoged = false;
    private bool hasShot = false;

    //variables used to check if player is on ground
    private bool isOnGround;
    [SerializeField] private float checkRadius; 
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerRB = GetComponent<Rigidbody2D>();
        playerInputScript = player.GetComponent<PlayerInput>();
  
        currentShootingpoint = idleShootingpoint;
    }

    private void Start()
    {
        GamemanagerScript = FindObjectOfType<Gamemanager>();
    }

    private void Update()
    {
        if (burstFire)
        {
            GamemanagerScript.ManageShots();
        }

        if( playerInputScript.ChargeTimer > 10)
        {
            speed = 250f;
            jumpForce = 250f;

            if (isDoged)
            {
                speed = 160f;
                jumpForce = 160f;
            }
        }      
        else if (isDoged)
        {
            if(playerInputScript.ChargeTimer < 1)
            {
                speed = 250f;
                jumpForce = 250f;
            }
        }
        else
        {
            speed = 330f;
            jumpForce = 330f;
        }

        CheckFacingDirection();
        if (isOnGround)
        {
            jumpsLeft = Jumps;
        }

        if (HasBeenHit)
        {
            invulnerabilityTimer++;
            if (invulnerabilityTimer == 100)
            {
                HasBeenHit = false;
                invulnerabilityTimer = 0;
            }
        }

        if (Health < 1)
        {
            GamemanagerScript.KillPlayer();
        }

        if(burstFire && ShotCount > 2 && playerInputScript.ChargeTimer < 1)
        {
            burstBuffer++;

            if (burstBuffer > 35)
            {
                ShotCount = 0;
                burstBuffer = 0;              
            }
        }

        if (hasShot)
        {
            timeSinceShot++;

            if (timeSinceShot > 35)
            {
                ShotCount = 0;
                PowerShot = false;
                burstBuffer = 0;

                GamemanagerScript.ManageShots();
            }                               
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


    public void Doge()
    {
        isDoged = !isDoged;

        animator.SetBool("IsDoge", isDoged);

        if (Idle.enabled == false)
        {
            Idle.enabled = true;
            Doged.enabled = false;
            currentShootingpoint = idleShootingpoint;
        }
        else
        {
            Idle.enabled = false;
            Doged.enabled = true;
            currentShootingpoint = dogedShootingpoint;
        }      
    }

    private void Jumping()
    {
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
        }
        else if (playerInputScript.MoveDirection == 1 && IsFacingRight ==false)
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
        if (burstFire && burstBuffer < 1 && timeSinceShot > fireRate || !hasShot)
        {
            Instantiate(bulletprefab, currentShootingpoint.position, currentShootingpoint.rotation);
            ShotCount++;
            timeSinceShot = 0;
            hasShot = true;
        }
        else if (!burstFire && timeSinceShot > fireRate)
        {
            Instantiate(bulletprefab, currentShootingpoint.position, currentShootingpoint.rotation);
            ShotCount++;
            timeSinceShot = 0;           
            hasShot = true;
        }
    }

    public void EmpoweredShot()
    {
        if (burstBuffer < 1 && timeSinceShot > fireRate || !hasShot)
        {
            Instantiate(bulletprefab, currentShootingpoint.position, currentShootingpoint.rotation);
            timeSinceShot = 0;
            hasShot = true;
            PowerShot = true;
        }
    }

    private void FlipPlayer()
    {
        IsFacingRight = !IsFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "MapBorder")
        {
            GamemanagerScript.KillPlayer();
        }
        else if (collider.gameObject.tag == "Trap")
        {
            GamemanagerScript.KillPlayer();
        }
        else if (collider.gameObject.tag == "ExitTrigger")
        {
            Exit = true;
        }
        else if (collider.gameObject.tag == "Acid" && !HasBeenHit)
        {
            Health--;
            HasBeenHit = true;
        }
        else if (collider.gameObject.tag == "CactusDart" && !HasBeenHit)
        {
            Health--;
            HasBeenHit = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            StandingByDoor = true;
        }
        else if (collision.gameObject.tag == "Rat" && !HasBeenHit)
        {
            Health--;
            HasBeenHit = true;
        }
        else if (collision.gameObject.tag == "Cactus" && !HasBeenHit)
        {
            Health--;
            HasBeenHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            StandingByDoor = false;
        }
    }
}
