using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D PlayerRB;

    private PlayerInput PlayerInputScript;

    [SerializeField]private float speed;

	// Use this for initialization
	void Start ()
    {
        PlayerRB = FindObjectOfType<Rigidbody2D>();

        Player = GameObject.FindGameObjectWithTag("Player");

        PlayerInputScript = Player.GetComponent<PlayerInput>();
	}

    private void Update()
    {
        Debug.Log(PlayerRB.velocity.x);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        PlayerRB.velocity = new Vector2(PlayerInputScript.MoveDirection * speed * Time.fixedDeltaTime, PlayerRB.velocity.y);
	}
}
