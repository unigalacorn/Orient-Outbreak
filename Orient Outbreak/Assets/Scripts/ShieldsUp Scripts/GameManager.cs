using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; //normal speed
    }

    public void GameOver()
    {
        Time.timeScale = 0; //game stops
        GameOverPanel.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ShieldsUp");
    }
}
