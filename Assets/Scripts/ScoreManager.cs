using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int scorePerCorrect = 10;
    public int penaltyPerWrong = 5;
    public Text scoreText;

    void Start()
    {
        UpdateScoreText();
    }

    public void OnCorrectSort()
    {
        score += scorePerCorrect;
        UpdateScoreText();
    }

    public void OnWrongSort()
    {
        score -= penaltyPerWrong;
        if (score < 0) score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }
}

