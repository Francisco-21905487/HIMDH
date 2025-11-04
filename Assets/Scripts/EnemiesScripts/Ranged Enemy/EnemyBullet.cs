using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    

    [Range(1, 10)]
    public float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 1f;

    private Rigidbody2D rb;
    private Cheats Cheats;
    private GameObject Player;

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        Cheats = Player.GetComponent<Cheats>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            //Destroy(other.gameObject);
            //Destroy(gameObject);

            Health playerHealth = other.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                if (!Cheats.PlayerInvencible)
                {
                    playerHealth.TakeDamage((int)damage);
                }
            }
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Tree"))
        {
            Health TreeHealth = other.gameObject.GetComponent<Health>();
            TreeHealth.TreeTakeDamage((int)damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Walls") || other.gameObject.CompareTag("Npc"))
        {
            Destroy(gameObject);
        }
    }
}
