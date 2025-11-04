using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float damage;
    public float knockbackForce;
    public bool canHit;
    
    public Collider2D swordCollider;

    private Cheats cheats;
    private GameObject boss;

    //Player
    private GameObject player;
    private Health playerHealth;

    //Tree
    private Health treeHealth;
    private GameObject tree;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");

        //Player
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();

        if (GameManager.currentGameMode == GameMode.ArenaMode)
        {
            //Tree
            tree = GameObject.Find("Tree");
            treeHealth = tree.GetComponent<Health>();
        }

        cheats = player.GetComponent<Cheats>();

        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
    }

    private void Update()
    {
        if (swordCollider.enabled == false)
        {
            canHit = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!cheats.PlayerInvencible && canHit)
            {
                playerHealth.TakeDamage((int)damage);
                canHit = false;
            }
        }

        if (collision.CompareTag("Tree"))
        {
            treeHealth.TreeTakeDamage((int)damage);
        }
    }
}
