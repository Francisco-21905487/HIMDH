using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public LayerMask whatIsEnemy;
    public Animator animator;

    public float bulletVelocity = 10f;
    public float bulletLifetime = 2f;
    public float shootCooldown = 0.5f; // Cooldown time between shots
    public bool canShoot;

    private bool hasShot = false;
    private float lastShootTime; // Time when the last shot was fired


    void Start()
    {
        lastShootTime = -shootCooldown; // Set initial value to allow shooting immediately
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time - lastShootTime > shootCooldown)
        {
            animator.SetTrigger("attackBow");
            hasShot = false;
            lastShootTime = Time.time;
        }

        if (canShoot && !hasShot)
        {
            Shoot();
            hasShot = true;
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - (Vector2)shootPoint.position).normalized;

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootDirection * bulletVelocity;
        // Disable shooting

        Destroy(newBullet, bulletLifetime);
    }
}
