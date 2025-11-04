using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ExplosiveArrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator explosionAnimator;
    public AudioSource explosionAudioSource;
    public AudioMixerGroup audioMixerGroupExplosion;

    public float explosionRange;
    public float damage;
    public float bulletVelocity = 10f;
    public bool canDestroyArrow;

    //private float lifeTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        explosionAnimator = GetComponent<Animator>(); 
        //Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.right * bulletVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            explosionAnimator.SetTrigger("explode");
            explosionAudioSource.outputAudioMixerGroup = audioMixerGroupExplosion;
            explosionAudioSource.Play();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Health enemyHealth = enemy.gameObject.GetComponent<Health>();

                if (Vector2.Distance(transform.position, enemy.transform.position) < explosionRange)
                {
                    
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damage);
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("Walls"))
        {
            explosionAnimator.SetTrigger("explode");
            explosionAudioSource.outputAudioMixerGroup = audioMixerGroupExplosion;
            explosionAudioSource.Play();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Health enemyHealth = enemy.gameObject.GetComponent<Health>();

                if (Vector2.Distance(transform.position, enemy.transform.position) < explosionRange)
                {
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damage);
                    }
                }
            }
        }

        if (other.gameObject.CompareTag("Npc"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("SecretWall"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void DestroyArrow()
    {
        Destroy(gameObject);
    }
}