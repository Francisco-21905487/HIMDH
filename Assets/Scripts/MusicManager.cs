using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicManager = GameObject.FindGameObjectsWithTag("MusicManager");
        if (musicManager.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
