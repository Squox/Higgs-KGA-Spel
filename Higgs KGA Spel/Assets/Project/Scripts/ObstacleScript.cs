using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //To store Bullet BoxCollider2D and Bullet in a local variable
    private BoxCollider2D BulletBC;
    private GameObject Bullet;

    // Use this for initialization
    void Start () {
        Bullet = GameObject.FindGameObjectWithTag("Bullet");

        BulletBC = Bullet.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D BulletBC)
    {
        Destroy(gameObject);
    }
}
