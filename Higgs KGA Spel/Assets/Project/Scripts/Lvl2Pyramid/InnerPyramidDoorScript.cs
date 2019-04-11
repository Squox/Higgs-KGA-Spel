using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InnerPyramidDoorScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool Open;

    private float startY;
    private float openSpeed = 100f;
    private float openPercent = 0.9f;

	// Use this for initialization
	void Start ()
    {
        startY = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Open && transform.position.y < startY + openPercent * transform.localScale.y)
            rb.velocity = new Vector2(0f, openSpeed * Time.fixedDeltaTime);
        else if (!Open && transform.position.y > startY)
            rb.velocity = new Vector2(0f, -openSpeed * Time.fixedDeltaTime);

        if (transform.position.y > startY + openPercent * transform.localScale.y)
            transform.position = new Vector3(transform.position.x, startY + openPercent * transform.localScale.y, 0f);
        else if (transform.position.y <= startY)
            transform.position = new Vector3(transform.position.x, startY, 0f);
    }
}
