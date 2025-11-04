using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScythAttack : MonoBehaviour
{
    public GameObject playerSwordPivot;
    public GameObject playerSwordHitBox;
    public Collider2D playerSwordCollider;

    public Animator animator;
    public float attackCooldown = 0.5f;

    public float damage;


    private float lastAttackTime;

    private void Start()
    {
        lastAttackTime = -attackCooldown;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackCooldown)
        {
            animator.SetTrigger("attackScythe");
            lastAttackTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth = collider.gameObject.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
