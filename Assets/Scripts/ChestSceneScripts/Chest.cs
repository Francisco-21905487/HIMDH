using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Chest : MonoBehaviour
{
    public AudioSource chestAudioSource;
    public AudioMixerGroup audioMixerGroupChests;
    public GameObject[] weapons;
    public GameObject potions;
    public Sprite newSprite;

    private GameObject player;

    private bool chestOpen = false;
    private float distance = 1.5f;
    private float spawnRadius = 1.5f;
    private float minDistance = 1f;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!chestOpen)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distance && Input.GetKeyDown("e"))
            {
                chestOpen = true;
                chestAudioSource.outputAudioMixerGroup = audioMixerGroupChests;
                chestAudioSource.Play();
                GetComponent<SpriteRenderer>().sprite = newSprite;

                int percentage = Random.Range(1, 100);

                Instantiate(potions, GenerateSpawnPosition(), Quaternion.identity); // 100%

                if (weapons.Length == 1)
                {
                    Instantiate(weapons[0], GenerateSpawnPosition(), Quaternion.identity); // 100%
                }
                else if(weapons.Length > 1)
                {
                    if (percentage > 0 && percentage < 11) // 10%
                    {
                        Instantiate(weapons[0], GenerateSpawnPosition(), Quaternion.identity);
                    }
                    else if (percentage > 10 && percentage < 41) // 30%
                    {
                        Instantiate(weapons[1], GenerateSpawnPosition(), Quaternion.identity);
                    }
                    else if (percentage > 40 && percentage < 101) // 60%
                    {
                        Instantiate(weapons[2], GenerateSpawnPosition(), Quaternion.identity);
                    }
                }
            }
        }
    }

    public Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnPosition;

        do
        {
            float angle = Random.Range(180f, 360f);
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
            if (collider.gameObject.tag == "Chest" || collider.gameObject.tag == "Walls" || collider.gameObject.tag == "Weapons" || collider.gameObject.tag == "Potion")
            {
                return true;
            }
        }

        return false;
    }
}

