using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 travelledDistance;

    private GameObject weapon;
    private Weapon weaponStats;

    private void Awake()
    {
        travelledDistance = new Vector3(0, 0, 0);

        weaponStats = GameObject.FindGameObjectWithTag("EquippedWeapon").GetComponent<Weapon>();
    }

    public void Angle(float bullet, Vector3 mousePosition, Vector3 weaponPosition)
    {
        float x = mousePosition.x - weaponPosition.x;
        float y = mousePosition.y - weaponPosition.y;

        float radians = Mathf.Atan2(y, x);

        radians += bullet;

        float accuracyMod = Random.Range(-weaponStats.accuracy, weaponStats.accuracy) * Mathf.Deg2Rad;
        radians += accuracyMod;

        velocity = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);

        velocity *= weaponStats.bulletSpeed * Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        transform.position += velocity;

        travelledDistance += velocity;

        if (travelledDistance.magnitude >= weaponStats.range)
            Destroy(gameObject);
    }
}