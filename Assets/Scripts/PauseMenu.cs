using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuState;


    // update to refere to functions
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    // resume and pause function
    public void Resume()
    {
        pauseMenuState.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuState.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    // menu and quit button functionality
    public void LoadMenu()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
        GameIsPaused = (false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
