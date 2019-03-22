using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool attacking = false;

    private float slamSpeed = -8f;
    private float retreatSpeed = 2f;
    private float overlap = .25f;
    private float startY;
    private float attackPause = 22f / 60f;

    private int pauseTimer;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!attacking)
        {
            rb.velocity = new Vector2(0, slamSpeed);
            attacking = true;
        }

        if (attacking && transform.position.y <= startY - transform.localScale.y + 2 * overlap)
        {
            rb.velocity = new Vector2(0, retreatSpeed);
        }

        if (transform.position.y >= startY && attacking)
        {
            transform.position = new Vector2(transform.position.x, startY);

            StartCoroutine(pause());
        }
	}

    private IEnumerator pause()
    {
        yield return new WaitForSeconds(attackPause);
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
