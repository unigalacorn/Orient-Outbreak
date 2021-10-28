using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Minigame minigame;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject GameOverPanel;

    private bool isGameOver = false;
    private static bool gameIsPaused = false;

    private void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            if (gameIsPaused)
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
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void HowToPlay()
    {

    }

    public void Settings()
    {

    }

    public void QuitMiniGame()
    {
        if (minigame == Minigame.ImmunityBooster)
        {
            GameManager.instance.isImmunityBoosterFailed = true;
        }
        else if (minigame == Minigame.ShieldsUp)
        {
            GameManager.instance.isShieldsUpFailed = true;
        }
        else if (minigame == Minigame.WerkIt)
        {
            GameManager.instance.isWerkItFailed = true;
        }

        GameManager.instance.UpdateGameState(GameState.Exploration);
        SceneLoader.instance.ChangeScene("Overworld Scene");
        Time.timeScale = 1f;
    }

    public void MinigameSuccessReturn()
    {
        if (minigame == Minigame.ImmunityBooster)
        {
            GameManager.instance.isImmunityBoosterSuccess = true;
        }
        else if (minigame == Minigame.ShieldsUp)
        {
            GameManager.instance.isShieldsUpSuccess = true;
        }
        else if (minigame == Minigame.WerkIt)
        {
            GameManager.instance.isWerkItSuccess = true;
        }


        GameManager.instance.UpdateGameState(GameState.Exploration);
        SceneLoader.instance.ChangeScene("Overworld Scene");
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Time.timeScale = 0; //game stops
        isGameOver = true;
        GameOverPanel.SetActive(true);
    }
}
