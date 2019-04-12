using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarStateInversionOrbScript : MonoBehaviour
{
    [SerializeField] private GameObject doorManager;

    private void turnPillar()
    {
        //Turn Pillar once there is one...
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("Hey");
            doorManager.GetComponent<InnerPyramidDoorManager>().InvertPillarDoors();
            turnPillar();
        }           
    }
}
