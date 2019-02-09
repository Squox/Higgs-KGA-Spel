﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidScript : MonoBehaviour
{
    private float destroyTime = 10f;
    private float destroy;

    public float AcidSpeed;

    private Rigidbody2D acidRB;
    private GameObject rat;
    private BossRatScript bossRatScript;

    private float acidXVelocity;
    private float acidYVelocity;
    private float acidXPosition;

    private bool falling = false;

    // Use this for initialization
    void Start ()
    {
        rat = GameObject.FindGameObjectWithTag("Rat");
        bossRatScript = rat.GetComponent<BossRatScript>();
        acidRB = GetComponent<Rigidbody2D>();    
        
        destroy = destroyTime + Time.time;

        if (bossRatScript.AcidShot)
        {
            acidXVelocity = Random.Range(5f, 6f);
            acidYVelocity = Random.Range(-2.5f, -1f);

            if (bossRatScript.IsFacingRight)
            {
                acidRB.velocity = new Vector2(acidXVelocity, acidYVelocity);
            }

            else if (!bossRatScript.IsFacingRight)
            {
                acidRB.velocity = new Vector2(-acidXVelocity, acidYVelocity);
            }
        }
        else if (bossRatScript.AcidRain)
        {
            acidRB.velocity = new Vector2(0, -2.5f);
        }
        else if (bossRatScript.AcidFire)
        {
            acidXVelocity = 3;
            acidYVelocity = 3;

            if (bossRatScript.IsFacingRight)
            {
                acidRB.velocity = new Vector2(acidXVelocity, acidYVelocity);
            }

            else if (!bossRatScript.IsFacingRight)
            {
                acidRB.velocity = new Vector2(-acidXVelocity, acidYVelocity);
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (destroy < Time.time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Acid" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "ShootingLimit")
        {
            Destroy(gameObject);
        }      
    }
}
