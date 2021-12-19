using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;
    private Rigidbody2D rb;
    private SpriteRenderer sRender;

    public AudioClip deathSound;
    public AudioSource _audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sRender = GetComponent<SpriteRenderer>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = deathSound;

        GameManager.Instance.AddEnemy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Death();
        }
    }

    public void Death()
    {
        _audioSource.Play();

        Instantiate(deathEffect, transform.position, transform.rotation);

        GameManager.UpdateScore(200);

        if (GameManager.Instance.enemies.Contains(gameObject)) { GameManager.Instance.enemies.Remove(gameObject); }

        Destroy(gameObject);
    }
}
