using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject shortSword;
    public GameObject spear;
    public GameObject scythe;
    public GameObject bigSword;
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
            if (shortSword.activeSelf == true)
            {
                animator.SetTrigger("attack");
                lastAttackTime = Time.time;
            }else if (spear.activeSelf == true)
            {
                animator.SetTrigger("attackSpear");
                lastAttackTime = Time.time;
            }else if (bigSword.activeSelf == true)
            {
                animator.SetTrigger("attackBigSword");
                lastAttackTime = Time.time;
            }
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