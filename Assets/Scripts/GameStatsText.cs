using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatsText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalScoreText;
    void Start()
    {
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);

        if (totalScoreText != null )
        {
            totalScoreText.text = "Score: " + totalScore.ToString();
        }
    }
}
