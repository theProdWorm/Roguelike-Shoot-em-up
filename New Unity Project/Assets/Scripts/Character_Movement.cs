using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    private Rigidbody2D myRB;
    Vector2 velocity;

    [SerializeField]
    private float speed;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();

        velocity = new Vector2(0, 0);
    }

    void FixedUpdate()
    {
        
        myRB.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        velocity /= 100;

        Vector3 move = new Vector3(velocity.x, velocity.y) * speed * Time.deltaTime;

        transform.position += move;
    }
}