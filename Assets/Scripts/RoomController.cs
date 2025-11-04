using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RoomController : MonoBehaviour
{
    public StorySpawn storySpawn;
    public GameObject[] enemies;
    public GameObject[] doors;
    public AudioClip[] doorsAudioClip;
    public AudioMixerGroup audioMixerGroupGates;
    public bool bossRoom;
    private float timerToCheck = 1f;
    private bool firstChecker = false;
    private bool canOpen = false;
    private float canOpenTimer = 1f;

    void Start()
    {
        OpenDoors();
    }

    void Update()
    {
        if (storySpawn.checkSpawnEnemy)
        {
            timerToCheck -= Time.deltaTime;
            if (timerToCheck <= 0 )
            {
                if (!AreEnemiesDead())
                {
                    OpenDoors();
                }
            }
        }
    }

    bool AreEnemiesDead()
    {
        List<GameObject> enemiesList = new List<GameObject>(enemies);
        List<GameObject> enemiesToRemove = new List<GameObject>();

        foreach (GameObject enemy in enemiesList)
        {
            if (enemy == null)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (GameObject enemyToRemove in enemiesToRemove)
        {
            enemiesList.Remove(enemyToRemove);
        }

        enemies = enemiesList.ToArray();

        if (enemies.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void CloseDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
            door.GetComponent<AudioSource>().clip = doorsAudioClip[1];
            door.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerGroupGates;
            door.GetComponent<AudioSource>().Play();
        }
    }

    void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door.activeSelf == true)
            {
                door.GetComponent<AudioSource>().clip = doorsAudioClip[0];
                door.GetComponent<AudioSource>().outputAudioMixerGroup = audioMixerGroupGates;
                door.GetComponent<AudioSource>().Play();

                canOpenTimer -= Time.deltaTime;
                if (canOpenTimer <= 0)
                {
                    canOpen = true;
                }

                if (canOpen)
                {
                    door.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !firstChecker)
        {
            if (bossRoom)
            {
                storySpawn.spawnBoss = true;
                firstChecker = true;
                CloseDoors();
            }
            else
            {
                storySpawn.spawnEnemy = true;
                storySpawn.checkSpawnEnemy = false;
                firstChecker = true;
                CloseDoors();
            }
        }
    }
}
