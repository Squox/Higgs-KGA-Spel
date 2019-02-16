using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusScript : MonoBehaviour
{
    [SerializeField] private Transform leftShootingPoint;
    [SerializeField] private Transform rightShootingPoint;
    [SerializeField] private GameObject cactusDartPrefab;

    private GameObject player;

    private int shotTimer;
    private int health = 3;
    private int shotCounter;
    private int shotBuffer;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        shotTimer++;

        if(shotTimer > 30 && shotBuffer < 1)
        {
            if (isPlayerRight())
            {
                Instantiate(cactusDartPrefab, rightShootingPoint.position, transform.rotation * Quaternion.Euler(0, 0, -90));
            }
            else if (!isPlayerRight())
            {
                Instantiate(cactusDartPrefab, leftShootingPoint.position, transform.rotation * Quaternion.Euler(0, 0, 90));
            }

            shotCounter++;
            shotTimer = 0;
        }

        if (shotCounter > 2)
        {
            shotBuffer++;
        }

        if (shotBuffer > 120)
        {
            shotBuffer = 0;
            shotCounter = 0;
        }

        if (health < 1)
        {
            Destroy(gameObject);
        }
	}

    public bool isPlayerRight()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
        }
    }
}
