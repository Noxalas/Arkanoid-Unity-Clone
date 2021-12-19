using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float speed = 100f;
    public bool speedDown = false;

    [HideInInspector]
    public float speedDownFactor = 2f;

    [HideInInspector]
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameManager.Instance.AddBall(gameObject);

        Launch(Vector2.up * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Vaus")
        {
            // Calculate hit factor
            float x = GetHitPosition(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            // Get normalized direction
            Vector2 dir = new Vector2(x, 1).normalized;
            // Set ball's velocity with dir * speed

            float force;

            if (speedDown)
            {
                force = speed / speedDownFactor;
            }
            else
            {
                force = speed;
            }

            Launch(dir * force);
        }
    }

    // Get hit position of ball relative to the racket 
    float GetHitPosition(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    public void Launch(Vector2 dir)
    {
        rb.velocity = dir;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.energyBalls.Contains(gameObject))
        {
            GameManager.Instance.energyBalls.Remove(gameObject);
        }

    }

}
