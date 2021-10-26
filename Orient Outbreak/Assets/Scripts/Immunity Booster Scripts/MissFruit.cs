using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissFruit : MonoBehaviour
{
    [SerializeField] private ImmunityBoosterManager gameManagerScript;
    [SerializeField] private GameObject finishGamePanel;
    [SerializeField] private Text scoreText;
    [SerializeField] private ImmunityBoosterManager minigameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            gameManagerScript.missFruit();
        }

        if (collision.CompareTag("End Flag"))
        {
            finishGamePanel.SetActive(true);
            scoreText.text = "Score: " + minigameManager.GetScore();
        }
    }
}
