using UnityEngine;

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

        if (!Drop()) return; // Returns if the weapon cannot be dropped

        weapon.GetComponent<Weapon>().enabled = true;

        // OnEnable() function handles the rest
    }

    private bool Drop() // Returns false if the weapon cannot be dropped
    {
        Transform weapon;

        // Set weapon to equal the currently equipped weapon of the player; if they have none, return false - "the weapon could not be dropped"
        try
        {
            weapon = transform.GetChild(0).GetChild(0);
        }
        catch
        {
            return true;
        }

        Weapon weaponScript = weapon.GetComponent<Weapon>();
        if (!weaponScript.canDisable) return false;

        weaponScript.enabled = false;

        // Force the player to abandon their child
        weapon.transform.SetParent(GameObject.Find("Dropped Weapons").transform);
        weapon.transform.rotation = Quaternion.identity;

        weapon.tag = "Weapon";

        return true;
    }
}