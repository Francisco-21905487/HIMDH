using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    //public GameObject musicManager;

    //private AudioSource musicSource;
    [SerializeField] private AudioMixer gameAudioMixer;
    [SerializeField] private Slider globalSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;

    private void Start()
    {
        ///musicManager = GameObject.FindWithTag("MusicManager");
       /// musicSource = musicManager.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("GlobalVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetGlobalVolume();
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("SoundEffectsVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSoundEffectsVolume();
        }
    }

    public void SetGlobalVolume()
    {
        float volume = globalSlider.value;
        gameAudioMixer.SetFloat("Global", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("GlobalVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        gameAudioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSoundEffectsVolume()
    {
        float volume = soundEffectsSlider.value;
        gameAudioMixer.SetFloat("SoundEffects", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SoundEffectsVolume", volume);
    }

    private void LoadVolume()
    {
        globalSlider.value = PlayerPrefs.GetFloat("GlobalVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume");

        SetGlobalVolume();
        SetMusicVolume();
        SetSoundEffectsVolume();
    }

    public void Back()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
}
