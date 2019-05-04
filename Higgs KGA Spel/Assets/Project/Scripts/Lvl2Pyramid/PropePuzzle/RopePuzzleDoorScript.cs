using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RopePuzzleDoorScript : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private bool Open;

    [SerializeField] private GameObject pressurePlate;

    private float startY;
    private float openSpeed = 100f;
    private float closeSpeed = 200f;
    private float openDistance = 3f;

    void Awake()
    {
        startY = transform.position.y;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (pressurePlate.GetComponent<PressurePlateScript>().Pressed)
            Open = true;
        else
            Open = false;

        if (Open && transform.position.y < startY + openDistance)
            rb.velocity = new Vector2(0f, openSpeed * Time.fixedDeltaTime);
        else if (!Open && transform.position.y > startY)
            rb.velocity = new Vector2(0f, -closeSpeed * Time.fixedDeltaTime);

        if (transform.position.y > startY + openDistance)
            transform.position = new Vector3(transform.position.x, startY + openDistance, 0f);
        else if (transform.position.y <= startY)
            transform.position = new Vector3(transform.position.x, startY, 0f);
    }
}
