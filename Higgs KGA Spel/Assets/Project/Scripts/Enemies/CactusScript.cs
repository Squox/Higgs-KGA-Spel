using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform leftShootingPoint;
    [SerializeField] private Transform rightShootingPoint;
    [SerializeField] private GameObject cactusDartPrefab;

    private GameObject player;
    private Rigidbody2D cactusDartRb;
    private PlayerActions playerActionsScript;

    [SerializeField] private float playerCheckRadius = 21;

    private int shotTimer;
    private int health = 3;
    private int shotCounter;
    private int shotBuffer;
    private int invulnerabilityTimer = 0;
    private int animationPause;
    public int DartSpeed = 5;

    private bool hasBeenHit = false;
    private bool attacking;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isPlayerRight())
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (hasBeenHit)
        {
            invulnerabilityTimer++;
            if (invulnerabilityTimer > 3)
            {
                invulnerabilityTimer = 0;
                hasBeenHit = false;
            }
        }

        shotTimer++;

        if (shotTimer > 21 && shotBuffer < 1 && isPlayerInRange())
        {
            attacking = true;
            animator.SetBool("Attack", attacking);

            animationPause++;

            if (animationPause > 9)
            {
                if (isPlayerRight())
                {
                    Instantiate(cactusDartPrefab, rightShootingPoint.position, transform.rotation * Quaternion.Euler(0, 0, 180));
                }
                else if (!isPlayerRight())
                {
                    Instantiate(cactusDartPrefab, leftShootingPoint.position, transform.rotation);
                }
                animationPause = 0;
                shotTimer = 0;
                shotCounter++;
                attacking = false;
                animator.SetBool("Attack", attacking);
            }            
        }

        if (shotCounter > 2)
        {
            shotBuffer++;
        }

        if (shotBuffer > 120)
        {
            shotBuffer = 0;
            shotCounter = 0;
        }

        if (health < 1)
        {
            Destroy(gameObject);
        }
	}

    private bool isPlayerRight()
    {
        if (player.transform.position.x > transform.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isPlayerInRange()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < playerCheckRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (playerActionsScript.PowerShot)
            {
                health -= 10;
            }
            else if (!playerActionsScript.PowerShot && !hasBeenHit)
            {
                health--;
                hasBeenHit = true;
            }

            playerActionsScript.PowerShot = false;
        }
    }
}
