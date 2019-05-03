using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzleScript : MonoBehaviour
{
    [SerializeField] private LineRenderer line1;
    [SerializeField] private LineRenderer line2;

    [SerializeField] private GameObject door;

    private Transform line1Tf;
    private Transform line2Tf;

    private bool line1Connected = false;
    private bool line2Connected = false;

	void Update ()
    {
        Vector3[] line1Positions = new Vector3[line1.positionCount];
        Vector3[] line2Positions = new Vector3[line2.positionCount];

        line1.GetPositions(line1Positions);
        line2.GetPositions(line2Positions);

		foreach (Vector3 pos in line1Positions)
        {
            if (Utility.IsInRange(pos, door.transform.position, 0.6f, 1f))
            {
                line1Connected = true;
                break;
            }
            else
                line1Connected = false;
                
        }
        foreach (Vector3 pos in line2Positions)
        {
            if (Utility.IsInRange(pos, door.transform.position, 0.6f, 1f))
            {
                line2Connected = true;
                break;
            }
            else
                line2Connected = false;
                
        }

        if (line1Connected && line2Connected)
            openDoor();
	}

    private void openDoor()
    {
        door.GetComponent<InnerPyramidDoorScript>().Open = true;
    }
}