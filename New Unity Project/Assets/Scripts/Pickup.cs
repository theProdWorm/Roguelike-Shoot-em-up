using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    private GameObject weapon;

    private bool inRange = false; // "Can pick up"
    private bool weaponEquipped = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
            weapon = other.gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
    }

    void PickupWeapon() 
    {
        if (!weaponEquipped)
        {
            // Enable shooting for the weapon
            weapon.GetComponent<Shooting>().enabled = true;

            // Set position and parent to follow the player around
            Transform weaponTrans = weapon.transform;
            weapon.transform.SetParent(weaponTrans);
            weapon.transform.position = weaponTrans.position;

            // Tell the program that the weapon has been equipped
            weaponEquipped = true;
            weapon.tag = "MainWeapon";
        }

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange && weaponEquipped)
            PickupWeapon();

        if (Input.GetKeyDown(KeyCode.G) && weaponEquipped)
            Drop();

    }

    void Drop()
    {
        // Disable shooting for the weapon
        weapon.GetComponent<Shooting>().enabled = false;

        // Force the player to abandon their child
        weapon.transform.SetParent(null);

        // Tell the program that the weapon has been drop
        weaponEquipped = false;
        weapon.tag = "Weapon";
    }
}
