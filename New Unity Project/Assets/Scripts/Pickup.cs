using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform weapon;

    private List<GameObject> weaponsInRange;

    void Start()
    {
        weaponsInRange = new List<GameObject>();
    }

    void Update()
    {
        if (weaponsInRange.Count > 0)
        {
            if (Input.GetKeyDown("e"))
            {
                var weapon = weaponsInRange[0];

                // Set parent
                // Set tag to EquippedWeapon
                // Set current EquippedWeapon to Weapon

                weapon.transform.SetParent(this.weapon);
                weapon.transform.position = this.weapon.position;

                weapon.tag = "EquippedWeapon";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            weaponsInRange.Add(other.gameObject);

            Debug.Log(weaponsInRange);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        weaponsInRange.Remove(other.gameObject);

        Debug.Log(weaponsInRange);
    }
}
