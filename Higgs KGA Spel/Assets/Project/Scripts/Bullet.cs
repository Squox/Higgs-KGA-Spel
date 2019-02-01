using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]private float destroyTime = 10f;
    private float destroy;
    
    public float BulletSpeed;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * BulletSpeed;
        destroy = destroyTime + Time.time;
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
        Destroy(gameObject);
    }
}
