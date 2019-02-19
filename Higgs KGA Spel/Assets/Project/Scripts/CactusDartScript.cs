using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusDartScript : MonoBehaviour
{
    [SerializeField] private float destroyTime = 10f;
    private float destroy;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();       
        destroy = destroyTime + Time.time;

        rb.transform.localScale = new Vector3(2f, 2f, 1);

        if (transform.rotation.z == 0)
        {
            rb.velocity = new Vector2(-5, 0);
        }
        else if (transform.rotation.z > 0)
        {
            rb.velocity = new Vector2(5, 0);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Cactus")
        {
            Destroy(gameObject);
        }      
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Enemy" && collider.gameObject.tag != "InvulnerableEnemy" && collider.gameObject.tag != "ShootingLimit" && collider.gameObject.tag != "Bullet" && collider.gameObject.tag != "CactusDart")
        {
            Destroy(gameObject);
        }
    }
}
