using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]private float destroyTime = 10f;
    private float destroy;
    
    public float BulletSpeed;

    private Rigidbody2D rb;
    private GameObject rat;
    private GameObject level;

    private BossRatScript bossRatScript;
    private LevelScript levelScript;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * BulletSpeed;
        destroy = destroyTime + Time.time;

        level = GameObject.FindGameObjectWithTag("Level");
        levelScript = level.GetComponent<LevelScript>();

        if (levelScript.RatAlive)
        {
            rat = GameObject.FindGameObjectWithTag("Rat");
            bossRatScript = rat.GetComponent<BossRatScript>();

            Physics2D.IgnoreCollision(rat.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
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
        if (collision.gameObject.tag != "Acid")
        {
            Destroy(gameObject);
        }
    }
}
