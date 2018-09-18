using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

	void Start ()
    {
		
	}
	
	void FixedUpdate ()
    {
        PlayerInput();       
    }


    public void PlayerInput()
    {
        if (Input.GetKey(KeyCode.A))
        {

        }
        if (Input.GetKey(KeyCode.D))
        {

        }
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKey(KeyCode.S))
        {

        }
        if (Input.GetKey(KeyCode.Space))
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        if (Input.GetKey(KeyCode.Tab))
        {

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

        }
    }
}
