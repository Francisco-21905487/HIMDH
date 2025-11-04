using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void Volume()
    {
        SceneManager.LoadScene("VolumeSettingsMenu");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
