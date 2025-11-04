using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerClaimDrops : MonoBehaviour
{
    private GameObject player;

    public AudioSource itemClaimAudioSource;
    public AudioMixerGroup audioMixerGroupItemPickUp;
    public Items item;

    private bool onTopOfItem;
    private bool canDestoyItem;
    private float timerToDestroy = 0.3f;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onTopOfItem)
        {
            PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                itemClaimAudioSource.outputAudioMixerGroup = audioMixerGroupItemPickUp;
                itemClaimAudioSource.Play();
                playerInventory.AddItem(item);
                onTopOfItem = false;
                canDestoyItem = true;
            }
            else
            {
                Debug.LogError("Player does not have an Inventory component.");
            }
        }
        if (canDestoyItem)
        {
            timerToDestroy -= Time.deltaTime;
            if (timerToDestroy <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTopOfItem = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onTopOfItem = false;
        }
    }
}

