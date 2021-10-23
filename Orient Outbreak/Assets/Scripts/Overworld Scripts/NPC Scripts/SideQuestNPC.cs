using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideQuestNPC : MonoBehaviour
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

            //Accepting Quest Dialogue
            if (!GameManager.instance.DoesQuestExist(sideQuest))        
            {
                dialogueManager.StartDialogue(dialogue.dialogueList[0]); 
            }
            //Waiting For Quest Dialogue
            else if (!GameManager.instance.IsQuestFinished(sideQuest) && !GameManager.instance.AreRequirementsMet(sideQuest))     
            {
                dialogueManager.StartDialogue(dialogue.dialogueList[1]);
            }
            //Turning In Quest
            else if (!GameManager.instance.IsQuestFinished(sideQuest) && GameManager.instance.AreRequirementsMet(sideQuest))     //If quest exists and not completed
            {
                GameManager.instance.TurnInQuest(sideQuest);
                dialogueManager.StartDialogue(dialogue.dialogueList[2]);
            }
            //Finished Quest
            else if (GameManager.instance.IsQuestFinished(sideQuest))       
            {
                dialogueManager.StartDialogue(dialogue.dialogueList[3]);
            }
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
