using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusDartScript : MonoBehaviour
{
    [SerializeField] private float destroyTime = 10f;
    private float destroy;

    public float DartSpeed;

    private Rigidbody2D rb;
    private GameObject cactus;
    private CactusScript cactusScript;

    // Use this for initialization
    void Start ()
    {
        DartSpeed = 5;
        cactus = GameObject.FindGameObjectWithTag("Cactus");
        cactusScript = cactus.GetComponent<CactusScript>();
        rb = GetComponent<Rigidbody2D>();       
        destroy = destroyTime + Time.time;

        rb.transform.localScale = new Vector3(0.2f, 0.2f, 1);

        if (cactusScript.isPlayerRight())
        {
            Debug.Log("player is right");
            rb.velocity = new Vector2(DartSpeed, 0);
        }
        else if (!cactusScript.isPlayerRight())
        {
            Debug.Log("player is left");
            rb.velocity = new Vector2(-DartSpeed, 0);
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
        if (collision.gameObject.tag != "Cactus" && collision.gameObject.tag != "ShootingLimit")
        {
            Destroy(gameObject);
        }      
    }
}
