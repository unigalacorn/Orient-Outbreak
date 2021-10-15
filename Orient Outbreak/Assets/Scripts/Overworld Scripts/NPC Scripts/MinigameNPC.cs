using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameNPC : MonoBehaviour
{
    #region Variables
    [SerializeField] private string minigameSceneName;
    private bool isPlayerInRange;
    #endregion

    #region Unity Methods
    private void Start()
    {
        isPlayerInRange = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && isPlayerInRange)
        {
            //Start Dialogue
            //GameManager.instance.UpdateGameState(GameState.Dialogue);     //Update Game State to Dialogue

            //If player accepts challenge, load scene with name "minigameScene"
            SceneManager.LoadScene(minigameSceneName);
            GameManager.instance.UpdateGameState(GameState.Minigame);       //Change to coroutine
        }
    }
    #endregion 

    #region On Collision Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameManager.instance.currentState == GameState.Exploration)
        {
            isPlayerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameManager.instance.currentState == GameState.Exploration)
        {
            isPlayerInRange = false;
        }
    }
    #endregion
}
