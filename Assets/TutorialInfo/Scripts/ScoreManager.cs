using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    private int score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //PlayerPrefs.SetInt("Coins", 0);
        score = PlayerPrefs.GetInt("Coins", 0);
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        PlayerPrefs.SetInt("Coins", score);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Coins", 0);
        
    }
    public void UpdateMenuScore()
    {
        MainMenu.instance.moneyText.text = "Money: " + PlayerPrefs.GetInt("Coins", 0);
    }
}
