using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AcidScript : MonoBehaviour
{
    private float destroyTime = 10f;
    private float acidXPosition;
    public float AcidSpeed;

    private Rigidbody2D acidRB;
    private GameObject rat;
    private BossRatScript bossRatScript;
    private Vector2 acidVel;  

    // Use this for initialization
    void Start ()
    {
        rat = GameObject.FindGameObjectWithTag("Enemy");

        if (rat == null)
            Destroy(gameObject);

        bossRatScript = rat.GetComponent<BossRatScript>();
        acidRB = GetComponent<Rigidbody2D>();      
    }

    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void calculateVelocity()
    {
        if (bossRatScript.AcidShot)
        {
            acidVel.x = Random.Range(5f, 6f);
            acidVel.y = Random.Range(-2.5f, -1f);

            if (bossRatScript.IsFacingRight)
            {
                acidRB.velocity = acidVel;
            }
            else if (!bossRatScript.IsFacingRight)
            {
                acidRB.velocity = new Vector2(-acidVel.x, acidVel.y);
            }
        }
        else if (bossRatScript.AcidRain)
        {
            acidRB.velocity = new Vector2(0, -2.5f);
        }
        else if (bossRatScript.AcidFire)
        {
            acidVel.x = 2;
            acidVel.y = 3;

            if (bossRatScript.IsFacingRight)
            {
                acidRB.velocity = acidVel;
            }

            else if (!bossRatScript.IsFacingRight)
            {
                acidRB.velocity = new Vector2(-acidVel.x, acidVel.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "ShootingLimit" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Interactables")
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            }
            Destroy(gameObject);
        }        
    }
}