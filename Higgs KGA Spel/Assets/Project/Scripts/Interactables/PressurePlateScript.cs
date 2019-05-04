using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private float startY;
    private float endY;
    private float pushSpeed = 0.5f;
    private float depth = 0.3f;

    [SerializeField] private bool stayPressed = true;

    public bool Active = false;
    public bool Pressed = false;

	// Use this for initialization
	void Start ()
    {
        Active = false;
        startY = transform.position.y;
        endY = startY - depth;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Active)
        {
            if (transform.position.y > endY)
                rb.velocity = new Vector2(0, -pushSpeed);
            else
            {
                rb.velocity = new Vector2(0, 0);
                Pressed = true;
            }
        }
        else 
        {
            Pressed = false;

            if (transform.position.y < startY)
                rb.velocity = new Vector2(0, pushSpeed);
            else if (transform.position.y > startY)
                transform.position = new Vector2(transform.position.x, startY);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PhysicsObject")
            Active = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "PhysicsObject") && !stayPressed)
            Active = false;
    }
}
