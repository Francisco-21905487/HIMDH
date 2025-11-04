using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalArrow : MonoBehaviour
{
    private Rigidbody2D rb;

    public float damage;
    private float lifeTime = 1f;
    public float bulletVelocity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        damage = 40f;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.right * bulletVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth = other.gameObject.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Walls") || other.gameObject.CompareTag("Npc"))
        {
            Destroy(gameObject);
        }
    }
}

