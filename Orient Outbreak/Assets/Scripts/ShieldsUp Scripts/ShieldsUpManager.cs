using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShieldsUpManager : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] private Text collectedDisplay;
    [SerializeField] Image lifeFill;

    [Header("Player Properties")]
    float life = 1f; 

    [Header("Score")]
    private int score;
    private int itemsCollected;

    [SerializeField] GameObject startMinigamePanel;
    [SerializeField] private Animator cameraHolderAnim;

    void Start()
    {
        Time.timeScale = 0;
        itemsCollected = 0;
        startMinigamePanel.SetActive(true);
    }

    #region Game Manager
    private void GameOver()
    {
        Time.timeScale = 0; //game stops
        GameOverPanel.SetActive(true);

    }

    public void StartMinigame()
    {
        startMinigamePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    #endregion

    #region Health System
    public void DecreasePlayerHealth() //remove hearts
    {
        if (life > 0f)
        {
            life -= 0.2f;
            lifeFill.fillAmount = life;
        }

        if (life <= 0.2f)
            GameOver();
    }
    #endregion

    #region Score System
    public void CollectItem()
    {
        itemsCollected += 10;

        collectedDisplay.text = "Score: " + itemsCollected;
    }
    #endregion

    #region Public Method
    public void CameraShake()
    {
        cameraHolderAnim.SetTrigger("shake");
    }
    #endregion
}
