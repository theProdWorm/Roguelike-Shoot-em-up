using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage, // The amount of premitigation damage the weapon deals
                fireRate, // Interval between bursts
                burstDelay, // Interval between single bullets in a burst
                bullets, // Amount of bullets in a single burst
                range, // How many units the bullets can move before disappearing
                bulletSpeed, // How fast the bullets travel
                inaccuracy; // How many degrees that bullets can randomly direct themselves in from the original direction

    public bool spread,
                burst,
                explosive;

    public GameObject Bullet;

    private AudioSource audioSource;

    private Vector3 mouseClickPosition;
    private bool canShoot = true;
    public bool canDisable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Transform weapon = GameObject.Find("Weapon").transform;

        // Set position and parent to follow the player around
        transform.SetParent(weapon);
        transform.position = weapon.position;

        tag = "EquippedWeapon";
    }

    private void Update()
    {
        Rotate();

        if (Input.GetButton("Fire1") && canShoot)
        {
            Positions();

            canDisable = false;
            if (burst)
            {
                StartCoroutine(Burst());
                return;
            }

            Fire();
        }
    }

    public void Positions()
    {
        mouseClickPosition = Input.mousePosition;

        // Gets and sets offset between click position and shooting point
        Vector3 offset = Camera.main.WorldToScreenPoint(transform.position);
        mouseClickPosition -= offset;
    }

    public float Angle(Vector2 from, Vector2 to)
    {
        float x = to.x - from.x;
        float y = to.y - from.y;

        float radians = Mathf.Atan2(y, x);

        return radians;
    }

    public void Rotate()
    {
        Transform hand = GameObject.Find("Weapon").GetComponent<Transform>();

        // Gets and sets offset between mouse position and hand position
        Vector3 _mPos = Input.mousePosition;
        Vector3 offset = Camera.main.WorldToScreenPoint(transform.position);
        _mPos -= offset;

        float angle = Angle(hand.position, _mPos) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Fire()
    {
        canShoot = false;

        for (int i = 0; i < bullets; i++)
        {
            InstantiateBullet(i);
        }

        audioSource.PlayOneShot(audioSource.clip);

        StartCoroutine(FireDelay());
    }

    private IEnumerator Burst()
    {
        canShoot = false;

        for (int i = 0; i < bullets; i++)
        {
            InstantiateBullet(i);

            audioSource.PlayOneShot(audioSource.clip);

            yield return new WaitForSeconds(burstDelay);
        }

        StartCoroutine(FireDelay());
    }

    private void InstantiateBullet(int i)
    {
        GameObject bullet = Instantiate(Bullet, transform.position, Quaternion.identity);

        float spread = this.spread ? Spread(i + 1) : 0;
        float angle = Angle(transform.position, mouseClickPosition);
        float inaccuracy = Random.Range(-this.inaccuracy, this.inaccuracy) * Mathf.Deg2Rad;

        float finalAngle = spread + angle + inaccuracy;
        //bullet.GetComponent<Bullet>().SetVelocity(finalAngle, bulletSpeed);
    }

    private IEnumerator FireDelay()
    {
        canDisable = true;

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private float Spread(float i)
    {
        float mid = (bullets + 1) / 2;

        i -= mid;

        float angle = i / mid * (bullets * 3);
        angle = Mathf.Deg2Rad * angle;

        return angle;
    }
}