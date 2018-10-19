using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    //To store PlayerInput script and player in a local variable
    private GameObject Player;
    private PlayerMovement PlayerMovementScript;

    //Floats
    [SerializeField] private float bulletspeed;

    public Rigidbody2D ShotRB;
    public Transform Shootingpoint;

	// Use this for initialization
	void Start () {
        Shootingpoint.GetComponent<Transform>();

        ShotRB.GetComponent<Rigidbody2D>();

        Player = GameObject.FindGameObjectWithTag("Player");

        PlayerMovementScript = Player.GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void shoot()
    {
        Rigidbody2D clone;
        clone = Instantiate(ShotRB, Shootingpoint.position, Quaternion.identity) as Rigidbody2D;

        if (PlayerMovementScript.IsFacingRight)
        {
            clone.velocity = new Vector2(bulletspeed, 0);
        }

        if (PlayerMovementScript.IsFacingRight)
        {
            clone.velocity = new Vector2(-bulletspeed, 0);
        }
    }
}
