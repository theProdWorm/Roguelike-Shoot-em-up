using System;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    /*   1 --> Needs top opening
         2 --> Needs right opening
         3 --> Needs bottom opening
         4 --> Needs left opening */

    private RoomTemplates templates;

    private readonly float timeBeforeDestruction = 5.0f;
    private bool spawned;
    private int rand;

    private GameObject room;

    private void Start()
    {
        spawned = false;
        templates = GameObject.Find("Room Templates").GetComponent<RoomTemplates>();

        Destroy(gameObject, timeBeforeDestruction);

        Invoke("Spawn", Time.deltaTime * 20);
    }

    private void Spawn()
    {
        if (!spawned)
        {
            switch (openingDirection)
            {
                case 1:
                    rand = UnityEngine.Random.Range(0, templates.topRooms.Length);
                    room = Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);

                    Invoke("SetOpening", .1f);
                    break;

                case 2:
                    rand = UnityEngine.Random.Range(0, templates.rightRooms.Length);
                    room = Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);

                    Invoke("SetOpening", .1f);
                    break;

                case 3:
                    rand = UnityEngine.Random.Range(0, templates.bottomRooms.Length);
                    room = Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);

                    Invoke("SetOpening", .1f);
                    break;

                case 4:
                    rand = UnityEngine.Random.Range(0, templates.leftRooms.Length);
                    room = Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);

                    Invoke("SetOpening", .1f);
                    break;

                default:
                    break;
            }

            spawned = true;
        }
    }

    private void SetOpening()
    {
        room.GetComponent<AddRoom>().requiredOpening = openingDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        templates = GameObject.Find("Room Templates").GetComponent<RoomTemplates>();

        if (other.CompareTag("SpawnPoint"))
        {
            if (other.name != "Destroyer")
                if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    Instantiate(templates.closed, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            spawned = true;
        }
    }
}
