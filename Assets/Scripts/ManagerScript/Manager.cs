using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }
    public GameObject ChestPrefab;
    public Transform[] ChestsSpawnPositions;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There's more than one Manager" + transform + " - " + Instance);
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnChests();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnChests()
    {
        foreach (Transform spawnPosition in ChestsSpawnPositions)
        {
            Instantiate(ChestPrefab, spawnPosition.position, spawnPosition.rotation);
        }
    }
}
