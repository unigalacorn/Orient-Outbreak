using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGiverNPC : MonoBehaviour
{
    #region Variables

    private bool isPlayerInRange;
    [Header("Side Quest NPC")]
    [SerializeField] private QuestName sideQuest;

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
            GameManager.instance.UpdateGameState(GameState.Dialogue);     //Update Game State to Dialogue

            //Before Quest
            if (!GameManager.instance.DoesQuestExist(sideQuest))
            {
                dialogueManager.StartDialogue(dialogue.dialogueList[0]);
            }
            //Quest Active, Give food
            else if (!GameManager.instance.IsQuestFinished(sideQuest) && !GameManager.instance.AreRequirementsMet(sideQuest) && !GameManager.instance.isFoodGiven)
            {
                GameManager.instance.isFoodGiven = true;
                dialogueManager.StartDialogue(dialogue.dialogueList[1]);
            }
            //Food Given
            else if (GameManager.instance.isFoodGiven == true)
            {
                dialogueManager.StartDialogue(dialogue.dialogueList[2]);
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
    #endregion
}
