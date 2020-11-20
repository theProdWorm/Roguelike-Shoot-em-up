using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth;
    public float health;

    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    void Update()
    {
        
    }

    private void Die(){
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage){ // This no work
        health -= damage;

        if (health <= 0){
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        print(other.gameObject);
    }
}