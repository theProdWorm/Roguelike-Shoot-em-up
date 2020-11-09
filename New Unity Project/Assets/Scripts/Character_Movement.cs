using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    private Rigidbody2D myRB;

    [SerializeField]
    private float speed;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        
        myRB.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * Time.deltaTime;

    }
}