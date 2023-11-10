using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private Rigidbody2D rb;
    public PlayerMovement playerMovement;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Death()
    {
        animator.SetTrigger("Die");

    }

    private void Update()
    {
        float moveSpeed = playerMovement.moveSpeed;

        if (currentHealth == 0 || maxHealth == 0)
        {
            Death();
            moveSpeed = 0f;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && currentHealth > 0)
        {
            currentHealth -= 1;
            print(currentHealth);

            if(currentHealth == 0)
            {
                print("Die very");
            }

        }
            
    }

}
