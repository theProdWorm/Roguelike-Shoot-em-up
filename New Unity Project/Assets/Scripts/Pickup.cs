using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform weaponTrans;

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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
            PickupWeapon();

        if (Input.GetKeyDown(KeyCode.G) && weaponEquipped)
            Drop();

    }

    void PickupWeapon() 
    {
        if (!weaponEquipped)
        {
            // Enable shooting for the weapon
            weapon.GetComponent<Shooting>().enabled = true;

            // Set position and parent to follow the player around
            weapon.transform.SetParent(weaponTrans);
            weapon.transform.position = weaponTrans.position;

            // Tell the program that the weapon has been equipped
            weapon.tag = "EquippedWeapon";
            weaponEquipped = true;
        }

    }

    void Drop()
    {
        GameObject equippedWeapon = transform.GetChild(0).GetChild(0).gameObject;

        // Disable shooting for the weapon
        equippedWeapon.GetComponent<Shooting>().enabled = false;

        // Force the player to abandon their child
        equippedWeapon.transform.SetParent(null);

        // Tell the program that the weapon has been drop
        equippedWeapon.tag = "Weapon";
        weaponEquipped = false;
    }
}
