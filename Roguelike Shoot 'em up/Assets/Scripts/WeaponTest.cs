using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform c_player;

    public float bulletSpeed,
                 bullets,
                 burstDelay,
                 fireRate,
                 inaccuracy;

    public bool burst,
                spread;

    private bool canShoot = true;

    private Transform transformAtClick;
    private Vector2 mousePos;

    private void OnEnable()
    {
        Transform player = GameObject.Find("Player").transform;

        // Set position and parent to follow the player around
        transform.SetParent(player);
        transform.position = player.position;
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1") && canShoot)
        {
            canShoot = false;

            transformAtClick = firePoint;

            if (burst)
            {
                StartCoroutine(Burst());
                return;
            }
            
            Fire();
        }
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.forward * angle;

        transform.position = c_player.position;
    }

    private void Fire()
    {
        for (int i = 0; i < bullets; i++)
        {
            float _spread = spread ? Spread(i) : 0;

            InstantiateBullet(_spread);
        }

        StartCoroutine(FireDelay());
    }

    private IEnumerator Burst()
    {
        transformAtClick = transform;

        for (int i = 0; i < bullets; i++)
        {
            float _spread = spread ? Spread(i) : 0;

            InstantiateBullet(_spread);
            yield return new WaitForSeconds(burstDelay);
        }

        StartCoroutine(FireDelay());
    }

    private void InstantiateBullet(float spread)
    {
        float deviation = Random.Range(-inaccuracy, inaccuracy);
        Quaternion clickRotation = Quaternion.AngleAxis(transformAtClick.eulerAngles.z + spread + deviation, Vector3.forward);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, clickRotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(clickRotation * Vector3.right * bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private float Spread(float i)
    {
        float mid = (bullets + 1) / 2;

        float angle = (i - mid) / mid * bullets * 3;

        return angle;
    }
}