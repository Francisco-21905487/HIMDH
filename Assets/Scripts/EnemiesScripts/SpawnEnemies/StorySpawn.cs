using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StorySpawn : MonoBehaviour
{
    public RoomController roomController;

    //public int numberOfEnemies;
    public int numberOfRangedEnemies;
    public int numberOfExplosiveEnemies;
    public int numberOfMeleeEnemies;
    //public float spawnRadius = 5f;
    public float minDistance;
    public bool spawnEnemy;
    public bool checkSpawnEnemy;
    public bool spawnBoss;
    public GameObject rangedEnemy;
    public GameObject explosiveEnemy;
    public GameObject meleeEnemy;
    public GameObject boss;
    public float rectangleWidth;
    public float rectangleHeight;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Number of enemies that is going to spawn
        if (spawnEnemy)
        {
            List<GameObject> enemiesList = new List<GameObject>();

            for (int i = 1; i <= numberOfRangedEnemies; i++)
            {
                Vector3 spawnPosition = GenerateSpawnPosition();

                // Instantiate and add enemies to the list
                enemiesList.Add(Instantiate(rangedEnemy, spawnPosition, Quaternion.identity));

                if (i == numberOfRangedEnemies)
                {
                    spawnEnemy = false;
                    checkSpawnEnemy = true;
                }

                roomController.enemies = enemiesList.ToArray();
            }

            for (int i = 1; i <= numberOfExplosiveEnemies; i++)
            {
                Vector3 spawnPosition = GenerateSpawnPosition();

                // Instantiate and add enemies to the list
                enemiesList.Add(Instantiate(explosiveEnemy, spawnPosition, Quaternion.identity));

                if (i == numberOfExplosiveEnemies)
                {
                    spawnEnemy = false;
                    checkSpawnEnemy = true;
                }

                roomController.enemies = enemiesList.ToArray();
            }

            for (int i = 1; i <= numberOfMeleeEnemies; i++)
            {
                Vector3 spawnPosition = GenerateSpawnPosition();

                // Instantiate and add enemies to the list
                enemiesList.Add(Instantiate(meleeEnemy, spawnPosition, Quaternion.identity));

                if (i == numberOfMeleeEnemies)
                {
                    spawnEnemy = false;
                    checkSpawnEnemy = true;
                }

                roomController.enemies = enemiesList.ToArray();
            }
        }

        if (spawnBoss)
        {
            List<GameObject> enemiesList = new List<GameObject>();
            enemiesList.Add(Instantiate(boss, transform.position, Quaternion.identity));
            roomController.enemies = enemiesList.ToArray();
            spawnBoss = false;
        }
    }

    //functions to spawn the enemy
    public Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnPosition;

        do
        {
            float halfWidth = rectangleWidth / 2f;
            float halfHeight = rectangleHeight / 2f;

            float x = Random.Range(transform.position.x - halfWidth, transform.position.x + halfWidth);
            float y = Random.Range(transform.position.y - halfHeight, transform.position.y + halfHeight);

            spawnPosition = new Vector3(x, y, 0f);
        } while (CheckOverlap(spawnPosition));

        return spawnPosition;
    }

    public bool CheckOverlap(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, minDistance);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Walls")
            {
                return true;
            }
        }

        return false;
    }
}
