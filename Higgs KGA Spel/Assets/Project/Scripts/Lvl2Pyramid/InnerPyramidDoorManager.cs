using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerPyramidDoorManager : MonoBehaviour
{
    [SerializeField] public GameObject[] Doors;

    public void OpenDoor(int door)
    {
        Doors[door].GetComponent<InnerPyramidDoorScript>().Open = true;
    }

    public void InvertDoorStates()
    {
        foreach (GameObject door in Doors)
        {
            door.GetComponent<InnerPyramidDoorScript>().Open = !door.GetComponent<InnerPyramidDoorScript>().Open;
        }
    }

    public void InvertPillarDoors()
    {
        Doors[6].GetComponent<InnerPyramidDoorScript>().Open = !Doors[6].GetComponent<InnerPyramidDoorScript>().Open;
        Doors[7].GetComponent<InnerPyramidDoorScript>().Open = !Doors[7].GetComponent<InnerPyramidDoorScript>().Open;
        Doors[9].GetComponent<InnerPyramidDoorScript>().Open = !Doors[9].GetComponent<InnerPyramidDoorScript>().Open;
        Doors[10].GetComponent<InnerPyramidDoorScript>().Open = !Doors[10].GetComponent<InnerPyramidDoorScript>().Open;
    }
}
