using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private ShieldsUpManager shieldsUpManager;
    [SerializeField] private GameObject finishGamePanel;
    [SerializeField] private Text finalScore;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("finish line");
            Time.timeScale = 0; //game stops
            finishGamePanel.SetActive(true);
            finalScore.text = "Score : " + shieldsUpManager.GetScore();
        }
    }
}
