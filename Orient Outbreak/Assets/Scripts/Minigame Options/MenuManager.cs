using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject GameOverPanel;

    private bool isGameOver = false;
    private static bool GameIsPaused = false;

    private void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene("ShieldsUp");
    }

    public void HowToPlay()
    {

    }

    public void Settings()
    {

    }

    public void QuitMiniGame()
    {
        SceneManager.LoadScene("OverworldScene");
    }

    public void GameOver()
    {
        Time.timeScale = 0; //game stops
        isGameOver = true;
        GameOverPanel.SetActive(true);
    }
}
