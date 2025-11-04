using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    public Collider2D swordcollider;

    private Cheats cheats;

    //Player
    private Health playerhealth;
    private GameObject player;

    //Tree
    private Health treehealth;
    private GameObject tree;


    public float damage;
    public float knockbackforce = 500f;

    private bool canHit;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerhealth = player.GetComponent<Health>();

        if (GameManager.currentGameMode == GameMode.ArenaMode)
        {
            tree = GameObject.Find("Tree");
            treehealth = tree.GetComponent<Health>();
        }

        cheats = player.GetComponent<Cheats>();

        if (swordcollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
    }

    private void Update()
    {
        if (swordcollider.enabled == false)
        {
            canHit = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !cheats.PlayerInvencible && canHit)
        {
            playerhealth.TakeDamage((int)damage);
            canHit = false;
        }

        if (GameManager.currentGameMode == GameMode.ArenaMode)
        {
            if (collision.CompareTag("Tree"))
            {
                treehealth.TreeTakeDamage((int)damage);
            }
        }
    }
}