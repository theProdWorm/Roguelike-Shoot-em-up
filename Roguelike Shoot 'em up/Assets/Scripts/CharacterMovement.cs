using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0f;

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
}