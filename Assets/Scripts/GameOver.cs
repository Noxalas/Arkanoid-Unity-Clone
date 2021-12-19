using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TMP_Text>().text = "You Lost! <br> Press 'R' to try again! <br> Score: " + GameManager.Score;
    }
}
