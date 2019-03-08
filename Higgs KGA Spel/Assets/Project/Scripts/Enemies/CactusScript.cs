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

    private float fireRate = 0.5f;
    private float animationDuration = 9f/60f;

    private int shotTimer;
    private int health = 3;
    private int shotCounter;
    private int burstBuffer = 2;
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

        StartCoroutine(shoot());
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

        if (health < 1)
        {
            Destroy(gameObject);
        }
	}

    private IEnumerator shoot()
    {
        while (shotCounter < 3)
        {
            if (isPlayerInRange())
            {                
                animator.SetBool("Attack", true);

                yield return new WaitForSeconds(animationDuration);

                animator.SetBool("Attack", false);

                if (isPlayerRight())
                {
                    Instantiate(cactusDartPrefab, rightShootingPoint.position, transform.rotation * Quaternion.Euler(0, 0, 180));
                }
                else if (!isPlayerRight())
                {
                    Instantiate(cactusDartPrefab, leftShootingPoint.position, transform.rotation);
                }               

                shotCounter++;
            }

            yield return new WaitForSeconds(fireRate - animationDuration);
            
        }

        yield return new WaitForSeconds(burstBuffer);

        shotCounter = 0;

        StartCoroutine(shoot());
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
