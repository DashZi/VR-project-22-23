using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighscorecoreText;

    int score = 0;
    int hightscore = 0;
    public GameObject Door;
    
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
    public void OpentheDoor()
    {
        if (score == 6)
            Door.transform.position = new Vector3(Door.transform.position.x, 4, Door.transform.position.z);
    }
}
