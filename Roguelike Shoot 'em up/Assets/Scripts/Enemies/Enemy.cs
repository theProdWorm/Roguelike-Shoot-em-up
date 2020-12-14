using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public float collisionDmg;

    private Rigidbody2D rb2D;

    private void Start()
    {
        health = maxHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected void WalkAround()
    {
        // Pick a random direction
        // Pick a random duration
        // Walk in the selected direction for the selected duration
        // Add delay

        // Repeat
    }

    protected bool PlayerInVision()
    {
        // Raycast to see if player is in vision
        // If player is hit by raycast, set duration for follow
        // Returns true if duration is greater than zero
        // Returns false otherwise
        // Add delay - will follow the player if they round corners, but only 

        return true;
    }

    protected bool PlayerInRange(float range)
    {
        // Test the distance between player and enemy
        // Returns true if the player is within the enemy's range
        // Returns false otherwise

        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<Player>().TakeDamage(collisionDmg);
    }
}