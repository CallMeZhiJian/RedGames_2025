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
    public TMP_Text HUDScoretext;
    public List<Image> lifeImages;
    public int totalLife = 3;
    BoxHolder boxHolder;
    private int bestScore = 0;

    [Header("Audio")]
    public AudioClip scoreClip;
    public AudioClip wrongClip;
    public AudioClip loseLifeClip;

    [Header("WinScreen")]
    public GameObject winScreenPanel;
    public Image newBest;
    public TMP_Text totalScoreText;
    public TMP_Text newBestScoreText;
    public TMP_Text oguScoreText;
    public TMP_Text tappyScoreText;
    public TMP_Text bamScoreText;
    public TMP_Text biggieScoreText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateScoreText();
        scoreMultiplier = 1;
    }

    public void OnCorrectSort(int score)
    {
        totalScore += score;
        UpdateScoreText();

        if (scoreClip != null) AudioManager.Instance.PlaySFX(scoreClip);
    }

    public void OnWrongSort(int score)
    {
        totalScore -= score;
        if (totalScore < 0)
        {
            totalScore = 0;
        }
        UpdateScoreText();

        if (wrongClip != null) AudioManager.Instance.PlaySFX(wrongClip);
    }
    const int INCREASE_PER_SCORES = 25;
    private int scoreMultiplier = 1;

    void UpdateScoreText()
    {
        if ( totalScore > INCREASE_PER_SCORES * scoreMultiplier)
        {
            SpawnObject.Instance.IncreaseSpawnRate(0.05f);
            scoreMultiplier++;
        }

        if (HUDScoretext != null)
        {
            HUDScoretext.text = "Score: " + totalScore.ToString();
        }
    }

    public void LoseLife()
    {
        totalLife--;

        if (loseLifeClip != null) AudioManager.Instance.PlaySFX(loseLifeClip);

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
        winScreenPanel.SetActive(true);

        if (totalScoreText != null)
        {
            totalScoreText.text = "Score: " + totalScore.ToString();
        }
        if (totalScore > bestScore)
        {
            bestScore = totalScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            if (newBest != null)
                newBest.gameObject.SetActive(true);
        }
        else
        {
            if (newBest != null)
            {
                newBest.gameObject.SetActive(false);
            }
        }
        if (newBestScoreText != null)
        {
            newBestScoreText.text = "Best Score: " + bestScore.ToString();
        }
        if (oguScoreText != null)
        {
            oguScoreText.text = BoxHolder.oguCounter.ToString();
        }
        if (tappyScoreText != null)
        {
            tappyScoreText.text = BoxHolder.tappyCounter.ToString();
        }
        if (bamScoreText != null)
        {
            bamScoreText.text = BoxHolder.bamCounter.ToString();
        }
        if (biggieScoreText != null)
        {
            biggieScoreText.text = BoxHolder.biggieCounter.ToString();
        }
    }


}

