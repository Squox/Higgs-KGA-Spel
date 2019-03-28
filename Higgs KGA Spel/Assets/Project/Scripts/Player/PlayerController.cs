using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform idleShootingpoint;
    [SerializeField] private Transform dogedShootingpoint;
   
    [SerializeField] public GameObject DeathScreen;
    [SerializeField] public GameObject PauseScreen;
    [SerializeField] public GameObject VictoryScreen;
    [SerializeField] public BoxCollider2D Idle;
    [SerializeField] public BoxCollider2D Doged;

    private Transform currentShootingpoint;

    //Floats:      
    private float deathScreenFadeTime = 60f;   
    private float pauseScreenFadeTime = 10f;

    public float LastFadeTime = 0f;
    public float VictoryScreenFadeTime = 40f;

    //Ints:
    [SerializeField] private int fireRate;
    [SerializeField] private int empoweredShotDamage = 10;
    [SerializeField] private int simpleShotDamage = 1;

    private int invulnerabilityTimer = 0;
    private int burstBuffer = 0;
    private int timeSinceShot = 0;
    private int deaths = -1;

    public static int Health;    
    public static int ShotCount = 0;
    public static int ShotDamage;

    //Bools:
    [SerializeField] private bool burstFire;

    public static bool HasBeenHit = false;
    public static bool PowerShot = false;
    public static bool CanExit = false;
    public static bool StandingByDoor = false;
    public static bool HasShot = false;
    public static bool CanUnpause = false;
    public static bool CanRestart = false;
    public static bool RatDead = false;
    public static bool IsDoged = false;

    private bool isDead = false;
    private bool isWinning = false;

    //variables used to check if player is underneath ceiling
    private bool ceilingAbove;
    [SerializeField] private float ceilingCheckRadius;
    [SerializeField] private LayerMask whatIsCeiling;
    [SerializeField] private Transform ceilingCheck;

    //variables used to check if player is by wall
    private bool byWall;
    [SerializeField] private float wallCheckRadius;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private Transform wallCheck;

    private void Awake()
    {
        CanExit = false;
        CanUnpause = false;
        CanRestart = false;
  
        currentShootingpoint = idleShootingpoint;
    }

    private void Start()
    {
        UIManager.InitializeUI();

        Health = Gamemanager.PlayerMaxHealth;
        invulnerabilityTimer = 0;
        HasBeenHit = false;
    }

    private void Update()
    {
        Gamemanager.PlayerHealth = Health;
        UIManager.ManageLives(Health);

        checkPlayerState();
        CheckDoge();
    }

    private void FixedUpdate ()
    {
        ceilingAbove = Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, whatIsCeiling);
        byWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        PlayerPhysics.OrientPlayer();
        PlayerPhysics.ChangeGravityScale();
        PlayerPhysics.ChangeSpeedAndJumpForce(PowerShot, !Idle.enabled);
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

        if (burstFire && !PowerShot)
        {
            UIManager.ManageShots(ShotCount);
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

        if (burstFire && ShotCount > 2 && !PowerShot)
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
                burstBuffer = 0;

                UIManager.ManageShots(ShotCount);
            }
        }
    }

    public void DefeatBoss()
    {
        StartCoroutine(Audiomanager.FadeOut(VictoryScreenFadeTime));
    }

    public IEnumerator Pause()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(UIManager.FadeIn(PauseScreen.GetComponent<SpriteRenderer>(), pauseScreenFadeTime));

        yield return new WaitForSeconds(pauseScreenFadeTime / 60);

        CanUnpause = true;
    }

    public IEnumerator Unpause()
    {
        CanUnpause = false;

        StartCoroutine(UIManager.FadeOut(PauseScreen.GetComponent<SpriteRenderer>(), pauseScreenFadeTime));

        yield return new WaitForSeconds(pauseScreenFadeTime / 60);

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private IEnumerator die()
    {
        Gamemanager.PlayerDead = true;

        if (deaths != Gamemanager.DeathCounter)
        {
            Gamemanager.DeathCounter++;
            deaths = Gamemanager.DeathCounter;
        }

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(Audiomanager.FadeOut(deathScreenFadeTime));
        StartCoroutine(UIManager.FadeIn(DeathScreen.GetComponent<SpriteRenderer>(), deathScreenFadeTime));

        yield return new WaitForSeconds(deathScreenFadeTime / 60);

        CanRestart = true;
    }

    private IEnumerator win()
    {
        isWinning = true;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        
        StartCoroutine(UIManager.FadeIn(VictoryScreen.GetComponent<SpriteRenderer>(), VictoryScreenFadeTime));

        yield return new WaitForSeconds(VictoryScreenFadeTime / 60);

        CanExit = true;
    }

    public void CheckDoge()
    {
        if (IsDoged)
        {
            float currentX = transform.position.x;
            Idle.enabled = false;
            Doged.enabled = true;
            currentShootingpoint = dogedShootingpoint;
            animator.SetBool("IsDoge", true);
        }
        else if (!ceilingAbove)
        {
            float currentX = transform.position.x;
            Idle.enabled = true;
            Doged.enabled = false;
            currentShootingpoint = idleShootingpoint;
            animator.SetBool("IsDoge", false);
        }     
    }

    public IEnumerator Shoot()
    {
        if (byWall)
            yield break;

        if (burstFire && burstBuffer < 1 && timeSinceShot > fireRate || !HasShot)
        {
            Instantiate(bulletprefab, currentShootingpoint.position, currentShootingpoint.rotation);
            ShotCount++;
            timeSinceShot = 0;
            HasShot = true;
            ShotDamage = simpleShotDamage;
        }
        else if (!burstFire && timeSinceShot > fireRate)
        {
            Instantiate(bulletprefab, currentShootingpoint.position, currentShootingpoint.rotation);
            ShotCount++;
            timeSinceShot = 0;           
            HasShot = true;
            ShotDamage = simpleShotDamage;
        }

        yield return new WaitForSeconds(fireRate / 60);

        ShotDamage = 0;
    }

    public IEnumerator EmpoweredShot(float chargeTime)
    {
        if (byWall)
            yield break;

        yield return new WaitForSeconds(chargeTime * 1 / 9);

        ShotCount = 0;
        PowerShot = true;

        while (ShotCount < 3)
        {
            yield return new WaitForSeconds(chargeTime * 1f / 3f);
            ShotCount++;
            UIManager.ManageShots(ShotCount);
        }

        yield return new WaitUntil(PlayerInput.FireEmpowered);

        Instantiate(bulletprefab, currentShootingpoint.position, currentShootingpoint.rotation);
        timeSinceShot = 0;
        HasShot = true;
        ShotDamage = empoweredShotDamage;

        yield return new WaitForSeconds(fireRate / 60);

        ShotDamage = 0;
        PowerShot = false;
    }   

    public void TakeDamage(int damage = 1)
    {
        if (!HasBeenHit && Health > 0)
        {
            Health -= damage;
            HasBeenHit = true;
        }     

        if (Health <= 0 && !isDead)
        {
            StartCoroutine(die());
            isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Level")
        {
            Health = 0;
        }
        else if (collider.gameObject.tag == "ExitTrigger" && !isWinning)
        {
            StartCoroutine(win());
        }
        else if (collider.gameObject.tag == "Heart" && Health < Gamemanager.PlayerMaxHealth)
        {
            Health++;
            UIManager.ManageLives(Health);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            StandingByDoor = true;
        }
        else if(collision.gameObject.tag == "InvulnerableEnemy")
        {
            TakeDamage();
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
