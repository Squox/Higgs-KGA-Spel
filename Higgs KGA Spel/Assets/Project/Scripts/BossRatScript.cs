using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRatScript : MonoBehaviour
{
    [SerializeField] private Transform shootingpoint;
    [SerializeField] private GameObject acidPrefab;

    private PolygonCollider2D bulletCollider;
    private GameObject bullet;
    private GameObject player;
    private Rigidbody2D RatRB;
    private Animator animator;
    private Vector2 fallpoint;
    private Transform priorShootingpoint;

    private AcidScript acidScript;

    private int health = 50;
    private int attackType;
    private int attackTimer = 0;

    private float movementSpeed = 100;
    private float fallTime = 2f;
    private float fall;
    private float acidX;
    private float acidY;

    

    //Bools:
    public bool IsFacingRight = true;
    public bool AcidShot = false;
    public bool AcidRain = false;
    public bool AcidFire = false;
    private bool attacking = false;

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        animator = gameObject.GetComponent<Animator>();
        acidScript = acidPrefab.GetComponent<AcidScript>();

        animator.enabled = false;

        RatRB = gameObject.GetComponent<Rigidbody2D>();
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

        if(health < 1)
        {
            Destroy(gameObject);
        }

        if(attackTimer > 60 && !AcidFire)
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
    }

    private void FlipRat()
    {
        IsFacingRight = !IsFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void attack()
    {
        attackType = Random.Range(1, 4);

        if (attackType == 1 && !attacking)
        {
            attacking = true;
            attackTypeOne();
        }
        else if (attackType == 2 && !attacking)
        {
            attacking = true;
            attackTypeTwo();
        }
        else if (attackType == 3 && !attacking)
        {
            attacking = true;
            attackTypeThree();
        }             
    }

    private void attackTypeOne()
    {
        AcidShot = false;

        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

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
            for (int i = 0; i < 10; i++)
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
}
