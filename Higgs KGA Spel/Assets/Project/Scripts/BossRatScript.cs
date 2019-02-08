using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRatScript : MonoBehaviour {

    private PolygonCollider2D bulletCollider;
    private GameObject bullet;
    private GameObject player;
    private Rigidbody2D RatRB;

    private int health = 100;
    private int attackType;
    private int attackTimer = 0;
    private float movementSpeed = 100;



    //Bools:
    public bool IsFacingRight = true;
    private bool attacking = false;

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            Debug.Log(health);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = GameObject.FindGameObjectWithTag("Bullet");

        RatRB = gameObject.GetComponent<Rigidbody2D>();

        attack();
    }

    // Update is called once per frame
    void Update ()
    {
        attackTimer++;

		if (gameObject.transform.position.x < player.transform.position.x && !IsFacingRight)
        {
            FlipRat();
        }
        else if (gameObject.transform.position.x > player.transform.position.x && IsFacingRight)
        {
            
            FlipRat();
        }

        if(health < 1)
        {
            Destroy(gameObject);
        }

        if(attackTimer > 100)
        {
            attack();
            attackTimer = 0;
        }
    }

    private void FlipRat()
    {
        IsFacingRight = !IsFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void attack()
    {
        Debug.Log("attack");

        attackType = Random.Range(0, 3);

        if (attackType == 1 && !attacking)
        {
            Debug.Log("attackType");

            attacking = true;
            attackTypeOne();
        }
        else if (attackType == 2 && !attacking)
        {
            attacking = true;
            attackTypeTwo();
        }
        else if (attackType == 3 && !attacking)
        {
            attacking = true;
            attackTypeThree();
        }

               
    }

    private void attackTypeOne()
    {
        Debug.Log("attackTypeOne");

        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

        attacking = false;
    }

    private void attackTypeTwo()
    {
        attacking = false;
    }

    private void attackTypeThree()
    {
        attacking = false;
    }
}
