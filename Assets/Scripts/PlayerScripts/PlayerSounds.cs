using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSounds : MonoBehaviour
{
    //Audio Source
    public AudioSource playerAudioSource;
    public AudioMixerGroup audioMixerGroupFootsteps;
    public AudioMixerGroup audioMixerGroupDash;
    public AudioMixerGroup audioMixerGroupArrow;
    public AudioMixerGroup audioMixerGroupSword;
    public AudioMixerGroup audioMixerGroupScythe;

    //Footsteps
    public AudioClip[] playerFootstepsAudioClip;

    //Dash
    public AudioClip playerDashAudioClip;

    //Arrow
    public AudioClip[] playerArrowAudioClip;

    //Sword
    public AudioClip[] playerSwordAudioClip;

    //Scythe
    public AudioClip playerScytheAudioClip;

    public void PlayFootstepsAudio()
    {
        playerAudioSource.clip = playerFootstepsAudioClip[Random.Range(0, playerFootstepsAudioClip.Length)];
        playerAudioSource.outputAudioMixerGroup = audioMixerGroupFootsteps;
        playerAudioSource.Play();
    }

    public void StopFootstepsAudio()
    {
        playerAudioSource.Stop();
    }

    public void PlayDashAudio()
    {
        playerAudioSource.clip = playerDashAudioClip;
        playerAudioSource.outputAudioMixerGroup = audioMixerGroupDash;
        playerAudioSource.Play();
    }

    public void StopDashAudio()
    {
        playerAudioSource.Stop();
    }

    public void PlayArrowAudio()
    {
        playerAudioSource.clip = playerArrowAudioClip[Random.Range(0, playerArrowAudioClip.Length)];
        playerAudioSource.outputAudioMixerGroup = audioMixerGroupArrow;
        playerAudioSource.Play();
    }

    public void StopArrowAudio()
    {
        playerAudioSource.Stop();
    }

    public void PlaySwordAudio()
    {
        playerAudioSource.clip = playerSwordAudioClip[Random.Range(0, playerSwordAudioClip.Length)];
        playerAudioSource.outputAudioMixerGroup = audioMixerGroupSword;
        playerAudioSource.Play();
    }

    public void StopSwordAudio()
    {
        playerAudioSource.Stop();
    }

    public void PlayScytheAudio()
    {
        playerAudioSource.clip = playerScytheAudioClip;
        playerAudioSource.outputAudioMixerGroup = audioMixerGroupScythe;
        playerAudioSource.Play();
    }

    public void StopScytheAudio()
    {
        playerAudioSource.Stop();
    }
}
