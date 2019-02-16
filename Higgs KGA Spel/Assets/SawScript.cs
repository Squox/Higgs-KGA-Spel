using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private int sawSpeed = 7;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(sawSpeed, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SawLeftBorder")
        {
            rb.velocity = new Vector2(sawSpeed, 0);
        }
        else if (collision.gameObject.tag == "SawRightBorder")
        {
            rb.velocity = new Vector2(-sawSpeed, 0);
        }
    }
}
