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
    [SerializeField] private string itemRef;

    [Header("Dialogue References")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Dialogues dialogue;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (itemRef == "fact1" && GameManager.instance.fact1)
            Destroy(this);
        else if (itemRef == "fact2" && GameManager.instance.fact2)
            Destroy(this);
        else if (itemRef == "fact3" && GameManager.instance.fact3)
            Destroy(this);
        else if (itemRef == "fact4" && GameManager.instance.fact4)
            Destroy(this);
        else if (itemRef == "fact5" && GameManager.instance.fact5)
            Destroy(this);
        else if (itemRef == "fact6" && GameManager.instance.fact6)
            Destroy(this);
        else if (itemRef == "fact7" && GameManager.instance.fact7)
            Destroy(this);
        else if (itemRef == "fact8" && GameManager.instance.fact8)
            Destroy(this);
        else if (itemRef == "fact9" && GameManager.instance.fact9)
            Destroy(this);
        else if (itemRef == "trash1" && GameManager.instance.trash1)
            Destroy(this);
        else if (itemRef == "trash2" && GameManager.instance.trash2)
            Destroy(this);
        else if (itemRef == "trash3" && GameManager.instance.trash3)
            Destroy(this);
        else if (itemRef == "trash4" && GameManager.instance.trash4)
            Destroy(this);
        else if (itemRef == "labgown" && GameManager.instance.fact1)
            Destroy(this);
    }

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

                if (itemRef == "fact1")
                {
                    GameManager.instance.fact1 = true;
                    Destroy(this);
                }
                else if (itemRef == "fact2")
                {
                    GameManager.instance.fact2 = true;
                    Destroy(this);
                }
                else if (itemRef == "fact3")
                {
                    GameManager.instance.fact3 = true;
                    Destroy(this);
                }
                else if (itemRef == "fact4")
                {
                    GameManager.instance.fact4 = true;
                    Destroy(this);
                }
                else if (itemRef == "fact5")
                {
                    GameManager.instance.fact5 = true;
                    Destroy(this);
                }
                else if (itemRef == "fact6")
                {
                    GameManager.instance.fact6 = true;
                    Destroy(this);
                }
                else if (itemRef == "fact7")
                {
                    GameManager.instance.fact7 = true;
                    Destroy(this);
                }
                else if (itemRef == "fact8")
                {
                    GameManager.instance.fact8 = true;
                    Destroy(this);
                }
                else if (itemRef == "fact9")
                {
                    GameManager.instance.fact9 = true;
                    Destroy(this);
                }


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
                    else if(itemRef == "trash1")
                    {
                        GameManager.instance.trash1 = true;
                        Destroy(this);
                    }
                    else if(itemRef == "trash2")
                    {
                        GameManager.instance.trash2 = true;
                        Destroy(this);
                    }
                    else if(itemRef == "trash3")
                    {
                        GameManager.instance.trash3 = true;
                        Destroy(this);
                    }
                    else if(itemRef == "trash4")
                    {
                        GameManager.instance.trash4 = true;
                        Destroy(this);
                    }
                    else if (itemRef == "labcoat")
                    {
                        GameManager.instance.labcoat = true;
                        Destroy(this);
                    }
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
