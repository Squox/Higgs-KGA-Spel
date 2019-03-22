using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class CactusScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform leftShootingPoint;
    [SerializeField] private Transform rightShootingPoint;
    [SerializeField] private GameObject cactusDartPrefab;

    private Rigidbody2D cactusDartRb;
    private EnemyController enemyController;

    [SerializeField] private float playerCheckRadius = 21f;

    private float fireRate = 0.5f;
    private float animationDuration = 9f/60f;

    private int health;
    private int shotCounter;
    private int burstBuffer = 2;
    private int animationPause;

    public int DartSpeed = 5;

	// Use this for initialization
	void Start ()
    {
        enemyController = GetComponent<EnemyController>();

        StartCoroutine(shoot());
	}
	
	// Update is called once per frame
	void Update ()
    {
        health = enemyController.Health;
        
        if (enemyController.IsPlayerRight())
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
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
            if (enemyController.IsPlayerInRange(playerCheckRadius))
            {                
                animator.SetBool("Attack", true);

                yield return new WaitForSeconds(animationDuration);

                animator.SetBool("Attack", false);

                if (enemyController.IsPlayerRight())
                {
                    Instantiate(cactusDartPrefab, rightShootingPoint.position, transform.rotation * Quaternion.Euler(0, 0, 180));
                }
                else if (!enemyController.IsPlayerRight())
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
