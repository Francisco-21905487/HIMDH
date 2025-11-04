using UnityEngine;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ModeChoose");
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}