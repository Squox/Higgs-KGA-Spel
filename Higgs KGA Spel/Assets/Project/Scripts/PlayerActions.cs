using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    [SerializeField]private GameObject bulletprefab;
    [SerializeField]private Transform shootingpoint;

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
        Instantiate(bulletprefab, shootingpoint.position, shootingpoint.rotation);
    }
}
