using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuraDialogueTrigger : NPCDialogueTrigger
{

    // Temp trigger, since wala pay player
    void Start()
    {
        //PlayerFlags flags = player.GetComponent<PlayerFlags>();

        //if (flags.hasMetTanod)
        //{
        //    dialogueManager.StartDialogue(dialogue.dialogueList[1]);
        //    return;
        //}
        //dialogueManager.StartDialogue(dialogue.dialogueList[0]);
    }

    // Actual trigger
    protected override void OnCollisionEnter2D(Collision2D collision)
    {  
        if (collision.gameObject.tag == "Player")
        {
            PlayerFlags flags = collision.gameObject.GetComponent<PlayerFlags>();


            // Add conditions to choose which dialogue to execute
            if (flags.hasMetTanod)
            {
                dialogueManager.StartDialogue(dialogue.dialogueList[1]);
                return;
            }

            dialogueManager.StartDialogue(dialogue.dialogueList[0]);
        }
    }
}
