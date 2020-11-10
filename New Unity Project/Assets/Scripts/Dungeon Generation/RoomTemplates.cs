using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] rightRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;

    public GameObject closed;

    public Dictionary<GameObject, int> rooms = new Dictionary<GameObject, int>(); // Keeps track of all rooms, as well as the direction in which they were required to have an opening

    public float wait = 5.0f;

    private void Update()
    {
        
    }
}
