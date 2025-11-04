using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject coinPrefab;
    public PlayerInventory playerInventory;
    public Animator animator;
    public AudioSource hurtAudioSource;
    public AudioSource potionAudioSource;
    public AudioMixerGroup audioMixerGroupHurt;
    public AudioMixerGroup audioMixerGroupPotion;
    public AudioClip[] hurtAudioClip;
    public float showCurrentHealth;
    public bool isDead = false;

    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private bool dropCoins;

    public enum EntityType { Player, Enemy, Boss };
    public EntityType entityType;

    private void Awake()
    {
        currentHealth = startingHealth;
        showCurrentHealth = startingHealth;
    }

    private void Start()
    {
        if (GameManager.currentGameMode == GameMode.ArenaMode)
        {
            dropCoins = true;
        }
        else
        {
            dropCoins = false;
        }
    }

    private void Update()
    {
        if (isDead) 
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        showCurrentHealth = currentHealth;

        hurtAudioSource.clip = hurtAudioClip[Random.Range(0, hurtAudioClip.Length)];
        hurtAudioSource.outputAudioMixerGroup = audioMixerGroupHurt;
        hurtAudioSource.Play();

        if (currentHealth <= 0)
        {
            if (entityType == EntityType.Player)
            {
                animator.SetTrigger("isDead");
                SceneManager.LoadScene("DeathScreen");
            }
            else if (entityType == EntityType.Enemy)
            {
                if (dropCoins)
                {
                    Instantiate(coinPrefab, transform.position, Quaternion.identity);
                }
                animator.SetTrigger("isDead");
            }
            else if (entityType == EntityType.Boss)
            {
                if (dropCoins)
                {
                    Instantiate(coinPrefab, transform.position, Quaternion.identity);
                }
                animator.SetTrigger("isDead");
                if (GameManager.currentGameMode == GameMode.StoryMode)
                {
                    SceneManager.LoadScene("WinScreen");
                }
            
            }
        }
    }

    public void TakeDamageK(float _damage, Vector3 position)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        showCurrentHealth = currentHealth;

        Vector2 knockbackDirection = (transform.position - position).normalized;
        Vector2 knockback = knockbackDirection * rb.mass * 100000;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockback, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            if (dropCoins)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            animator.SetTrigger("isDead");
            //Destroy(gameObject);
        }
    }

    public void TreeTakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        showCurrentHealth = currentHealth;
        if (currentHealth <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            //Destroy(gameObject);
        }
    }

    public void GiveHealth(float heal)
    {
        if (currentHealth > 0 && currentHealth < startingHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + heal, 0, startingHealth);
        }
        else
        {
            Debug.Log("You have full health");
        }
    }

    public void GiveHealthFromInventory(float heal, Items item)
    {
        if (currentHealth > 0 && currentHealth < startingHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + heal, 0, startingHealth);
            potionAudioSource.outputAudioMixerGroup = audioMixerGroupPotion;
            potionAudioSource.Play();
            playerInventory.RemoveItem(item);
        }
        else
        {
            Debug.Log("You have full health");
        }
    }

    public void GiveHealthFromKey(float heal)
    {
        Items Item = playerInventory.FindItemByName("Potion");

        if (Item != null)
        {
            if (currentHealth > 0 && currentHealth < startingHealth)
            {
                currentHealth = Mathf.Clamp(currentHealth + heal, 0, startingHealth);
                potionAudioSource.outputAudioMixerGroup = audioMixerGroupPotion;
                potionAudioSource.Play();
                playerInventory.RemoveItem(Item);
            }
            else
            {
                Debug.Log("You have full health");
            }
        }
        else
        {
            Debug.Log("You don't have a health potion");
        }
    }
}
