using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShieldsUpManager : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] private Text healthDisplay;
    [SerializeField] private Text collectedDisplay;

    [Header("Player Properties")]
    private int health;

    [Header("Score")]
    private int score;
    private int itemsCollected;

    void Start()
    {
        Time.timeScale = 1; 
        health = 3;
        itemsCollected = 0;
    }

    #region Game Manager

    private void GameOver()
    {
        Time.timeScale = 0; //game stops
        GameOverPanel.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("ShieldsUp");
    }
    #endregion

    #region Health System
    public void DecreasePlayerHealth()
    {
        health -= 1;

        healthDisplay.text = "Health: " + (health);

        if (health <= 0)
            GameOver();
    }
    #endregion

    #region Score System
    public void CollectItem()
    {
        itemsCollected += 1;

        collectedDisplay.text = "Items Collected: " + itemsCollected;
    }
    #endregion
}
