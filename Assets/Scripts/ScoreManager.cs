using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int totalScore = 0;
    public TMP_Text scoreText;
    public List<Image> lifeImages;
    public int totalLife = 3;

    [Header("Audio")]
    public AudioClip scoreClip;
    public AudioClip wrongClip;
    public AudioClip loseLifeClip;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void OnCorrectSort(int score)
    {
        totalScore += score;
        UpdateScoreText();

        if(scoreClip != null) AudioManager.Instance.PlaySFX(scoreClip);
    }

    public void OnWrongSort(int score)
    {
        totalScore -= score;
        if (totalScore < 0)
        {
            totalScore = 0;
        }
        UpdateScoreText();

        if(wrongClip != null) AudioManager.Instance.PlaySFX(wrongClip);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore.ToString();
        }
    }

    public void LoseLife()
    {
        totalLife--;

        if(loseLifeClip != null) AudioManager.Instance.PlaySFX(loseLifeClip);

        if (totalLife < 0)
        {
            totalLife = 0;
        }

        UpdateLifeUI();

        if (totalLife == 0)
        {
            GameOver();
        }
    }

    public void UpdateLifeUI()
    {
        for (int i = 0; i < lifeImages.Count; i++)
        {
            lifeImages[i].enabled = i < totalLife;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }

}

