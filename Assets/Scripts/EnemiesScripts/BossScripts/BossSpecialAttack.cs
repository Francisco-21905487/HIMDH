using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BossSpecialAttack : MonoBehaviour
{
    public AudioClip[] lightningAudioClip;
    public AudioSource lightningAudio;
    public AudioMixerGroup audioMixerGroupZap;
    public bool canDestroy;

    private void PlayLightningAudio()
    {
        lightningAudio.clip = lightningAudioClip[Random.Range(0, lightningAudioClip.Length)];
        lightningAudio.outputAudioMixerGroup = audioMixerGroupZap;
        lightningAudio.Play();
    }

    private void StopLightningAudio()
    {
        lightningAudio.Stop();
    }

    private void DestroyLightning()
    {
        GameObject boss = GameObject.Find("Boss(Clone)");

        if(boss != null)
        {
            BossBehavior bossBehavior = boss.GetComponent<BossBehavior>();
            if (bossBehavior != null)
            {
                bossBehavior.canDestroyLightning = true;
            }
            else
            {
                Debug.LogWarning("No BossBehavior class found");
            }
        }
        else
        {
            Debug.LogWarning("No boss found");
        }
        
    }
}
