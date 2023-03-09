using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("the obj is the ingredient");
    }
    
}
