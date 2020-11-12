using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vollidertest : MonoBehaviour
{
    

    private bool isInRange;

    private void Update()
    {
        if (isInRange == true)
        {
            Debug.Log("i lådan");
        }
        else 
        {
            Debug.Log("inte i lådan");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
    }
}
