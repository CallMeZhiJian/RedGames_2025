using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public int scorePerCorrect = 5;
    public int penaltyPerWrong = 3;
    public int scorePerBiggieCorrect = 10;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        Instance = this;
    }

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

    public void OnBiggieCorrectSort()
    {
        score += scorePerBiggieCorrect;
        UpdateScoreText();
    }

    public void OnBiggieWrongSort()
    {

    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.SetText("Score: " + score.ToString());
    }
}

