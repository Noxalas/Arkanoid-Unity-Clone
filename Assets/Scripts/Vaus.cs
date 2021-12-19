using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Vaus : MonoBehaviour
{
    public float horizontalSpeed = 150f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        rb.velocity = Vector2.right * moveX * horizontalSpeed;
    }
}
