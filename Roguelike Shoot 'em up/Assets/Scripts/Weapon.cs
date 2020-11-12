using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float fireRate; // Interval between bullets
    public int bullets;
    public float range;
    public float bulletSpeed;
    public float burstDelay;
    public float accuracy;

    public bool spread;
    public bool burst;

    public GameObject Bullet;

    private Vector3 mousePosition;
    private bool canShoot = true;

    public void Positions()
    {
        mousePosition = Input.mousePosition;

        // Translates player position in game into player position on screen
        Vector3 offset = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition -= offset;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && canShoot)
        {
            Positions();

            if (burst)
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

        for (int i = 0; i < bullets; i++)
        {
            GameObject bullet = Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);

            float spread = this.spread ? FireSpread(i + 1) : 0;

            bullet.GetComponent<Bullet_Movement>().Angle(spread, mousePosition, transform.position);
        }

        StartCoroutine(FireDelay());
    }

    private IEnumerator FireBurst()
    {
        canShoot = false;

        for (int i = 0; i < bullets; i++)
        {
            GameObject bullet = Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);

            float spread = this.spread ? FireSpread(i + 1) : 0;

            bullet.GetComponent<Bullet_Movement>().Angle(spread, mousePosition, transform.position);

            yield return new WaitForFixedUpdate();
            yield return new WaitForSecondsRealtime(burstDelay);
        }

        StartCoroutine(FireDelay());
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSecondsRealtime(fireRate);
        canShoot = true;
    }

    private float FireSpread(float i)
    {
        float mid = (bullets + 1) / 2;

        i -= mid;

        float angle = i / mid * (bullets * 3);
        angle = Mathf.Deg2Rad * angle;

        return angle;
    }
}