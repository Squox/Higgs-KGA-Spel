﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
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
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InvulnerableEnemy")
        {
            rb.velocity = new Vector2(rb.velocity.x * -1, 0);
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            movingRight = !movingRight;
        }
    }
}