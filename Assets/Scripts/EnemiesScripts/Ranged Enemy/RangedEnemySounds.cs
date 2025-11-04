using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RangedEnemySounds : MonoBehaviour
{
    public AudioSource rangedEnemyAudioSource;
    public AudioMixerGroup audioMixerGroupFootsteps;
    public AudioMixerGroup audioMixerGroupArrow;

    //Footsteps
    public AudioClip[] rangedEnemyFootstepsAudioClip;

    //Arrow
    public AudioClip[] rangedEnemyArrowAudioClip;

    public void PlayFootstepsAudio()
    {
        rangedEnemyAudioSource.clip = rangedEnemyFootstepsAudioClip[Random.Range(0, rangedEnemyFootstepsAudioClip.Length)];
        rangedEnemyAudioSource.outputAudioMixerGroup = audioMixerGroupFootsteps;
        rangedEnemyAudioSource.Play();
    }

    public void StopFootstepsAudio()
    {
        rangedEnemyAudioSource.Stop();
    }

    public void PlayArrowAudio()
    {
        rangedEnemyAudioSource.clip = rangedEnemyArrowAudioClip[Random.Range(0, rangedEnemyArrowAudioClip.Length)];
        rangedEnemyAudioSource.outputAudioMixerGroup = audioMixerGroupArrow;
        rangedEnemyAudioSource.Play();
    }

    public void StopArrowAudio()
    {
        rangedEnemyAudioSource.Stop();
    }
}
