using UnityEngine;

public class AddRoom : MonoBehaviour
{
    public int requiredOpening;

    private void Start()
    {
        GameObject.Find("Room Templates").GetComponent<RoomTemplates>().rooms.Add(gameObject, requiredOpening);
    }
}
