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
                InstantiateLight(270f, collision.gameObject.transform.position);
            else if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0 && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
                InstantiateLight(90f, collision.gameObject.transform.position);
            else if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0 && collision.gameObject.GetComponent<Rigidbody2D>().velocity.x == 0)
                InstantiateLight(0f, collision.gameObject.transform.position);
            else if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0 && collision.gameObject.GetComponent<Rigidbody2D>().velocity.x == 0)
                InstantiateLight(180f, collision.gameObject.transform.position);

            StartCoroutine(delay());
        }
    }

    private void InstantiateLight(float angle, Vector3 position)
    {
        if (transform.rotation.eulerAngles.z == angle)
            Instantiate(LightProjectile, position, Quaternion.Euler(0f, 0f, angle));
        else if (transform.rotation.eulerAngles.z == angle + 90f || (angle + 90f == 360f && transform.rotation.eulerAngles.z == 0f))
            Instantiate(LightProjectile, position, Quaternion.Euler(0f, 0f, angle + 180f));
        else
            Instantiate(LightProjectile, position, Quaternion.Euler(0f, 0f, angle + 90f));
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
