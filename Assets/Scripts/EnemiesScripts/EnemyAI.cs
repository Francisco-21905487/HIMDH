using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float detectionRadius = 5.0f;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }

        if (player.position.x < transform.position.x)

            {
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


    }
}
