using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private float startT;
    private string textT;
    private float guiT;
    private int minutes;
    private int seconds;
    private int fraction;
    public Text textfield;

    void start()
    {
        startT = Time.time;
    }

    void update()
    {
        guiT = Time.time - startT;
        minutes = (int)guiT / 60;
        seconds = (int)guiT % 60;
        fraction = ((int)guiT * 100) % 100;
        textT = string.Format("{0:00}:{1:00}", minutes, seconds, fraction);

        if (minutes >= 2)
        {
            textfield.text = "GAME OVER";
        }
        else
        {
            textfield.text = textT;
        }
    }
}
