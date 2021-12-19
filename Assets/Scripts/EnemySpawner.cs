using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public bool isOpen;
    public Vector2 spawnOffset;
    private int enemyQueue;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (!GameManager.Instance.enemySpawners.Contains(gameObject))
        {
            GameManager.Instance.enemySpawners.Add(gameObject);
        }
    }

    private void Update()
    {
        if (isOpen)
        {
            while (enemyQueue > 0)
            {
                Instantiate(enemy, transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0), transform.rotation);
                enemyQueue--;
            }

            if (enemyQueue <= 0)
            {
                isOpen = false;
                _animator.SetBool("Open", isOpen);
            }


        }
    }

    public void SpawnEnemy()
    {
        isOpen = true;
        _animator.SetBool("Open", isOpen);

        enemyQueue++;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.enemySpawners.Contains(gameObject))
        {
            GameManager.Instance.enemySpawners.Remove(gameObject);
        }
    }

}
