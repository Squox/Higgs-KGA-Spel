using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Ints:
    public int MoveDirection = 0;


	// Use this for initialization
	private void Start ()
    {
		
	}
	
	// Update is called once per frame
	private void Update ()
    {
        CheckPlayerInput();	
	}

   

    private void CheckPlayerInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveDirection = -1;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            MoveDirection = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveDirection = 1;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            MoveDirection = 0;
        }
    }
}
