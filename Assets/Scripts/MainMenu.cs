using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 1;
    }

    public void PlayGame()
    {
        ScoreManager.instance.ResetScore();
        SceneManager.LoadScene("DemmyScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
