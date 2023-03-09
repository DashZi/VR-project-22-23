using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    public int score = 0;
    public int maxscore;
    public GameObject Door;

    void Start()
    {
        score = 0;
    }

    public void AddPoint(int newscore)
    {
        score += newscore;
    }

    public void Updatescore()
    {
        Debug.Log(ScoreText.gameObject.name);
        ScoreText.text = "Points: " + score;
    }

    void Update()
    {
        Updatescore();
    }

    public void OpentheDoor()
    {
        if (score == 6)
            Door.transform.position = new Vector3(Door.transform.position.x, 4, Door.transform.position.z);
    }
}
