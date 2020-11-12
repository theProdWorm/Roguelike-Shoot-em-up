using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myRB;

    private Animator animator;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();

        animator = transform.GetChild(1).GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        myRB.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * Time.deltaTime;

        animator.SetBool("isRunning", myRB.velocity != Vector2.zero ? true : false);
    }
}