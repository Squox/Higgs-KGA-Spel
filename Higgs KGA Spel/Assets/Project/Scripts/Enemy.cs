using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //To store Bullet BoxCollider2D and Bullet in a local variable
    private BoxCollider2D BulletBC;
    private GameObject Bullet;

    //Integers
    [SerializeField] private int health;

	// Use this for initialization
	void Start () {
        Bullet = GameObject.FindGameObjectWithTag("Bullet");

        BulletBC = Bullet.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (health == 0)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D BulletBC)
    {
        health--;
    }
}
