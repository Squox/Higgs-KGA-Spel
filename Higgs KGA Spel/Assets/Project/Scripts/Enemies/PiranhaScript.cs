using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaScript : MonoBehaviour
{
    [SerializeField] private float fallMultiplier = 1.7f;
    [SerializeField] private float lowJumpMultiplier = 1.7f;

    private GameObject player;
    private Rigidbody2D rb;

    private float startY = 0f;
    private float startX = 0f;
    private float playerCheckRadius = 2.5f;
    private float jumpForce = 700f;
    private float swimRange = 0.8f;
    private float swimSpeed = 2f;
    private float jumpPause = 160f / 60f;

    private int jumpTimer = 0;    
    private int health = 3;

    private bool jumping = false;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        startY = transform.position.y;
        startX = transform.position.x;

        StartCoroutine(jump());
	}
	
	// Update is called once per frame
	void Update ()
    {
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

    private void swim()
    {
        Debug.Log("Hey");

        float currentX = transform.position.x;

        if (currentX > startX + swimRange)
        {
            if (swimSpeed > 0)
            {
                swimSpeed = swimSpeed * -1;
                transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
            }                    
        }
        else if (currentX < startX - swimRange)
        {
            if (swimSpeed < 0)
            {
                swimSpeed = swimSpeed * -1;
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }            
        }

        if (swimSpeed > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
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
        if (collision.gameObject.tag == "Bullet")
        {
            if (player.GetComponent<PlayerActions>().PowerShot)
            {
                health -= 10;
            }
            else
            {
                health--;
            }            
        }
    }
}
