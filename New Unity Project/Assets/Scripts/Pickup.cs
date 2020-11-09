using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{   
    string info;
    GameObject Weapon;
    bool inRange = false;
    public Transform Weapon_pos;

    [SerializeField]
    bool weaponEquipped = false;
    GameObject Player;


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        inRange = true;
        if (weaponEquipped == false)
        {
            Weapon = collider2D.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

    void Pickup_w() 
    {   
        info = Weapon.name;
        if (Weapon.tag == "Weapon")
        {
            Weapon.tag = "MainWeapon";
            print("picked up" + info);
            Destroy(Weapon);
            weaponEquipped = true;

            Weapon.GetComponent<Shooting>().enabled = true;
            Weapon = Instantiate(Weapon);
            Weapon.transform.SetParent(Weapon_pos);
            Weapon.transform.position = Weapon_pos.position;
        }  
    
    }

    public void Update()
    {
        if (inRange == true && Input.GetKeyDown(KeyCode.E) && weaponEquipped == false)
        {
            Pickup_w();
        }

        if (Input.GetKeyDown(KeyCode.G) == true && weaponEquipped == true)
        {
            Drop();

        }

    }

    void Drop() 
    {
            print("dropped" + info);
            weaponEquipped = false;
        Weapon.tag = "Weapon";

            Weapon.GetComponent<Shooting>().enabled = false;
            Weapon.GetComponent<BoxCollider2D>().enabled = true;
            Weapon.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;

        Weapon.transform.SetParent(null);
        
    }

}
