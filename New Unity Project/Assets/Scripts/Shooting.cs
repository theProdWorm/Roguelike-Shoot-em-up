using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float Damage;
    public float FireRate; // Interval between bullets
    public int Bullets;
    public float Range;
    public float BulletSpeed;
    public float BurstDelay;
    public float Accuracy;

    public bool Spread;
    public bool Burst;

    protected int bulletIteration;

    public GameObject Bullet;

    private Vector3 weaponPosition, mousePosition;
    private bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    public void Positions()
    {
        mousePosition = Input.mousePosition;

        GameObject weapon = GameObject.FindGameObjectWithTag("EquippedWeapon");
        weaponPosition = weapon.transform.position;

        // Translates player position in game into player position on screen
        Vector3 offset = Camera.main.WorldToScreenPoint(weaponPosition);
        mousePosition -= offset;
    }

    private void Update()
    {
        canShoot = gameObject.tag == "EquippedWeapon" ? canShoot : false;

        if (Input.GetButton("Fire1") && canShoot)
        {
            Positions();

            if (Burst)
            {
                StartCoroutine(FireBurst());
                return;
            }

            Fire();
        }
    }

    private void Fire()
    {
        canShoot = false;

        for (int i = 0; i < Bullets; i++)
        {
            GameObject bullet = Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);

            float spread = Spread ? FireSpread(i + 1) : 0;

            bullet.GetComponent<Bullet_Movement>().Angle(spread, mousePosition, weaponPosition);
        }

        StartCoroutine(FireDelay());
    }

    private IEnumerator FireBurst()
    {
        canShoot = false;

        for (int i = 0; i < Bullets; i++)
        {
            GameObject bullet = Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);

            float spread = Spread ? FireSpread(i + 1) : 0;

            bullet.GetComponent<Bullet_Movement>().Angle(spread, mousePosition, weaponPosition);

            yield return new WaitForFixedUpdate();
            yield return new WaitForSecondsRealtime(BurstDelay);
        }

        StartCoroutine(FireDelay());
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSecondsRealtime(FireRate);
        canShoot = true;
    }

    private float FireSpread(float i)
    {
        float mid = (Bullets + 1) / 2;

        i -= mid;

        float angle = i / mid * (Bullets * 3);
        angle = Mathf.Deg2Rad * angle;

        return angle;
    }
}