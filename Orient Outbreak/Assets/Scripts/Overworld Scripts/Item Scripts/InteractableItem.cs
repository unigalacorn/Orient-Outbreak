using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    #region Variables
    private bool isPlayerInRange;
    [Header("Item Info")]
    [SerializeField] private ItemName item;
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

            //Facts
            if (item == ItemName.Facts)
            {
                dialogueManager.StartDialogue(dialogue.dialogueList[0]);
                Destroy(gameObject);  //Destory object once retrieved.
            }

            //Lab Gown or Trash
            else if (item == ItemName.LabGown || item == ItemName.Trash)
            {
                // If side quest does not exist unable to retrieve item
                if (!GameManager.instance.DoesQuestExist(sideQuest))
                {
                    dialogueManager.StartDialogue(dialogue.dialogueList[0]);
                }
                //If side quest exist lab gown is obtainable
                else
                {
                    dialogueManager.StartDialogue(dialogue.dialogueList[1]);

                    if (item == ItemName.Trash)
                    {
                        GameManager.instance.IncreaseAmount(sideQuest);
                    }

                    Destroy(gameObject);  //Destory object once retrieved.
                }
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
