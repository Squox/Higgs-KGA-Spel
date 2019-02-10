using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRatScript : MonoBehaviour
{
    [SerializeField] private Transform shootingpoint;
    [SerializeField] private GameObject acidPrefab;

    private BoxCollider2D idleCollider;
    private BoxCollider2D dogedCollider;
    private PolygonCollider2D bulletCollider;
    private GameObject bullet;
    private GameObject player;
    private GameObject ratHealthBar;
    private Rigidbody2D ratRB;
    private Animator animator;
    private Vector2 fallpoint;
    private Transform priorShootingpoint;
    private AcidScript acidScript;
    private PlayerActions playerActionScript;

    public int Health = 100;

    private int attackType;
    private int attackTimer = 0;
    private int lastAttack;
    private int attackDelay = 120;

    [SerializeField] private float fallMultiplier = 1.7f;
    [SerializeField] private float lowJumpMultiplier = 1.7f;

    private float movementSpeed = 400;
    private float jumpForce = 530f;
    private float fallTime = 2f;
    private float fall;
    private float acidX;
    private float acidY;

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
    private bool attacking = false;
    private bool jump = false;

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Health--;
            ratHealthBar.transform.localScale = new Vector3(Health * 10, ratHealthBar.transform.localScale.y, ratHealthBar.transform.localScale.z);
        }
    }

    private void Start()
    {      
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        ratHealthBar = GameObject.FindGameObjectWithTag("RatHealthBar");
        animator = gameObject.GetComponent<Animator>();
        acidScript = acidPrefab.GetComponent<AcidScript>();
        playerActionScript = player.GetComponent<PlayerActions>();
        idleCollider = playerActionScript.Idle;
        dogedCollider = playerActionScript.Doged;

        animator.enabled = false;

        ratRB = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(idleCollider, GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(dogedCollider, GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update ()
    {
        attackTimer++;  

		if (gameObject.transform.position.x < player.transform.position.x && !IsFacingRight)
        {
            FlipRat();
        }
        else if (gameObject.transform.position.x > player.transform.position.x && IsFacingRight)
        {
            
            FlipRat();
        }

        if(Health < 1)
        {
            Destroy(gameObject);
        }

        if(attackTimer > attackDelay && !AcidFire)
        {
            attack();
            attackTimer = 0;
            AcidRain = false;
        }

        if(fall < Time.time && AcidFire)
        {
            AcidFire = false;

            if (IsFacingRight)
            {
                for (int i = 0; i < 10; i++)
                {
                    acidX = Random.Range(0.4f, 8f);
                    acidY = Random.Range(4f, 6f);

                    fallpoint = priorShootingpoint.position + new Vector3(acidX, acidY, 0);

                    Instantiate(acidPrefab, fallpoint, shootingpoint.rotation * Quaternion.Euler(0 ,0, 90));
                }
            }
            else if (!IsFacingRight)
            {
                for (int i = 0; i < 10; i++)
                {
                    acidX = Random.Range(1f, 9f);
                    acidY = Random.Range(4f, 6f);

                    fallpoint = priorShootingpoint.position + new Vector3(-acidX, acidY, 0);

                    Instantiate(acidPrefab, fallpoint, shootingpoint.rotation * Quaternion.Euler(0, 0, 90));
                }
            }   
            
            AcidRain = true;
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

    private void attack()
    {
        if (!jump)
        {
            ratRB.velocity = new Vector2(0, 0);
        }

        attackType = Random.Range(1, 5);

        if (attackType == 1 && !attacking)
        {
            lastAttack = attackType;
            attacking = true;
            attackTypeOne();
        }
        else if (attackType == 2 && !attacking)
        {
            lastAttack = attackType;
            attacking = true;
            attackTypeTwo();
        }
        else if (attackType == 3 && !attacking)
        {
            lastAttack = attackType;
            attacking = true;
            attackTypeThree();
        }
        else if (attackType == 4 && !attacking && lastAttack != 4)
        {
            lastAttack = attackType;
            attacking = true;
            attackTypeFour();
        }
    }

    private void attackTypeOne()
    {
        AcidShot = false;

        if (IsFacingRight)
        {
            ratRB.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, 0);
        }
        else if (!IsFacingRight)
        {
            ratRB.velocity = new Vector2(-movementSpeed * Time.fixedDeltaTime, 0);
        }

        attacking = false;
    }

    private void attackTypeTwo()
    {
        Instantiate(acidPrefab, shootingpoint.position, shootingpoint.rotation);

        AcidShot = true;
        attacking = false;
    }

    private void attackTypeThree()
    {
        AcidShot = false;

        if (IsFacingRight)
        {
            for (float i = 0; i < 10; i++)
            {
                Instantiate(acidPrefab, shootingpoint.position + new Vector3(i * i / 10, i * i / 10, 0), shootingpoint.rotation * Quaternion.Euler(0, 0, 45));
            }
        }
        else if (!IsFacingRight)
        {
            for (float i = 0; i < 10; i++)
            {
                Instantiate(acidPrefab, shootingpoint.position - new Vector3(i * i / 10, -i * i / 10, 0), shootingpoint.rotation * Quaternion.Euler(0, 0, 45));
            }
        }

        AcidFire = true;

        priorShootingpoint = shootingpoint;

        fall = fallTime + Time.time;

        attacking = false;
    }

    private void attackTypeFour()
    {
        AcidShot = false;

        if (IsFacingRight)
        {
            ratRB.velocity = new Vector2(jumpForce * Time.fixedDeltaTime / 2, jumpForce * Time.fixedDeltaTime);
        }
        else if (!IsFacingRight)
        {
            ratRB.velocity = new Vector2(-jumpForce * Time.fixedDeltaTime / 2, jumpForce * Time.fixedDeltaTime);
        }
         
        attacking = false;
        if (!jump)
        {
            jump = true;
        }        
    }
}
