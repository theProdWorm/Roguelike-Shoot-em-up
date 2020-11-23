using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float factor;
    public Transform player;

    void FixedUpdate()
    {
        Vector2 movement = Vector2.Lerp(transform.position, player.position, factor);

        transform.position = new Vector3(movement.x, movement.y, -100);
    }
}
