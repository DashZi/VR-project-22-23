using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingPot : MonoBehaviour
{
    public ScoreManager score;
    public AudioSource AudioSource;
    private bool activated = false;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Im the pot, the ingredient is inside");
        if (other.tag == "ingredient")
        {
            if (!activated)
            {
                activated = true;
                score.AddPoint(1);
                AudioSource.enabled = true;
            }
        }
    }

    
}
