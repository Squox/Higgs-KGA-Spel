using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePhysics : MonoBehaviour
{
    private GameObject floor;
    private BoxCollider2D floorBC;
    private Rigidbody2D interactiveRB;

    // Use this for initialization
    void Start ()
    {
        floor = GameObject.FindGameObjectWithTag("Floor");
        floorBC = floor.GetComponent<BoxCollider2D>();

        interactiveRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D floorBC)
    {
        interactiveRB.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
