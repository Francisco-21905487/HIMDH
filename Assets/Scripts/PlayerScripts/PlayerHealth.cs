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

    private void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            playerMovement.moveSpeed = 0f;
            //animator.SetTrigger("Die");
        }
    }

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && currentHealth > 0)
        {
            currentHealth -= 1;
            print(currentHealth);

            PlayerHealth.TakeDamage(1);

            if (currentHealth == 0)
            {
                print("Die very");
            }

        }
            
    }*/

}
