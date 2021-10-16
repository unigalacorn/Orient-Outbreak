using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for interacting with NPC
/// </summary>
public class NPCInteract : MonoBehaviour
{
    [Header("References")]
    public DialogueManager dialogueManager;
    public Dialogues dialogue;

    [Header("Variables")]
    protected bool isPlayerInRange;

    #region On Collision Methods
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameManager.instance.currentState == GameState.Exploration)
        {
            isPlayerInRange = true;
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameManager.instance.currentState == GameState.Exploration)
        {
            isPlayerInRange = false;
        }
    }
    #endregion
}
