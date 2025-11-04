using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusic : MonoBehaviour
{
    public AudioClip newMusic;

    private void Start()
    {
        GameObject musicManager = GameObject.FindGameObjectWithTag("MusicManager");
        if (musicManager.GetComponent<AudioSource>().isPlaying == true)
        {
            musicManager.GetComponent<AudioSource>().Stop();
            musicManager.GetComponent<AudioSource>().clip = newMusic;
            musicManager.GetComponent<AudioSource>().Play();
        }
        
    }
}
