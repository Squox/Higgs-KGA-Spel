using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomswitchScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera roomToShow;
    [SerializeField] private CinemachineVirtualCamera[] rooms;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (CinemachineVirtualCamera cam in rooms)
                cam.Priority = 0;

            roomToShow.Priority = 1;           
        }
    }
}
