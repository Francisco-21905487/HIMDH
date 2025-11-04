using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyRange : MonoBehaviour
{
    public enum EnemyBehaviorState
    {
        Idle,
        Pursue,
        AttackPlayer,
        AttackTree
    }

    Vector2 moveDirection;

    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    public float distanceToFollow;
    public float distanceToStop;
    public float retreatDistance;
    public float fireRate;
    public bool canShoot;
    public EnemyBehaviorState behaviorState = EnemyBehaviorState.Idle;

    private float timeToFire;
    private bool canMove;
    private bool hasShot = false;


    public Transform firingPoint;
    public GameObject enemyBullet;

    private Rigidbody2D rb;
    private Animator animator;

    //Player
    private Transform player;

    //Tree
    private Transform tree;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (GameManager.currentGameMode == GameMode.ArenaMode)
        {
            tree = GameObject.FindGameObjectWithTag("Tree").transform;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;
        Vector3 escapePlayerdirection = (transform.position - player.transform.position).normalized;

        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Vector2.Distance(transform.position, player.position) > distanceToFollow && Vector2.Distance(transform.position, player.position) < distanceToStop)
        {
            canMove = true;
            animator.SetBool("canRun", true);
            moveDirection = playerDirection;
        }
        else if (Vector2.Distance(transform.position, player.position) < distanceToFollow && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            behaviorState = EnemyBehaviorState.AttackPlayer;
            canMove = false;
            animator.SetBool("canRun", false);
            if (timeToFire <= -1f)
            {
                animator.SetTrigger("shoot");
                hasShot = false;
                timeToFire = fireRate;
            }
            else
            {
                timeToFire -= Time.deltaTime;
            }

            if (canShoot && !hasShot)
            {
                Shoot(player);
                hasShot = true;
            }
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            canMove = true;
            animator.SetBool("canRun", true);
            moveDirection = escapePlayerdirection;
        }
        else
        {
            if (GameManager.currentGameMode == GameMode.ArenaMode)
            {
                Vector3 treeDirection = (tree.transform.position - transform.position).normalized;
                canMove = true;
                animator.SetBool("canRun", true);
                moveDirection = treeDirection;

                if (Vector2.Distance(transform.position, tree.position) < distanceToFollow && Vector2.Distance(transform.position, tree.position) > retreatDistance)
                {
                    behaviorState = EnemyBehaviorState.AttackTree;
                    canMove = false;
                    animator.SetBool("canRun", false);
                    if (timeToFire <= -1f)
                    {
                        animator.SetTrigger("shoot");
                        hasShot = false;
                        timeToFire = fireRate;
                    }
                    else
                    {
                        timeToFire -= Time.deltaTime;
                    }

                    if (canShoot && !hasShot)
                    {
                        Shoot(tree);
                        hasShot = true;
                    }
                }
            }
        }
    }
    private void Shoot(Transform gameobject)
    {
        //if (timeToFire <= -1f)
        //{
            GameObject newBullet = Instantiate(enemyBullet, firingPoint.position, Quaternion.identity);

            Vector2 shootDirection = (gameobject.position - firingPoint.position).normalized;

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            newBullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = shootDirection * newBullet.GetComponent<EnemyBullet>().speed;

            //timeToFire = fireRate;
        //}
        //else
        //{
           // timeToFire -= Time.deltaTime;
        //}
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
        else
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * 0;
        }

    }

    /*private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > distanceToFollow && Vector2.Distance(transform.position, target.position) < distanceToStop)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, target.position) < distanceToFollow && Vector2.Distance(transform.position, target.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            RotateTowardsTarget();
            Shoot();
        }
        else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
    }*/
}
