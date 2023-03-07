using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text ScoreText;
    public Text HighscorecoreText;

    int score = 0;
    int hightscore = 0;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hightscore = PlayerPrefs.GetInt("highscore", 0);
        ScoreText.text = "POINTS: " + score.ToString();
        HighscorecoreText.text = "HIGHTSCORE: " + hightscore.ToString();
    }

    public void AddPoint()
    {
        score += 1;
        ScoreText.text = "POINTS: " + score.ToString();
        if (hightscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }
}
