using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public AudioClip newMusic;
    public static GameManager Instance;
    public static GameMode currentGameMode;
    public GameMode gameMode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentGameMode = gameMode;
        /*GameObject musicManager = GameObject.FindGameObjectWithTag("MusicManager");
        if (musicManager != null)
        {
            musicManager.GetComponent<AudioSource>().Stop();
            musicManager.GetComponent<AudioSource>().clip = newMusic;
            musicManager.GetComponent<AudioSource>().Play();
        }*/
    }

    public static void SwitchGameMode(GameMode mode)
    {
        currentGameMode = mode;
    }
}

public enum GameMode
{
    StoryMode,
    ArenaMode,
}