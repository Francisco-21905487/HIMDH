using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponFollowPlayer : MonoBehaviour
{
    public GameObject enemy;
    public GameObject weaponPivot;

    public bool needFlip;

    //Player
    private GameObject player;

    //Tree
    private GameObject tree;

    private MeleeEnemyBehavior meleeEnemyBehavior;
    private BossBehavior bossBehavior;
    private EnemyRange enemyRange;

    void Start()
    {
        //Player
        player = GameObject.Find("Player");

        if (GameManager.currentGameMode == GameMode.ArenaMode)
        {
            //Tree
            tree = GameObject.Find("Tree");

            meleeEnemyBehavior = enemy.GetComponent<MeleeEnemyBehavior>();
            bossBehavior = enemy.GetComponent<BossBehavior>();
            enemyRange = enemy.GetComponent<EnemyRange>();
        }
    }
    void Update()
    {
        Vector3 direction;

        if (GameManager.currentGameMode == GameMode.StoryMode)
        {
            direction = (player.transform.position - enemy.transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (needFlip)
            {
                if (player.transform.position.x > enemy.transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (player.transform.position.x < enemy.transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }

            weaponPivot.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (GameManager.currentGameMode == GameMode.ArenaMode)
        {
            if (meleeEnemyBehavior != null)
            {

                if (player.transform.position.x > enemy.transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (player.transform.position.x < enemy.transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                if (meleeEnemyBehavior.behaviorState == MeleeEnemyBehavior.EnemyBehaviorState.AttackTree)
                {
                    direction = (tree.transform.position - enemy.transform.position).normalized;
                }
                else
                {
                    direction = (player.transform.position - enemy.transform.position).normalized;
                }

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                weaponPivot.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (enemyRange != null)
            {
                if (enemyRange.behaviorState == EnemyRange.EnemyBehaviorState.AttackTree)
                {
                    direction = (tree.transform.position - enemy.transform.position).normalized;
                }
                else
                {
                    direction = (player.transform.position - enemy.transform.position).normalized;
                }

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                weaponPivot.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (bossBehavior != null)
            {
                if (player.transform.position.x > enemy.transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (player.transform.position.x < enemy.transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                if (bossBehavior.behaviorState == BossBehavior.EnemyBehaviorState.AttackTree)
                {
                    direction = (tree.transform.position - enemy.transform.position).normalized;
                }
                else
                {
                    direction = (player.transform.position - enemy.transform.position).normalized;
                }
            }
        }
    }
}
