using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStateInversionOrbScript : MonoBehaviour
{
    [SerializeField] private GameObject doorManager;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "NoColProjectile")
        {
            doorManager.GetComponent<InnerPyramidDoorManager>().InvertDoorStates();
        }
    }
}
