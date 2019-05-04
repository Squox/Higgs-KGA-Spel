using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformRotateOrbScript : MonoBehaviour
{
    [SerializeField] private GameObject plattform;

    private float rotaionSpeed = 60f; //Higher is slower
    private float rotationPause = 3f;

    private bool rotating = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "NoColProjectile") && !rotating)
            StartCoroutine(rotate());
    }

    private IEnumerator rotate()
    {
        int angle1;
        int angle2;

        if (plattform.transform.rotation.eulerAngles.z == 0)
        {
            angle1 = 270;
            angle2 = 180;

            plattform.transform.Rotate(0f, 0f, -90f / rotaionSpeed);
        }
        else
        {
            angle1 = 90;
            angle2 = 0;
        }
            
        rotating = true;

        while(plattform.transform.rotation.eulerAngles.z > angle1)
        {
            if (plattform.transform.rotation.eulerAngles.z - (90f / rotaionSpeed) > angle1)
                plattform.transform.Rotate(0f, 0f, -90f / rotaionSpeed);
            else
            {
                plattform.transform.rotation = Quaternion.Euler(0f, 0f, angle1);
                break;
            }

            Debug.Log(plattform.transform.rotation.eulerAngles.z);
                
            yield return null;
        }

        yield return new WaitForSeconds(rotationPause);

        while (plattform.transform.rotation.eulerAngles.z > angle2)
        {
            if (plattform.transform.rotation.eulerAngles.z - (90f / rotaionSpeed) > angle2)
                plattform.transform.Rotate(0f, 0f, -90f / rotaionSpeed);
            else
            {
                plattform.transform.rotation = Quaternion.Euler(0f, 0f, angle2);
                break;
            }

            yield return null;
        }

        rotating = false;
    }
}
