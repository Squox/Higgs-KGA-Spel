using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LightProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb;

    private float speed = 300f;

    private bool delaying = true;  

	// Use this for initialization
	void Start ()
    {
        tag = "Untagged";

        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed * Time.fixedDeltaTime;

        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        else
            rb.velocity = new Vector2(0f, rb.velocity.y);

        StartCoroutine(delay());
	}

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(0.08f);

        tag = "NoColProjectile";

        delaying = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!delaying && collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Glas" && collision.gameObject.tag != "NoColProjectile" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "InvulnerableEnemy" && collision.gameObject.tag != "Interactables")
            Destroy(gameObject);
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
    }
}
