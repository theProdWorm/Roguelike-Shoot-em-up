using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Pickup : MonoBehaviour
{
    private GameObject weapon;

#region Trigger checks
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (weapon is null)
                weapon = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        weapon = null;
    }
#endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PickupWeapon();

        if (Input.GetKeyDown(KeyCode.G))
            Drop();
    }

    private void PickupWeapon() 
    {
        if (weapon is null) return;

        Drop();

        // Enable shooting for the weapon
        weapon.GetComponent<Weapon>().enabled = true;

        // Set position and parent to follow the player around
        weapon.transform.SetParent(transform.GetChild(0).transform);
        weapon.transform.position = transform.GetChild(0).transform.position;

        // Tell the program that the weapon has been equipped
        weapon.tag = "EquippedWeapon";
    }

    private void Drop()
    {
        if (transform.GetChild(0).childCount <= 0) return;

        GameObject equippedWeapon = transform.GetChild(0).GetChild(0).gameObject;

        equippedWeapon.GetComponent<Weapon>().enabled = false;

        // Force the player to abandon their child
        equippedWeapon.transform.SetParent(GameObject.Find("Dropped Weapons").transform);

        // Tell the program that the weapon has been dropped
        equippedWeapon.tag = "Weapon";
    }
}
