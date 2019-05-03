using UnityEngine;
using System.Collections;

public class TriangleRotateScript : MonoBehaviour
{
    [SerializeField] private GameObject LightProjectile;

    [SerializeField] private bool RotateOnHit = true;
    private bool delaying = false;

	void Update () {
        //transform.Rotate(0,0,0.1f);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (delaying)
            return;

        if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "NoColProjectile")
        {
            Destroy(collision.gameObject);

            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x < 0 && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                if (transform.rotation.eulerAngles.z == 270)
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 270f));
                else if (transform.rotation.eulerAngles.z == 0)
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 90f));
                else
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
            else if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0 && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                if (transform.rotation.eulerAngles.z == 90)
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 90f));
                else if (transform.rotation.eulerAngles.z == 180)
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 270f));
                else
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 180f));
            }
            else if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0 && collision.gameObject.GetComponent<Rigidbody2D>().velocity.x == 0)
            {
                Debug.Log(transform.rotation.eulerAngles.z);
                if (transform.rotation.eulerAngles.z == 0)
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));
                else if (transform.rotation.eulerAngles.z == 90)
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 180f));
                else
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 90f));
            }
            else if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0 && collision.gameObject.GetComponent<Rigidbody2D>().velocity.x == 0)
            {
                Debug.Log(transform.rotation.eulerAngles.z);
                if (transform.rotation.eulerAngles.z == 180)
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 180f));
                else if (transform.rotation.eulerAngles.z == 270)
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));
                else
                    Instantiate(LightProjectile, collision.gameObject.transform.position, Quaternion.Euler(0f, 0f, 270f));
            }

            StartCoroutine(delay());
        }
    }

    private IEnumerator delay()
    {
        delaying = true;

        yield return new WaitForSeconds(0.2f);

        if (RotateOnHit)
            transform.Rotate(0f, 0f, 90f);

        delaying = false;
    }
}
