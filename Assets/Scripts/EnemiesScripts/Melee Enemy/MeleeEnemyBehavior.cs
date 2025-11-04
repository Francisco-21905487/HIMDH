using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeEnemyBehavior : MonoBehaviour
{
    public enum EnemyBehaviorState
    {
        Idle,
        Pursue,
        AttackPlayer,
        AttackTree
    }

    Vector2 moveDirection;

    public GameObject meleeEnemySwordPivot;
    public GameObject meleeEnemySwordHitBox;
    public Collider2D swordCollider;

    private Rigidbody2D rb;
    private Animator animator;

    //Player
    private GameObject player;

    //Tree
    private GameObject tree;

    public float moveSpeed;
    public bool canMove;
    public float distanceToDetect;
    public float distanceToAttack;
    public float attackCooldown;
    public EnemyBehaviorState behaviorState = EnemyBehaviorState.Idle;

    //Attack Player
    private float lastAttackTime;

    private float distanceToPlayer;
    private float distanceToTree;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Player
        player = GameObject.Find("Player");

        //Tree
        if (GameManager.currentGameMode == GameMode.ArenaMode)
        {
            tree = GameObject.Find("Tree");
        }

        lastAttackTime = -attackCooldown;
    }


    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;
        float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        meleeEnemySwordPivot.transform.rotation = Quaternion.AngleAxis(playerAngle, Vector3.forward);

        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // Detect Behavior
        if (distanceToPlayer < distanceToAttack)
        {
            HandleNormalAttack();
        }
        else if (distanceToPlayer < distanceToDetect)
        {
            canMove = true;
            animator.SetBool("canWalk", true);
            behaviorState = EnemyBehaviorState.Pursue;
            moveDirection = playerDirection;
        }
        else
        {
            if (GameManager.currentGameMode == GameMode.ArenaMode)
            {
                distanceToTree = Vector2.Distance(transform.position, tree.transform.position);
                Vector3 treeDirection = (tree.transform.position - transform.position).normalized;
                float treeAngle = Mathf.Atan2(treeDirection.y, treeDirection.x) * Mathf.Rad2Deg;

                meleeEnemySwordPivot.transform.rotation = Quaternion.AngleAxis(treeAngle, Vector3.forward);

                behaviorState = EnemyBehaviorState.AttackTree;
                moveDirection = treeDirection;

                canMove = true;
                animator.SetBool("canWalk", true);

                if (distanceToTree < distanceToAttack)
                {
                    animator.SetBool("canWalk", false);
                    HandleNormalAttackToTree();
                }
            }
            else
            {
                canMove = false;
                animator.SetBool("canWalk", false);
            }
        }
    }

    public void HandleNormalAttack()
    {
        behaviorState = EnemyBehaviorState.AttackPlayer;

        if (Time.time - lastAttackTime > attackCooldown)
        {
            animator.SetTrigger("attack");
            lastAttackTime = Time.time;
        }
    }

    public void HandleNormalAttackToTree()
    {
        behaviorState = EnemyBehaviorState.AttackTree;

        if (Time.time - lastAttackTime > attackCooldown)
        {
            animator.SetTrigger("attack");
            lastAttackTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}