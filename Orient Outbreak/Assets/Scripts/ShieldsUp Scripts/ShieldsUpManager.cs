using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShieldsUpManager : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Text collectedDisplay;
    [SerializeField] Image lifeFill;

    [Header("Player Properties")]
    float life = 1f; 

    [Header("Score")]
    private int itemsCollected;

    [SerializeField] private Animator cameraHolderAnim;
    [SerializeField] private MenuManager menuManager;

    void Start()
    {
        Time.timeScale = 1f;
        itemsCollected = 0;
    }

    #region Health System
    public void DecreasePlayerHealth() //remove hearts
    {
        if (life > 0f)
        {
            life -= 0.2f;
            lifeFill.fillAmount = life;
        }

        if (life <= 0.2f)
            menuManager.GameOver();
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
        //cameraHolderAnim.SetTrigger("shake");
    }

    public string GetScore()
    {
        string score = itemsCollected.ToString();
        return score;
    }
    #endregion
}
