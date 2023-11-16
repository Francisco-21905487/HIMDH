using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveEnemyBehavior : MonoBehaviour
{
    public GameObject Player;
    public PlayerHealth PlayerHealth;
    Vector2 MoveDirection;

    private Rigidbody2D rb;

    public float MoveSpeed;
    public float DistanceToExplode;
    public float DistanceToDetect;
    public float MaxDistanceDamage;
    public int Damage;
    public int BehaviorState = 1; // 1 = Idle | 2 = Pursue | 3 = Explode

    private bool StartCountdown;
    private float CountdownToExplode = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Detect Behavior
        if (Vector2.Distance(transform.position, Player.transform.position) < DistanceToExplode)
        {
            BehaviorState = 3;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            MoveSpeed = 0;
            StartCountdown = true;

        }
        else if(Vector2.Distance(transform.position, Player.transform.position) < DistanceToDetect)
        {
            BehaviorState = 2;
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            MoveDirection = direction;
        }

        //Countdown To Explode
        if (StartCountdown)
        {
            CountdownToExplode -= Time.deltaTime;
            if (CountdownToExplode < 0)
            {
                if (Vector2.Distance(transform.position, Player.transform.position) < MaxDistanceDamage)
                {
                    float DistanceToDamage = 1f - (Vector2.Distance(transform.position, Player.transform.position) / MaxDistanceDamage);
                    float DamageByDistance = DistanceToDamage * Damage;
                    PlayerHealth.TakeDamage((int) DamageByDistance);
                    Debug.Log((int) DamageByDistance);
                }
                Destroy(gameObject);
                StartCountdown = false;
            }
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(MoveDirection.x, MoveDirection.y) * MoveSpeed;
    }
}
