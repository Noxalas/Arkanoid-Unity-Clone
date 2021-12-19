using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float speed = 100f;
    public bool speedDown = false;

    public AudioClip vausBounce;
    public AudioClip wallBounce;
    private AudioSource _audioSource;
    private float realSpeed;

    [HideInInspector]
    public float speedDownFactor = 2f;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public bool isActive = false;

    private Vaus vaus;
    private float vausPreviousPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        vaus = FindObjectOfType<Vaus>();

        GameManager.Instance.AddBall(gameObject);

        realSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Vaus")
        {
            _audioSource.clip = vausBounce;
            _audioSource.Play();

            // Calculate hit factor
            float x = GetHitPosition(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            // Get normalized direction
            Vector2 dir = new Vector2(x, 1).normalized;
            // Set ball's velocity with dir * speed

            if (speedDown)
            {
                realSpeed = speed / speedDownFactor;
            }
            else
            {
                realSpeed = speed;
            }

            Launch(dir);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            _audioSource.clip = wallBounce;
            _audioSource.Play();

            ContactPoint2D coll = collision.contacts[0];
            Vector2 collDir = new Vector2(coll.point.x, coll.point.y);

            Vector2 newDir = (rb.position - collDir).normalized;

            rb.velocity = newDir * speed;

            collision.gameObject.GetComponent<Enemy>().Death();
        }
        else
        {
            _audioSource.clip = wallBounce;
            _audioSource.Play();
        }
    }

    // Get hit position of ball relative to the racket 
    float GetHitPosition(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    private void Update()
    {
        if (!isActive)
        {
            this.transform.position = new Vector2(vaus.transform.position.x, this.transform.position.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Launch(Vector2.up);
            }
            else
            {
                vausPreviousPosition = vaus.transform.position.x;
            }
        }
    }

    public void Launch(Vector2 dir)
    {
        float direction = vaus.transform.position.x - vausPreviousPosition;
        float speedX = realSpeed;

        if (isActive) { direction = dir.x; }

        if (direction < 0)
        {
            speedX = -speed;
        }
        else if (direction == 0)
        {
            if (vaus.transform.position.x < 0)
            {
                speedX = -speed;
            }
        }

        rb.velocity = new Vector2(speedX / 2, speed);

        isActive = true;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.energyBalls.Contains(gameObject))
        {
            GameManager.Instance.RemoveBall(gameObject);
        }
    }

}
