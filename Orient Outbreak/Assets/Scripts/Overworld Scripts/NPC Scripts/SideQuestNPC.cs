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
            //Start Dialogue
            GameManager.instance.UpdateGameState(GameState.Dialogue);     //Update Game State to Dialogue

            if (!GameManager.instance.DoesQuestExist(sideQuest))        //If quest does not exist
            {
                //Start dialogue where npc offers quest
            }
            else if (!GameManager.instance.IsQuestFinished(sideQuest))     //If quest exists and not completed
            {
                //Start dialogue of npc waiting for the quest to be completed
            }
            else if (GameManager.instance.IsQuestFinished(sideQuest))       //If quest exists and completed
            {
                //Start dialogue of npc thanking the player
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
