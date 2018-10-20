using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]private float DestroyTime = 10f;
    private float Destroy;
    


    public float BulletSpeed;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * BulletSpeed;
        Destroy = DestroyTime + Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Destroy < Time.time) 
        {
        Destroy(gameObject);
        }
	}
}
