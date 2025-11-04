using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesAM : MonoBehaviour
{
    public GameObject meleeEnemy; // type 1
    public GameObject explosiveEnemy; // type 2
    public GameObject rangedEnemy; // type 3
    public GameObject boss; // type 4
    public Transform[] enemiesSpawnPositions;

    public bool canSpawnEnemies;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnEnemies)
        {
            if (Input.GetKeyDown("1"))
            {
                SpawnEnemies(1);
            }

            if (Input.GetKeyDown("2"))
            {
                SpawnEnemies(2);
            }

            if (Input.GetKeyDown("3"))
            {
                SpawnEnemies(3);
            }

            if (Input.GetKeyDown("4"))
            {
                SpawnEnemies(4);
            }
        }
    }

    public void SpawnEnemiesBasedOnReceivedData(string receivedData)
    {
        if (canSpawnEnemies)
        {
            if (receivedData == "enemy1")
            {
                SpawnEnemies(1);
            }
            else if (receivedData == "enemy2")
            {
                SpawnEnemies(2);
            }
            else if (receivedData == "enemy3")
            {
                SpawnEnemies(3);
            }
            else if (receivedData == "enemy4")
            {
                SpawnEnemies(4);
            }
        }
    }

    private void SpawnEnemies(int type)
    {
        int RandSpawn = Random.Range(0, enemiesSpawnPositions.Length);

        if (type == 1)
        {
            Instantiate(meleeEnemy, enemiesSpawnPositions[RandSpawn].position, enemiesSpawnPositions[RandSpawn].rotation);
        }

        if (type == 2)
        {
            Instantiate(explosiveEnemy, enemiesSpawnPositions[RandSpawn].position, enemiesSpawnPositions[RandSpawn].rotation);
        }

        if (type == 3)
        {
            Instantiate(rangedEnemy, enemiesSpawnPositions[RandSpawn].position, enemiesSpawnPositions[RandSpawn].rotation);
        }

        if (type == 4)
        {
            Instantiate(boss, enemiesSpawnPositions[RandSpawn].position, enemiesSpawnPositions[RandSpawn].rotation);
        }

    }
}
