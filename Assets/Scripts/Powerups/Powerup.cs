using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Powerup : MonoBehaviour
{
    public bool expiresImmediately;
    public AudioClip soundEffect;
    protected Vaus player;
    protected SpriteRenderer spriteRenderer;
    protected enum state
    {
        Attract,
        Collected,
        Expiring
    }

    protected state powerUpState = state.Attract;
    private Rigidbody2D rb;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        PowerupCollected(other.gameObject);
    }

    protected virtual void PowerupCollected(GameObject collector)
    {
        if (collector.tag != "Player") { return; }

        if (powerUpState == state.Collected || powerUpState == state.Expiring) { return; }

        powerUpState = state.Collected;

        GameManager.UpdateScore(500);

        player = collector.GetComponent<Vaus>();

        PlaySound();

        ApplyPowerupEffect();

        spriteRenderer.enabled = false;
    }

    protected virtual void PlaySound()
    {
        // Play sound
    }

    protected virtual void ApplyPowerupEffect()
    {
        if (expiresImmediately) { PowerupHasExpired(); }
    }

    protected virtual void PowerupHasExpired()
    {
        if (powerUpState == state.Expiring) { return; }

        powerUpState = state.Expiring;

        DestroySelfAfterDelay();
    }

    protected virtual void DestroySelfAfterDelay()
    {
        Destroy(gameObject, 10f);
    }
}
