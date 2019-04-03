using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;

    private Rigidbody2D rb;

    private int sawSpeed = 7;
    private int spinSpeed = 10;
    private int spinCounter;

    private bool movingRight = true;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(sawSpeed, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        spinCounter += spinSpeed;

        if (!movingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, spinCounter);
        }
        else if (movingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, -spinCounter);
        }        

        if (spinCounter > 360)
        {
            spinCounter = 0;
        }

        checkForBorder();
	}

    private void checkForBorder()
    {
        if (transform.position.x - transform.localScale.x / 2 < leftBorder.position.x)
        {
            transform.position = new Vector3(leftBorder.position.x + transform.localScale.x, transform.position.y, transform.position.z);
            flip();
        }
        else if(transform.position.x + transform.localScale.x / 2 > rightBorder.position.x)
        {
            transform.position = new Vector3(rightBorder.position.x - transform.localScale.x, transform.position.y, transform.position.z);
            flip();
        }
    }

    private void flip()
    {
        rb.velocity = new Vector2(rb.velocity.x * -1, 0);
        Utility.Flip2D(gameObject);
        movingRight = !movingRight;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
