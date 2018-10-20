using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    [SerializeField]private GameObject Bulletprefab;
    [SerializeField]private Transform Shootingpoint;

	// Use this for initialization
	void Start ()
    {
          
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void shoot()
    {
        Debug.Log("Shoot");
        Instantiate(Bulletprefab, Shootingpoint.position, Shootingpoint.rotation);
    }
}
