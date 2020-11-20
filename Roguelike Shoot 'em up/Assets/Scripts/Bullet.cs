using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Instantiate explosion
        // Explosion prefab should handle damage and such

        Destroy(gameObject);
    }
}