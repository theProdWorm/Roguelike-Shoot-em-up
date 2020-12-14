using UnityEngine;

public class Nonstop : Enemy
{
    public float speed;
    public float range;

    public Transform player;
    public Animator animator;

    void Update()
    {
        if (PlayerInRange(range))
        {
            // Attempt an attack:
            // Set attack trigger in animator
            // 
        }

        if (PlayerInVision())
        {
            Vector2.MoveTowards(transform.position, player.position, speed);
        }
    }
}
