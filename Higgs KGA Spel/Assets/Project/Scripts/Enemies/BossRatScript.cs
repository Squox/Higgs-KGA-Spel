using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyController))]
public class BossRatScript : MonoBehaviour
{
    [SerializeField] private GameObject ratHealthBar;
    [SerializeField] private BoxCollider2D idleCol;
    [SerializeField] private BoxCollider2D chargedCol;

    private BoxCollider2D idleCollider;
    private BoxCollider2D dogedCollider;
    private GameObject player;
    private GameObject level;
    private Rigidbody2D ratRB;
    private Animator animator;
    private PlayerController playerActionScript;
    private EnemyController enemyController;
    private Level1Script level1Script;

    private int health;
    private int maxHealth;

    [SerializeField] private float fallMultiplier = 1.7f;
    [SerializeField] private float lowJumpMultiplier = 1.7f;
    [SerializeField] private float dashSpeed = 1000f;
    [SerializeField] private float jumpForce = 530f;
   
    private float startScaleX;

    //variables used to check if rat is on ground
    private bool isOnGround;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    //Bools:
    public bool IsFacingRight = true;     

    private bool jump = false;
    private bool charging = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ratHealthBar = GameObject.FindGameObjectWithTag("RatHealthBar");
        animator = gameObject.GetComponent<Animator>();
        playerActionScript = player.GetComponent<PlayerController>();
        enemyController = GetComponent<EnemyController>();
        level = GameObject.FindGameObjectWithTag("Level");
        level1Script = level.GetComponent<Level1Script>();
        idleCollider = playerActionScript.Idle;
        dogedCollider = playerActionScript.Doged;
        ratRB = gameObject.GetComponent<Rigidbody2D>();

        maxHealth = enemyController.MaxHealth;
        startScaleX = ratHealthBar.transform.localScale.x;

        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        health = enemyController.Health;

        Physics2D.IgnoreCollision(idleCollider, GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(dogedCollider, GetComponent<BoxCollider2D>());

        if (ratHealthBar.transform.localScale.x <= 0)
        {
            ratHealthBar.transform.localScale = new Vector3(0, ratHealthBar.transform.localScale.y, ratHealthBar.transform.localScale.z);
        }
        else if (ratHealthBar.transform.localScale.x > 0)
        {
            ratHealthBar.transform.localScale = new Vector3(startScaleX - startScaleX / maxHealth * (maxHealth - health), ratHealthBar.transform.localScale.y, ratHealthBar.transform.localScale.z);
        }

        if (health < 1)
        {
            level1Script.RatAlive = false;
            PlayerController.DefeatBoss();
            Destroy(gameObject);
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
        updateFacingDirection(charging);
    }

    private void updateFacingDirection(bool _charging)
    {
        if (_charging)
            return;

        if (gameObject.transform.position.x < player.transform.position.x)
        {
            IsFacingRight = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (gameObject.transform.position.x > player.transform.position.x)
        {
            IsFacingRight = false;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
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

    private bool isCharging()
    {
        if (charging)
        {
            chargedCol.enabled = true;
            idleCol.enabled = false;
            return true;
        }
        else
        {
            chargedCol.enabled = false;
            idleCol.enabled = true;
            return false;
        }           
    }

    //------------------------------------------------>>
    #region AttackFunctionality

    [SerializeField] private Transform shootingpoint;
    [SerializeField] private GameObject acidPrefab;

    public bool AcidShot = false;
    public bool AcidRain = false;
    public bool AcidFire = false;

    private int attackType;
    private int lastAttack;

    private float timeTillFall = 2f;
    private float acidFireFireRate = 0.1f;
    private float attackDelay = 2f;

    private IEnumerator attack()
    {
        if (!jump && !charging)
        {
            ratRB.velocity = new Vector2(0, 0);
        }

        if (attackType == 3)
            yield return new WaitForSeconds(timeTillFall + 1);
        else if (attackType == 1)
        {
            yield return new WaitWhile(isCharging);
            yield return new WaitForSeconds(attackDelay);
        }           
        else
            yield return new WaitForSeconds(attackDelay);

        attackType = Random.Range(1, 5);

        if (attackType == 1)
        {
            StartCoroutine(attackTypeOne());
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
     
    private IEnumerator attackTypeOne()
    {
        charging = true;
        animator.SetBool("chargeAttack", true);

        yield return new WaitForSeconds(0.4f);

        if (IsFacingRight)
            ratRB.velocity = new Vector2(dashSpeed * Time.fixedDeltaTime, 0);
        else
            ratRB.velocity = new Vector2(-dashSpeed * Time.fixedDeltaTime, 0);

        yield return new WaitForSeconds(0.4f);

        animator.SetBool("chargeAttack", false);
        charging = false;     
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

        animator.SetBool("TiltNeck", true);

        Transform priorShootingpoint = shootingpoint;

        int shotCounter = 0;

        while (shotCounter < 5)
        {
            Instantiate(acidPrefab, priorShootingpoint.position, priorShootingpoint.rotation * Quaternion.Euler(0, 0, 60));

            shotCounter++;

            yield return new WaitForSeconds(acidFireFireRate);
        }

        animator.SetBool("TiltNeck", false);

        yield return new WaitForSeconds(timeTillFall);

        AcidFire = false;
        AcidRain = true;      

        for (int i = 0; i < 10; i++)
        {
            Vector3 acidPos = new Vector3(Random.Range(0.1f, 5f), Random.Range(4f, 6f), 0);
            Vector2 fallpoint;

            if (IsFacingRight)
            {
                fallpoint = priorShootingpoint.position + acidPos;
            }
            else
            {
                fallpoint = priorShootingpoint.position + new Vector3(-acidPos.x, acidPos.y, 0);
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

    #endregion
    //------------------------------------------------<<

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
