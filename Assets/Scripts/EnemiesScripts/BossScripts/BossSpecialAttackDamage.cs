using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpecialAttackDamage : MonoBehaviour
{

    private GameObject player;
    private Cheats cheats;
    private Health playerHealth;

    public float damage;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();
        cheats = player.GetComponent<Cheats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!cheats.PlayerInvencible)
            {
                playerHealth.TakeDamage((int)damage);
            }
        }
    }
}
