using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform idleShootingpoint;
    [SerializeField] private Transform dogedShootingpoint;

    private UIManager uiManagerScript;
    private Gamemanager GamemanagerScript;
    private Audiomanager AudiomanagerScript;
    private Transform currentShootingpoint;
    private GameObject deathScreen;
    private GameObject pauseScreen;
    private GameObject victoryScreen;

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
    private float deathScreenFadeTime = 60f;
    private float victoryScreenFadeTime = 40f;
    private float pauseScreenFadeTime = 10f;

    public float LastFadeTime = 0f;

    //Ints:
    [SerializeField] private int fireRate;

    private int jumpsLeft = 0;
    private int invulnerabilityTimer;
    private int burstBuffer = 0;
    private int shotBuffer = 0;
    private int timeSinceShot = 0;
    private int deaths = -1;

    public int Health;    
    public int Jumps = 1;
    public int ShotCount = 0;

    //Bools:
    [SerializeField] private bool burstFire;

    public bool IsFacingRight = true;
    public bool HasBeenHit = false;
    public bool PowerShot = false;
    public bool CanExit = false;
    public bool StandingByDoor = false;
    public bool HasShot = false;
    public bool CanUnpause = false;
    public bool CanRestart = false;
    public bool Charging = false;

    private bool isDoged = false;
    private bool fading = false;

    //variables used to check if player is on ground
    private bool isOnGround;
    [SerializeField] private float checkRadius; 
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        CanExit = false;
        CanUnpause = false;
        CanRestart = false;
        fading = false;

        player = GameObject.FindGameObjectWithTag("Player");

        playerRB = GetComponent<Rigidbody2D>();
        playerInputScript = player.GetComponent<PlayerInput>();
  
        currentShootingpoint = idleShootingpoint;

        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
        victoryScreen = GameObject.FindGameObjectWithTag("VictoryScreen");
    }

    private void Start()
    {
        uiManagerScript = FindObjectOfType<UIManager>();
        GamemanagerScript = FindObjectOfType<Gamemanager>();
        AudiomanagerScript = FindObjectOfType<Audiomanager>();

        AudiomanagerScript.PlayerIsDead = false;
        uiManagerScript.InitializeUI();

        Health = GamemanagerScript.PlayerMaxHealth;        

        Doge();
        Doge();
    }

    private void Update()
    {
        GamemanagerScript.PlayerHealth = Health;
        uiManagerScript.ManageLives(Health);

        checkPlayerState();
        changeSpeedAndJumpForce();
        CheckFacingDirection();
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

    private void changeSpeedAndJumpForce()
    {
        if (playerInputScript.ChargeTimer > 10)
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
            if (playerInputScript.ChargeTimer < 1)
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
    }

    private void checkPlayerState()
    {
        if (invulnerabilityTimer > 0 && Health > 0)
        {
            if (invulnerabilityTimer % 2 == 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (burstFire || PowerShot || Charging)
        {
            uiManagerScript.ManageShots(ShotCount, PowerShot);
        }

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

        if (burstFire && ShotCount > 2 && playerInputScript.ChargeTimer < 1)
        {
            burstBuffer++;

            if (burstBuffer > 35)
            {
                ShotCount = 0;
                burstBuffer = 0;
            }
        }

        if (HasShot)
        {
            timeSinceShot++;

            if (timeSinceShot > 35)
            {
                HasShot = false;
                ShotCount = 0;
                PowerShot = false;
                burstBuffer = 0;

                uiManagerScript.ManageShots(ShotCount, PowerShot);
            }
        }
    }

    public IEnumerator Pause()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(uiManagerScript.FadeIn(pauseScreen.GetComponent<SpriteRenderer>(), pauseScreenFadeTime));

        yield return new WaitForSeconds(pauseScreenFadeTime / 60);

        CanUnpause = true;
    }

    public IEnumerator Unpause()
    {
        CanUnpause = false;

        StartCoroutine(uiManagerScript.FadeOut(pauseScreen.GetComponent<SpriteRenderer>(), pauseScreenFadeTime));

        yield return new WaitForSeconds(pauseScreenFadeTime / 60);

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private IEnumerator die()
    {
        AudiomanagerScript.PlayerIsDead = true;
        GamemanagerScript.PlayerDead = true;

        if (deaths != GamemanagerScript.DeathCounter)
        {
            GamemanagerScript.DeathCounter++;
            deaths = GamemanagerScript.DeathCounter;
        }

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(uiManagerScript.FadeIn(deathScreen.GetComponent<SpriteRenderer>(), deathScreenFadeTime));

        yield return new WaitForSeconds(deathScreenFadeTime / 60);

        CanRestart = true;
    }

    private IEnumerator win()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(uiManagerScript.FadeIn(victoryScreen.GetComponent<SpriteRenderer>(), victoryScreenFadeTime));

        yield return new WaitForSeconds(victoryScreenFadeTime / 60);

        CanExit = true;
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
        if (burstFire && burstBuffer < 1 && timeSinceShot > fireRate || !HasShot)
        {
            Instantiate(bulletprefab, currentShootingpoint.position, currentShootingpoint.rotation);
            ShotCount++;
            timeSinceShot = 0;
            HasShot = true;
        }
        else if (!burstFire && timeSinceShot > fireRate)
        {
            Instantiate(bulletprefab, currentShootingpoint.position, currentShootingpoint.rotation);
            ShotCount++;
            timeSinceShot = 0;           
            HasShot = true;
        }
    }

    public void EmpoweredShot()
    {
        if (burstBuffer < 1 && timeSinceShot > fireRate || !HasShot)
        {
            Instantiate(bulletprefab, currentShootingpoint.position, currentShootingpoint.rotation);
            timeSinceShot = 0;
            HasShot = true;
            PowerShot = true;
        }
    }

    private void FlipPlayer()
    {
        IsFacingRight = !IsFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void takeDamage(int damage)
    {
        Health -= damage;
        HasBeenHit = true;

        if (Health < 1)
        {
            StartCoroutine(die());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Level")
        {
            AudiomanagerScript.PlayerIsDead = true;
            Health = 0;
        }
        else if (collider.gameObject.tag == "ExitTrigger")
        {
            StartCoroutine(win());
        }
        else if (collider.gameObject.tag == "Acid" && !HasBeenHit)
        {
            takeDamage(1);
        }
        else if (collider.gameObject.tag == "CactusDart" && !HasBeenHit)
        {
            takeDamage(1);
        }
        else if (collider.gameObject.tag == "InvulnerableEnemy" && !HasBeenHit)
        {
            takeDamage(1);
        }
        else if (collider.gameObject.tag == "Enemy" && !HasBeenHit)
        {
            takeDamage(1);
        }
        else if (collider.gameObject.tag == "Heart" && Health < 3)
        {
            Health++;
            uiManagerScript.ManageLives(Health);
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
            takeDamage(1);
        }
        else if (collision.gameObject.tag == "Cactus" && !HasBeenHit)
        {
            takeDamage(1);
        }
        else if (collision.gameObject.tag == "Trap" && !HasBeenHit)
        {
            takeDamage(1);
        }
        else if (collision.gameObject.tag == "Water" && !HasBeenHit)
        {
            takeDamage(1);
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
