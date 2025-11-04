using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeChoose : MonoBehaviour
{
    public void StoryMode()
    {
        SceneManager.LoadScene("StoryModeScene");
        GameManager.SwitchGameMode(GameMode.StoryMode);
    }

    public void ArenaMode()
    {
        SceneManager.LoadScene("ArenaModeScene");
        GameManager.SwitchGameMode(GameMode.ArenaMode);
    }

}
