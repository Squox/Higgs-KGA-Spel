using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRatScript : MonoBehaviour {

    private PolygonCollider2D bulletCollider;
    private GameObject bullet;

    int Health = 3;

	// Use this for initialization
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Health--;
            Debug.Log(Health);
        }
       
    }

    // Update is called once per frame
    void Update () {
		
	}
}
