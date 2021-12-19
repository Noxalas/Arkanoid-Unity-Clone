using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TMP_Text>().text = "You won! <br> Press 'R' to play again! <br> Score: " + GameManager.Score;
    }
}
