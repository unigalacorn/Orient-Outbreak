using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraInteract : NPCInteract
{
    #region Unity Methods
    void Start()
    {
        isPlayerInRange = false;
    }

    private void Update()
    { 
        if (isPlayerInRange && Input.GetKeyUp("space"))
        {
            isPlayerInRange = false;
            GameManager.instance.UpdateGameState(GameState.Dialogue);

            // INSERT CONDITIONS wHICH DIALOGUE TO ACTIVATE

            //// Example 
            // If player has interacted with TestNPC1 before -> Dialogue 0, if not -> Dialogue 1
            
            if (GameManager.instance.hasInteractedWithTestNPC1 == false)
            {
                dialogueManager.StartDialogue(dialogue.dialogueList[0]);
                GameManager.instance.hasInteractedWithTestNPC1 = true;
            }
            else
            {
                dialogueManager.StartDialogue(dialogue.dialogueList[1]);
            }
            ////
        }
    }
    #endregion
}
