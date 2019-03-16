using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float destroyTime = 10f;
    
    public float BulletSpeed;

    [SerializeField] private int damage = 1;

    [SerializeField] private GameObject hitEffect;

    private Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * BulletSpeed;
        StartCoroutine(destroyBullet());
    }

    private IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "InvulnerableEnemy")
        {
            Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

            if(collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
