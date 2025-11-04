using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BossSounds : MonoBehaviour
{
    public AudioSource bossAudioSource;
    public AudioMixerGroup audioMixerGroupFootsteps;
    public AudioMixerGroup audioMixerGroupSword;

    //Footsteps
    public AudioClip[] bossFootstepsAudioClip;

    //Sword
    public AudioClip[] bossSwordAudioClip;

    public void PlayFootstepsAudio()
    {
        bossAudioSource.clip = bossFootstepsAudioClip[Random.Range(0, bossFootstepsAudioClip.Length)];
        bossAudioSource.outputAudioMixerGroup = audioMixerGroupFootsteps;
        bossAudioSource.Play();
    }

    public void StopFootstepsAudio()
    {
        bossAudioSource.Stop();
    }

    public void PlaySwordAudio()
    {
        bossAudioSource.clip = bossSwordAudioClip[Random.Range(0, bossSwordAudioClip.Length)];
        bossAudioSource.outputAudioMixerGroup = audioMixerGroupSword;
        bossAudioSource.Play();
    }

    public void StopSwordAudio()
    {
        bossAudioSource.Stop();
    }
}
