using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class ExplosiveEnemyBehavior : MonoBehaviour
{
    public enum EnemyBehaviorState
    {
        Idle,
        Pursue,
        ExplodeNearPlayer,
        FollowTree,
        ExplodeNearTree
    }

    Vector2 moveDirection;

    private Cheats cheats;
    private Rigidbody2D rb;
    private Animator animator;

    //Player
    private Health playerHealth;
    private GameObject player;

    //Tree
    private Health treeHealth;
    private GameObject tree;

    public float moveSpeed;
    public float distanceToExplode;
    public float distanceToDetect;
    public float distanceDamage;
    public int maxDamage;
    public EnemyBehaviorState behaviorState = EnemyBehaviorState.Idle;

    [SerializeField] private bool canExplode;
    [SerializeField] private bool canDestroy;
    private bool canMove = true;
    private bool explode = true;
    private bool canHit = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Player
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();


        //Tree
        if (GameManager.currentGameMode == GameMode.ArenaMode)
        {
            tree = GameObject.Find("Tree");
            treeHealth = tree.GetComponent<Health>();
        }

        cheats = player.GetComponent<Cheats>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //Detect Behavior
        if (canExplode == false)
        {
            if (playerDistance < distanceToExplode)
            {
                behaviorState = EnemyBehaviorState.ExplodeNearPlayer;
                canMove = false;
                animator.SetBool("canRun", false);

                if (explode)
                {
                    animator.SetTrigger("explode");
                    explode = false;
                }

            }
            else if (playerDistance < distanceToDetect)
            {
                behaviorState = EnemyBehaviorState.Pursue;
                Vector3 direction = (player.transform.position - transform.position).normalized;
                moveDirection = direction;
                canMove = true;
                animator.SetBool("canRun", true);
            }
            else
            {
                if (GameManager.currentGameMode == GameMode.ArenaMode)
                {
                    behaviorState = EnemyBehaviorState.FollowTree;
                    Vector3 direction = (tree.transform.position - transform.position).normalized;
                    moveDirection = direction;
                    canMove = true;
                    animator.SetBool("canRun", true);
                    if (Vector2.Distance(transform.position, tree.transform.position) < distanceToExplode)
                    {
                        behaviorState = EnemyBehaviorState.ExplodeNearTree;
                        canMove = false;
                        animator.SetBool("canRun", false);
                        if (explode)
                        {
                            animator.SetTrigger("explode");
                            explode = false;
                        }
                    }
                }
                else
                {
                    canMove = false;
                    animator.SetBool("canRun", false);
                }
            }
        }

        //Explode
        if (canExplode)
        {
            canMove = false;
            GetComponent<BoxCollider2D>().enabled = false;
            if (playerDistance < distanceDamage)
            {
                
                if (canHit)
                {
                    if (!cheats.PlayerInvencible)
                    {
                        playerHealth.TakeDamage((int) CalculateDamage(playerDistance));
                    }

                    canHit = false;
                }
                
            }
            else if (GameManager.currentGameMode == GameMode.ArenaMode && Vector2.Distance(transform.position, tree.transform.position) < distanceDamage)
            {
                if (canHit)
                {
                    treeHealth.TreeTakeDamage((int) CalculateDamage(Vector2.Distance(transform.position, tree.transform.position)));
                    canHit = false;
                }
            }
        }

        if (canDestroy)
        {
            Destroy(gameObject);
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
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * 0;
        }
    }

    private float CalculateDamage(float distance)
    {
        // Calculate damage based on distance
        float distanceFactor = Mathf.Clamp01((distance - 0) / (distanceDamage - 0));
        float calculatedDamage = maxDamage * (1f - distanceFactor);
        return calculatedDamage;
    }
}

