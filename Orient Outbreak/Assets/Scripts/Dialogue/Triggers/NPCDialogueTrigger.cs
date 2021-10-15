using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for choosing which dialogue to show for each NPC
/// </summary>
public class NPCDialogueTrigger : MonoBehaviour
{
    [Header("References")]
    public DialogueManager dialogueManager;
    public Dialogues dialogue;

    // Insert gameobjects where variables to be checked reside in
    // (Example) public GameObject player;
    public GameObject player;
    
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Triggers
            // Add conditions to choose which dialogue to execute


            // Trigger: Quest 1 finished (example)
            //if(isQuest1Finished){
            // parser.StartDialogue(dialogue.dialogueList[1]); return;}


            // Trigger: No condition
            //parser.StartDialogue(dialogue.dialogueList[0]);
        }
    }
}
