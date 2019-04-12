using UnityEngine;

public class TriangleRotateScript : MonoBehaviour {
	void Update () {
        //transform.Rotate(0,0,0.1f);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
            transform.Rotate(0, 0, 90f);
    }
}
