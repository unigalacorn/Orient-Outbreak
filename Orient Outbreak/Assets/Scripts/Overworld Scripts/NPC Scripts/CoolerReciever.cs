using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerReciever : MonoBehaviour
{
    #region Variables
    private bool isPlayerInRange;

    [Header("Dialogue References")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Dialogues dialogue;
    #endregion

    #region Unity Methods
    private void Start()
    {
        isPlayerInRange = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && isPlayerInRange && GameManager.instance.currentState != GameState.Dialogue)
        {
            Debug.Log(GameManager.instance.isImmunityBoosterDone);
            Debug.Log(GameManager.instance.isShieldsUpDone);
            Debug.Log(GameManager.instance.isWerkItDone);

            //Win Game
            if (GameManager.instance.isImmunityBoosterDone && GameManager.instance.isShieldsUpDone && GameManager.instance.isWerkItDone)
            {
                Debug.Log("in");
                GameManager.instance.UpdateGameState(GameState.Dialogue);
                dialogueManager.StartDialogue(dialogue.dialogueList[1]);
            }
            else
            {
                //Start Dialogue
                GameManager.instance.UpdateGameState(GameState.Dialogue);     //Update Game State to Dialogue
                dialogueManager.StartDialogue(dialogue.dialogueList[0]); // start first dialogue in dialogue list
            }
        }
    }
    #endregion 

    #region On Collision Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && GameManager.instance.currentState == GameState.Exploration)
        {
            isPlayerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && GameManager.instance.currentState == GameState.Exploration)
        {
            isPlayerInRange = false;
        }
    }
}
    #endregion
