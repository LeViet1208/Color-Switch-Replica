using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    private int score = 0;
    private int bestScore = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        UpdateScoreUI();
    }

    public void AddScore()
    {
        score += 1;
        

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }

        PlayerPrefs.SetInt("LastScore", score);
        PlayerPrefs.Save();

        UpdateScoreUI();

        switch (score)
        {
            case 3:
            case 5:
            case 7:
            case 10:
            case 15:
                if (Spawner.instance == null)
                {
                    Debug.Log("Invalid Spawner");
                }
                else
                {
                    Spawner.instance.ExpandObstacle();
                }
                break;
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (bestScoreText != null)
            bestScoreText.text = "Best score: " + bestScore;
    }
}
