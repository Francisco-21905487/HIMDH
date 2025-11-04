using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathReturn : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
