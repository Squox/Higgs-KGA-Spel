using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    private static float fallMultiplier = 2.5f;
    private static float lowJumpMultiplier = 2f;
    private static float speed = 330f;
    private static float jumpForce = 330f;

    public static float MoveDirection = 0;

    private static Rigidbody2D rb;

    private static bool isOnGround;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    // Use this for initialization
    void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rb.velocity = new Vector2(MoveDirection * speed * Time.fixedDeltaTime, rb.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public static void ChangeGravityScale()
    {
        if (rb.velocity.y < 0.1f)
            rb.gravityScale = fallMultiplier;

        else if (rb.velocity.y > 0.1f && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.W))
            rb.gravityScale = lowJumpMultiplier;

        else
            rb.gravityScale = 1f;
    }

    public static void Jump()
    {
        if (isOnGround)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime); 
    }

    public static void ChangeSpeedAndJumpForce(bool charging, bool doged)
    {
        if (charging)
        {
            speed = 250f;
            jumpForce = 250f;

            if (doged)
            {
                speed = 160f;
                jumpForce = 160f;
            }
        }
        else if (doged)
        {
            if (!charging)
            {
                speed = 250f;
                jumpForce = 250f;
            }
        }
        else
        {
            speed = 330f;
            jumpForce = 330f;
        }
    }

    public static void OrientPlayer()
    {
        if (MoveDirection < 0)
        {
            rb.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (MoveDirection > 0)
        {
            rb.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
