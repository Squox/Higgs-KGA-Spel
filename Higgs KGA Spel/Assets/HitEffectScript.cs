using System.Collections;
using UnityEngine;

public class HitEffectScript : MonoBehaviour
{
	void Start ()
    {
        StartCoroutine(destroy());
	}
	
	private IEnumerator destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
