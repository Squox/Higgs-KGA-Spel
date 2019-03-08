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
    private float playerCheckRadius = 2f;
    private float jumpForce = 700f;
    private float swimRange = 0.8f;
    private float swimSpeed = 2f;
        
    private int jumpTimer = 0;
    private int jumpPause = 160;
    private int health = 3;

    private bool jumping = false;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        startY = transform.position.y;
        startX = transform.position.x;
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
        else if (transform.position.y == startY)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            swim();
        }

        if (jumping)
        {
            StartCoroutine(pause());
        }

        if (health < 1)
        {
            Destroy(gameObject);
        }

        checkPosition();
        ChangeGravityScale();
	}

    private IEnumerator pause()
    {
        yield return new WaitForSeconds(jumpPause / 60);
        jumping = false;
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

        rb.velocity = new Vector2(swimSpeed, 0);
    }

    private void checkPosition()
    {
        if (startY > transform.position.y)
        {
            transform.position = new Vector2(transform.position.x, startY);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
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
            rb.gravityScale = lowJumpMultiplier;
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
