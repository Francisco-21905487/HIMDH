using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ExplosiveEnemySounds : MonoBehaviour
{
    public AudioSource explosiveEnemyAudioSource;
    public AudioMixerGroup audioMixerGroupFootsteps;
    public AudioMixerGroup audioMixerGroupExplosion;

    //Footsteps
    public AudioClip[] explosiveEnemyFootstepsAudioClip;

    //Explosive
    public AudioClip explosiveEnemyExplosionAudioClip;

    public void PlayFootstepsAudio()
    {
        explosiveEnemyAudioSource.clip = explosiveEnemyFootstepsAudioClip[Random.Range(0, explosiveEnemyFootstepsAudioClip.Length)];
        explosiveEnemyAudioSource.outputAudioMixerGroup = audioMixerGroupFootsteps;
        explosiveEnemyAudioSource.Play();
    }

    public void StopFootstepsAudio()
    {
        explosiveEnemyAudioSource.Stop();
    }

    public void PlayExplosionAudio()
    {
        explosiveEnemyAudioSource.clip = explosiveEnemyExplosionAudioClip;
        explosiveEnemyAudioSource.outputAudioMixerGroup = audioMixerGroupExplosion;
        explosiveEnemyAudioSource.Play();
    }

    public void StopExplosionAudio()
    {
        explosiveEnemyAudioSource.Stop();
    }
}
