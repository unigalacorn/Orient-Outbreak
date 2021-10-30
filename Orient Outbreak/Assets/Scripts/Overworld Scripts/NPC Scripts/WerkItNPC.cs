using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerkItNPC : MonoBehaviour
{
    #region Variables
    private bool isPlayerInRange;

    [Header("Main Quest NPC")]
    [SerializeField] private Minigame minigame;

    [Header("Dialogue References")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Dialogues dialogue;
    #endregion

    #region Unity Methods
    private void Start()
    {
        isPlayerInRange = false;

        if (GameManager.instance.isWerkItSuccess || GameManager.instance.isWerkItFailed)
        {
            GameManager.instance.UpdateGameState(GameState.Dialogue);
        }

        StartCoroutine(StartDialogue());
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && isPlayerInRange && GameManager.instance.currentState != GameState.Dialogue)
        {
            GameManager.instance.UpdateGameState(GameState.Dialogue);

            if (GameManager.instance.dayCycle == DayCycles.Night || GameManager.instance.dayCycle == DayCycles.Midnight)
            {
                //Come Back
                dialogueManager.StartDialogue(dialogue.dialogueList[3]);
            }
            else if (GameManager.instance.dayCycle == DayCycles.Sunrise || GameManager.instance.dayCycle == DayCycles.Morning || GameManager.instance.dayCycle == DayCycles.Afternoon || GameManager.instance.dayCycle == DayCycles.Sunset)
            {
                //Minigame
                if (!GameManager.instance.isWerkItDone)
                {
                    dialogueManager.StartDialogue(dialogue.dialogueList[4]);
                }
                //Play Again
                if (GameManager.instance.isWerkItDone)
                {
                    dialogueManager.StartDialogue(dialogue.dialogueList[5]);
                }
            }
        }
    }
    #endregion

    #region Coroutine
    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(0.05f);

        if (GameManager.instance.isWerkItSuccess && !GameManager.instance.isWerkItDone)
        {
            GameManager.instance.isWerkItDone = true;

            //Play First Success
            dialogueManager.StartDialogue(dialogue.dialogueList[0]);

            GameManager.instance.isWerkItSuccess = false;
        }
        else if (GameManager.instance.isWerkItSuccess && GameManager.instance.isWerkItDone)
        {
            //Play Success
            dialogueManager.StartDialogue(dialogue.dialogueList[1]);
            GameManager.instance.isWerkItSuccess = false;
        }
        else if (GameManager.instance.isWerkItFailed)
        {
            //Play minigame fail
            dialogueManager.StartDialogue(dialogue.dialogueList[2]);

            GameManager.instance.isWerkItFailed = false;
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
