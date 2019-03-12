using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRatScript : MonoBehaviour
{
    [SerializeField] private Transform shootingpoint;
    [SerializeField] private GameObject acidPrefab;

    private Audiomanager audiomanagerScript;
    private BoxCollider2D idleCollider;
    private BoxCollider2D dogedCollider;
    private PolygonCollider2D bulletCollider;
    private GameObject player;
    private GameObject bullet;
    private GameObject ratHealthBar;
    private Rigidbody2D ratRB;
    private Animator animator;
    private Vector2 fallpoint;
    private Transform priorShootingpoint;
    private PlayerActions playerActionScript;

    public int Health = 500;

    private int attackType;
    private int lastAttack;  

    [SerializeField] private float fallMultiplier = 1.7f;
    [SerializeField] private float lowJumpMultiplier = 1.7f;

    private float movementSpeed = 400;
    private float jumpForce = 530f;
    private float timeTillFall = 2f;
    private float fallTime = 2f;
    private float acidX;
    private float acidY;
    private float acidFireFireRate = 0.1f;
    private float attackDelay = 2f;

    //variables used to check if rat is on ground
    private bool isOnGround;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    //Bools:
    public bool IsFacingRight = true;
    public bool AcidShot = false;
    public bool AcidRain = false;
    public bool AcidFire = false;

    private bool jump = false;
    private bool hasBeenHit = false;

    private int invulnerabilityTimer;

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (!hasBeenHit)
            {
                if (playerActionScript.PowerShot)
                {
                    Health -= 100;
                }
                else
                {
                    Health--;
                }
            }

            hasBeenHit = true;

            if (ratHealthBar.transform.localScale.x > 0)
            {
                ratHealthBar.transform.localScale = new Vector3(Health * 7 / 10, ratHealthBar.transform.localScale.y, ratHealthBar.transform.localScale.z);
            }

            playerActionScript.PowerShot = false;
        }
    }

    private void Start()
    {
        Health = 500;

        audiomanagerScript = FindObjectOfType<Audiomanager>();
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        ratHealthBar = GameObject.FindGameObjectWithTag("RatHealthBar");
        animator = gameObject.GetComponent<Animator>();
        playerActionScript = player.GetComponent<PlayerActions>();
        idleCollider = playerActionScript.Idle;
        dogedCollider = playerActionScript.Doged;

        animator.enabled = false;

        ratRB = gameObject.GetComponent<Rigidbody2D>();

        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(idleCollider, GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(dogedCollider, GetComponent<BoxCollider2D>());

        if (hasBeenHit)
        {
            invulnerabilityTimer++;
            if (invulnerabilityTimer > 3)
            {
                hasBeenHit = false;
                invulnerabilityTimer = 0;
            }
        }

        if (ratHealthBar.transform.localScale.x < 0)
        {
            ratHealthBar.transform.localScale = new Vector3(0, ratHealthBar.transform.localScale.y, ratHealthBar.transform.localScale.z);
        }

        if (Health < 1)
        {
            playerActionScript.RatDead = true;
            Destroy(gameObject);
        }       

        if (gameObject.transform.position.x < player.transform.position.x && !IsFacingRight) 
        {
            FlipRat();
        }
        else if (gameObject.transform.position.x > player.transform.position.x && IsFacingRight)
        {
            FlipRat();
        }

        if (isOnGround)
        {
            jump = false;
        }
    }

    private void FixedUpdate()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        ChangeGravityScale();
    }

    private void FlipRat()
    {
        if (!jump)
        {
            IsFacingRight = !IsFacingRight;

            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void ChangeGravityScale()
    {
        if (ratRB.velocity.y < 0f)
        {
            ratRB.gravityScale = fallMultiplier;
        }

        else if (ratRB.velocity.y > 0.1f)
        {
            ratRB.gravityScale = lowJumpMultiplier;
        }

        else
        {
            ratRB.gravityScale = 1f;
        }
    }

    private IEnumerator attack()
    {
        if (!jump)
        {
            ratRB.velocity = new Vector2(0, 0);
        }

        if (attackType == 3)
        {
            yield return new WaitForSeconds(timeTillFall + 1);
        }
        else
        {
            yield return new WaitForSeconds(attackDelay);
        }

        attackType = Random.Range(1, 5);

        if (attackType == 1)
        {
            attackTypeOne();
        }
        else if (attackType == 2)
        {
            StartCoroutine(attackTypeTwo());
        }
        else if (attackType == 3)
        {
            StartCoroutine(attackTypeThree());
        }
        else if (attackType == 4)
        {
            attackTypeFour();
        }        

        StartCoroutine(attack());
    }

    private bool isAcidRain()
    {
        if (AcidRain || AcidFire)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
     
    private void attackTypeOne()
    {
        if (IsFacingRight)
        {
            ratRB.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, 0);
        }
        else if (!IsFacingRight)
        {
            ratRB.velocity = new Vector2(-movementSpeed * Time.fixedDeltaTime, 0);
        }
    }

    private IEnumerator attackTypeTwo()
    {
        AcidShot = true;

        Instantiate(acidPrefab, shootingpoint.position, shootingpoint.rotation);

        yield return new WaitForSeconds(attackDelay);

        AcidShot = false;
    }

    private IEnumerator attackTypeThree()
    {
        AcidFire = true;

        priorShootingpoint = shootingpoint;

        int shotCounter = 0;

        while (shotCounter < 5)
        {
            Instantiate(acidPrefab, priorShootingpoint.position, priorShootingpoint.rotation * Quaternion.Euler(0, 0, 60));

            shotCounter++;

            yield return new WaitForSeconds(acidFireFireRate);
        }

        yield return new WaitForSeconds(timeTillFall);

        AcidFire = false;
        AcidRain = true;

        for (int i = 0; i < 10; i++)
        {
            acidX = Random.Range(0.1f, 5f);
            acidY = Random.Range(4f, 6f);

            if (IsFacingRight)
            {
                fallpoint = priorShootingpoint.position + new Vector3(acidX, acidY, 0);
            }
            else
            {
                fallpoint = priorShootingpoint.position + new Vector3(-acidX, acidY, 0);
            }

            Instantiate(acidPrefab, fallpoint, shootingpoint.rotation * Quaternion.Euler(0, 0, 90));
        }

        yield return null;

        AcidRain = false;
    }

    private void attackTypeFour()
    {
        if (IsFacingRight)
        {
            ratRB.velocity = new Vector2(jumpForce * Time.fixedDeltaTime / 2, jumpForce * Time.fixedDeltaTime);
        }
        else if (!IsFacingRight)
        {
            ratRB.velocity = new Vector2(-jumpForce * Time.fixedDeltaTime / 2, jumpForce * Time.fixedDeltaTime);
        }
         
        if (!jump)
        {
            jump = true;
        }        
    }
}
