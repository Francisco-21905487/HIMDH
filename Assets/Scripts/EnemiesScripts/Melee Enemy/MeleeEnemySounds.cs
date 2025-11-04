using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MeleeEnemySounds : MonoBehaviour
{
    public AudioSource enemyAudioSource;
    public AudioMixerGroup audioMixerGroupFootsteps;
    public AudioMixerGroup audioMixerGroupSword;

    //Footsteps
    public AudioClip[] meleeEnemyFootstepsAudioClip;

    //Sword
    public AudioClip[] enemySwordAudioClip;

    public void PlayFootstepsAudio()
    {
        enemyAudioSource.clip = meleeEnemyFootstepsAudioClip[Random.Range(0, meleeEnemyFootstepsAudioClip.Length)];
        enemyAudioSource.outputAudioMixerGroup = audioMixerGroupFootsteps;
        enemyAudioSource.Play();
    }

    public void StopFootstepsAudio()
    {
        enemyAudioSource.Stop();
    }

    public void PlaySwordAudio()
    {
        enemyAudioSource.clip = enemySwordAudioClip[Random.Range(0, enemySwordAudioClip.Length)];
        enemyAudioSource.outputAudioMixerGroup = audioMixerGroupSword;
        enemyAudioSource.Play();
    }

    public void StopSwordAudio()
    {
        enemyAudioSource.Stop();
    }
}
