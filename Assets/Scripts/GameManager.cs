using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public List<GameObject> energyBalls;


    public void AddBall(GameObject curBall)
    {
        if (!energyBalls.Contains(curBall))
        {
            energyBalls.Add(curBall);
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
}



