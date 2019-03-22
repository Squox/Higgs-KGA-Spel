using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyController))]
public class PiranhaScript : MonoBehaviour
{
    [SerializeField] private float fallMultiplier = 1.7f;

    private Rigidbody2D rb;
    private EnemyController enemyController;

    private float startY = 0f;
    private float startX = 0f;
    private float playerCheckRadius = 2.5f;
    private float jumpForce = 700f;
    private float swimRange = 0.8f;
    private float swimSpeed = 2f;
    private float jumpPause = 160f / 60f;

    private int health = 3;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyController = GetComponent<EnemyController>();
        startY = transform.position.y;
        startX = transform.position.x;

        StartCoroutine(jump());
	}
	
	// Update is called once per frame
	void Update ()
    {
        health = enemyController.Health;

        if (transform.position.y <= startY)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            swim();
        }

        if (health < 1)
        {
            Destroy(gameObject);
        }

        checkPosition();
        ChangeGravityScale();
	}

    private IEnumerator jump()
    {
        yield return new WaitUntil(isPlayerInRange);

        rb.velocity = new Vector2(0, jumpForce * Time.fixedDeltaTime);

        yield return null;

        rb.velocity = new Vector2(0, rb.velocity.y);

        yield return new WaitForSeconds(jumpPause);

        StartCoroutine(jump());
    }

    private bool isPlayerInRange()
    {
        if (enemyController.IsPlayerInRange(playerCheckRadius))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void swim()
    {
        float currentX = transform.position.x;

        if (currentX > startX + swimRange)
        {
            if (swimSpeed > 0)
            {
                swimSpeed = swimSpeed * -1;
            }                    
        }
        else if (currentX < startX - swimRange)
        {
            if (swimSpeed < 0)
            {
                swimSpeed = swimSpeed * -1;
            }            
        }

        if (swimSpeed > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
        else if (swimSpeed < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }

        rb.velocity = new Vector2(swimSpeed, rb.velocity.y);
    }

    private void checkPosition()
    {
        if (startY > transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, startY);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void ChangeGravityScale()
    {
        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = fallMultiplier;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90);
        }
        else if (rb.velocity.y > 0.1f)
        {
            rb.gravityScale = fallMultiplier;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90);
        }
        else if (rb.position.y <= startY)
        {
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
