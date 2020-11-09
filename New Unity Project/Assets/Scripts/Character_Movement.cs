using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myRB;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        myRB.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
    }
}