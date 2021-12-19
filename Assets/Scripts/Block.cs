using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Block : MonoBehaviour
{

    public int health = 1;

    [SerializeField]
    Animator _animator;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_animator != null)
        {
            _animator.SetBool("Hit", true);
        }

        if (health == -1) { return; }

        health--;

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    void StopAnimation()
    {
        _animator.SetBool("Hit", false);
    }
}
