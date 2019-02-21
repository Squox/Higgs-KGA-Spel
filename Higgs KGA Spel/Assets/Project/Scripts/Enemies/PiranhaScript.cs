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
    private float playerCheckRadius = 2f;
    private float jumpForce = 700f;

    private int jumpTimer = 0;
    private int jumpPause = 120;
    private int health = 3;

    private bool jumping = false;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isPlayerInRange() && !jumping)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(0, jumpForce * Time.fixedDeltaTime);
            jumping = true;
        }

        if (jumping)
        {
            jumpTimer++;
            if (jumpTimer > jumpPause)
            {
                jumping = false;
                jumpTimer = 0;
            }
        }

        if (health < 1)
        {
            Destroy(gameObject);
        }

        checkPosition();
        ChangeGravityScale();
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
        if (isPlayerRight() && player.transform.position.x - transform.position.x < playerCheckRadius)
        {
            return true;
        }
        else if (!isPlayerRight() && transform.position.x - player.transform.position.x < playerCheckRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void checkPosition()
    {
        if (startY > rb.position.y)
        {
            rb.position = new Vector2(rb.position.x, startY);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void ChangeGravityScale()
    {
        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0.1f)
        {
            rb.gravityScale = lowJumpMultiplier;
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
