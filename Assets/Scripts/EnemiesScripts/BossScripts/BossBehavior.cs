using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossBehavior : MonoBehaviour
{
    public enum EnemyBehaviorState
    {
        Idle,
        Pursue,
        AttackPlayer,
        SpecialAttack,
        AttackTree
    }

    public GameObject lightningRadiusPrefab;
    public GameObject bossSwordPivot;
    public GameObject bossSword;
    public Collider2D swordCollider;

    Vector2 moveDirection;

    private Rigidbody2D rb;
    private Animator animator;
    private GameObject centerOfRoom;
    private GameObject bossRoom;

    //Player
    private GameObject player;

    //Tree
    private GameObject tree;

    public float moveSpeed;
    public bool canDestroyLightning;
    public float distanceToDetect;
    public float distanceToAttack;
    public int numberOfLightning = 5;
    public float spawnRadius = 5f;
    public float minDistance = 1f;
    public bool invencible = false;
    public EnemyBehaviorState behaviorState = EnemyBehaviorState.Idle;

    //Attack Player
    private float lastAttackTime;
    private int percentage;
    private float timerToStopNAttack = 2f;
    private bool resetSpecialAttack;
    private bool canMove = true;
    private bool specialAttack = false;
    private bool percentageAttack = true;
    private bool inPosition = false;
    private bool spawnLightning = true;
    private float arrivalThreshold = 0.2f;


    // Start is called before the first frame update
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

        //BossRoom = GameObject.Find("BossRoom");
        centerOfRoom = GameObject.Find("CenterOfRoom");
        bossSword = GameObject.Find("BossShortSword");

        lastAttackTime = -timerToStopNAttack;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;
        float playerAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //Detect Behavior
        if (Vector2.Distance(transform.position, player.transform.position) < distanceToAttack && specialAttack == false)
        {
            if (percentageAttack)
            {
                percentage = Random.Range(0, 101);
                percentageAttack = false;
            }

            if (percentage <= 70)
            {
                HandleNormalAttack();
            }
            else
            {
                swordCollider.enabled = false;
                specialAttack = true;
            }
            
        }
        else if(Vector2.Distance(transform.position, player.transform.position) < distanceToDetect && specialAttack == false)
        {
            behaviorState = EnemyBehaviorState.Pursue;
            bossSwordPivot.transform.rotation = Quaternion.AngleAxis(playerAngle, Vector3.forward);
            moveDirection = playerDirection;
            canMove = true;
            animator.SetBool("canWalk", true);
        }
        else
        {
            if (GameManager.currentGameMode == GameMode.ArenaMode)
            {
                Vector3 treeDirection = (tree.transform.position - transform.position).normalized;
                float treeAngle = Mathf.Atan2(treeDirection.y, treeDirection.x) * Mathf.Rad2Deg;

                behaviorState = EnemyBehaviorState.SpecialAttack;
                bossSwordPivot.transform.rotation = Quaternion.AngleAxis(treeAngle, Vector3.forward);
                moveDirection = treeDirection;

                if (Vector2.Distance(transform.position, tree.transform.position) < distanceToAttack)
                {
                    HandleNormalAttackToTree();
                }
            }
            else
            {
                if (specialAttack == false)
                {
                    canMove = false;
                    animator.SetBool("canWalk", false);
                }
                
            }
        }

        if (specialAttack)
        {
            HandleSpecialAttack();
        }
    }

    public void HandleSpecialAttack()
    {
        if (GameManager.currentGameMode == GameMode.StoryMode)
        {
            behaviorState = EnemyBehaviorState.SpecialAttack;

            invencible = true;

            bossSword.SetActive(false);

            Vector3 direction = (centerOfRoom.transform.position - transform.position).normalized;
            moveDirection = direction;

            if (Vector3.Distance(transform.position, centerOfRoom.transform.position) < arrivalThreshold)
            {
                transform.position = centerOfRoom.transform.position;
                canMove = false;
                animator.SetBool("canWalk", false);
                inPosition = true;
            }

            if (inPosition)
            {
                if (spawnLightning)
                {
                    for (int i = 1; i <= numberOfLightning; i++)
                    {
                        Vector3 spawnPosition = GenerateSpawnPosition();

                        Instantiate(lightningRadiusPrefab, spawnPosition, Quaternion.identity);

                        if (i == numberOfLightning)
                        {
                            spawnLightning = false;
                        }
                    }
                }

                if (canDestroyLightning)
                {
                    GameObject[] DestroyLightnings = GameObject.FindGameObjectsWithTag("Lightning");
                    foreach (GameObject DestroyLightning in DestroyLightnings)
                    {
                        Destroy(DestroyLightning);
                    }

                    if (DestroyLightnings.Length == 0)
                    {
                        resetSpecialAttack = true;
                    }
                }

                if (resetSpecialAttack)
                {
                    bossSword.SetActive(true);
                    specialAttack = false;
                    percentageAttack = true;
                    canMove = true;
                    animator.SetBool("canWalk", true);
                    spawnLightning = true;
                    invencible = false;
                    canDestroyLightning = false;
                    resetSpecialAttack = false;
                    inPosition = false;
                }
            }
        }
        else
        {
            behaviorState = EnemyBehaviorState.SpecialAttack;

            invencible = true;

            bossSword.SetActive(false);

            canMove = false;
            animator.SetBool("canWalk", false);

            if (spawnLightning)
            {
                for (int i = 1; i <= numberOfLightning; i++)
                {
                    Vector3 spawnPosition = GenerateSpawnPosition();

                    Instantiate(lightningRadiusPrefab, spawnPosition, Quaternion.identity);

                    if (i == numberOfLightning)
                    {
                        spawnLightning = false;
                    }
                }
            }

            if (canDestroyLightning)
            {
                GameObject[] DestroyLightnings = GameObject.FindGameObjectsWithTag("Lightning");
                foreach (GameObject DestroyLightning in DestroyLightnings)
                {
                    Destroy(DestroyLightning);
                }

                if (DestroyLightnings.Length == 0)
                {
                    resetSpecialAttack = true;
                }
            }

            if (resetSpecialAttack)
            {
                bossSword.SetActive(true);
                specialAttack = false;
                percentageAttack = true;
                canMove = true;
                animator.SetBool("canWalk", true);
                spawnLightning = true;
                invencible = false;
                canDestroyLightning = false;
                resetSpecialAttack = false;
                inPosition = false;
            }
        }
    }

    public Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnPosition;

        do
        {
            float angle = Random.Range(0f, 360f);
            float x = transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius;
            float y = transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius;

            spawnPosition = new Vector3(x, y, 0f);
        } while (CheckOverlap(spawnPosition));

        return spawnPosition;
    }

    public bool CheckOverlap(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, minDistance);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Lightning")
            {
                return true;
            }
        }

        return false;
    }

    public void HandleNormalAttack()
    {
        behaviorState = EnemyBehaviorState.AttackPlayer;

        if (Time.time - lastAttackTime > timerToStopNAttack)
        {
            animator.SetTrigger("attack");
            percentageAttack = true;
            lastAttackTime = Time.time;
        }
    }

    public void HandleNormalAttackToTree()
    {
        behaviorState = EnemyBehaviorState.AttackTree;

        if (Time.time - lastAttackTime > timerToStopNAttack)
        {
            animator.SetTrigger("attack");
            percentageAttack = true;
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
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * 0;
        }
        
    }
}
