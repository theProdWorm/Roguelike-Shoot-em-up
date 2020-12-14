using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float health;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 velocity;

    private void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        animator.SetBool("isRunning", velocity != Vector2.zero);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * speed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}