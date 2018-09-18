using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D PlayerRB;

	void Start ()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
    {
		
	}



    public void PlayerMoveRight()
    {
        PlayerRB.velocity = new Vector2(2, PlayerRB.velocity.y);
    }
    public void PlayerMoveLeft()
    {
        PlayerRB.velocity = new Vector2(-2, PlayerRB.velocity.y);
    }

    private void LateUpdate()
    {
        PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y);
    }

}
