using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Block : MonoBehaviour
{

    public int health = 1;
    public int score = 100;

    public GameObject[] powerupDrops;

    [SerializeField]
    Animator _animator;

    private void Start()
    {
        if (health > 0) { GameManager.Instance.blocks.Add(gameObject); }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_animator != null)
        {
            _animator.SetBool("Hit", true);
        }

        if (collision.gameObject.tag != "Enemy")
        {
            if (health == -1) { return; }

            health--;

            if (health == 0)
            {
                GameManager.UpdateScore(score);
                GameManager.Instance.RemoveBlock(gameObject);
                if (powerupDrops[0] != null) { DropPowerup(); }
                Destroy(gameObject);
            }
        }
    }

    public void DropPowerup()
    {
        float dropRate = .11f;

        if (Random.Range(0f, 1f) <= dropRate)
        {
            int rand2 = Random.Range(0, 6);
            print(rand2);
            Instantiate(powerupDrops[rand2], transform.position, transform.rotation);
        }
    }

    void StopAnimation()
    {
        _animator.SetBool("Hit", false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveBlock(gameObject);
    }
}
