using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        t = enemySpawnDelay;
    }


    [HideInInspector]
    public List<GameObject> blocks;

    [HideInInspector]
    public List<GameObject> energyBalls;

    [HideInInspector]
    public List<GameObject> enemies;

    [HideInInspector]
    public List<GameObject> enemySpawners;

    [HideInInspector]
    public GameObject playerObject;

    public float enemySpawnDelay = 3f;
    public int maxEnemies = 3;

    public const int INIT_LIVES = 2;
    public static int Lives { get; private set; }
    public static int Score { get; private set; }
    public static int Level { get; private set; }

    private float t;

    static GameManager()
    {
        Lives = INIT_LIVES;
        Score = 0;
    }

    private void Start()
    {
        Level = GetLevel();
    }

    public void InitGame()
    {
        Lives = INIT_LIVES;
        Score = 0;
        Level = GetLevel();
    }

    public void AddEnemy(GameObject enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    public void SpawnEnemy()
    {
        if (enemies.Count < maxEnemies)
        {
            Debug.Log("There are" + enemySpawners.Count + "spawners. Spawning...");

            if (enemySpawners.Count == 0) { return; }

            int rand = Random.Range(0, enemySpawners.Count);

            Debug.Log(rand);

            enemySpawners[rand].GetComponent<EnemySpawner>().SpawnEnemy();
        }
    }

    public void AddBall(GameObject curBall)
    {
        if (!energyBalls.Contains(curBall))
        {
            energyBalls.Add(curBall);
        }
    }

    public void RemoveBall(GameObject curBall)
    {
        if (energyBalls.Contains(curBall))
        {
            energyBalls.Remove(curBall);

            if (energyBalls.Count == 0) { LoseLife(); }
        }
    }

    public void ToggleBallSlowDown(float factor)
    {
        if (energyBalls.Count > 0)
        {
            foreach (var ball in energyBalls)
            {
                Ball ballScript = ball.GetComponent<Ball>();

                ballScript.speedDown = !ballScript.speedDown;
                ballScript.speedDownFactor = factor;

                if (ballScript.speedDown) { ballScript.rb.velocity = ballScript.rb.velocity / factor; }
                else { ballScript.rb.velocity *= factor; }
            }
        }
    }

    public void RemoveBlock(GameObject block)
    {
        if (blocks.Contains(block))
        {
            blocks.Remove(block);
        }
    }

    private void Update()
    {
        t -= Time.deltaTime;

        if (t <= 0)
        {
            SpawnEnemy();
            t = enemySpawnDelay;

            Debug.Log("Enemy spawners ticked");
        }

        if (Input.GetKeyDown("r"))
        {
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
                SceneManager.LoadScene("Level1");

                _instance.InitGame();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            foreach (var block in blocks)
            {
                block.GetComponent<Block>().DropPowerup();
                Destroy(block);
            }
        }
    }

    public static int GetLives()
    {
        return Lives;
    }
    public static void GetExtraLife()
    {
        Lives++;

        FindObjectOfType<HealthUI>().UpdateHealth();
    }

    public static void LoseLife()
    {
        Lives--;


        if (Lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.Log("*** GAME OVER ***");
            SceneManager.LoadScene("GameOver");
        }
    }

    public static void UpdateScore(int score)
    {
        Score += score;

        Debug.Log("Score: " + Score);
    }

    private static int GetLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Contains("Level"))
        {
            Level = int.Parse(sceneName.Replace("Level", ""));
        }

        return Level;
    }


}



