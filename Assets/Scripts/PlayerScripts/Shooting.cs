using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletVelocity = 10f;
    public float bulletLifetime = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        newBullet.transform.rotation = Quaternion.Euler(0, 0, -90);

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletVelocity;

        Destroy(newBullet, bulletLifetime);
    }


}
