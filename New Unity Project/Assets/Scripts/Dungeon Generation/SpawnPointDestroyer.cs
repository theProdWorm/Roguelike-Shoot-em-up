using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointDestroyer : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
