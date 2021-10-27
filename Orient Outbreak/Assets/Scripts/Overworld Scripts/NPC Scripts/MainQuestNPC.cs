using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestNPC : MonoBehaviour
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

        #region Immunity Booster
        if (minigame == Minigame.ImmunityBooster)
        {
            if (GameManager.instance.isImmunityBoosterSuccess) 
            {
                GameManager.instance.UpdateGameState(GameState.Dialogue);
                //Play minigame success
                GameManager.instance.isImmunityBoosterSuccess = false;
            }
            else if (GameManager.instance.isImmunityBoosterFailed)
            {
                GameManager.instance.UpdateGameState(GameState.Dialogue);
                //Play minigame fail
                GameManager.instance.isImmunityBoosterFailed = false;
            }
        }
        #endregion

        #region Shields Up
        if (minigame == Minigame.ShieldsUp)
        {
            if (GameManager.instance.isShieldsUpSuccess)
            {
                GameManager.instance.UpdateGameState(GameState.Dialogue);
                //Play minigame success
                GameManager.instance.isShieldsUpSuccess = false;
            }
            else if (GameManager.instance.isShieldsUpFailed)
            {
                GameManager.instance.UpdateGameState(GameState.Dialogue);
                //Play minigame fail
                GameManager.instance.isShieldsUpFailed = false;
            }
        }
        #endregion

        #region Werk It
        if (minigame == Minigame.WerkIt)
        {
            if (GameManager.instance.isWerkItSuccess)
            {
                GameManager.instance.UpdateGameState(GameState.Dialogue);
                //Play minigame success
                GameManager.instance.isWerkItSuccess = false;
            }
            else if (GameManager.instance.isWerkItFailed)
            {
                GameManager.instance.UpdateGameState(GameState.Dialogue);
                //Play minigame fail
                GameManager.instance.isWerkItFailed = false;
            }
        }
        #endregion
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && isPlayerInRange && GameManager.instance.currentState != GameState.Dialogue)
        {
            #region Immunity Booster
            if (minigame == Minigame.ImmunityBooster)
            {
                if (GameManager.instance.dayCycle == DayCycles.Sunrise || GameManager.instance.dayCycle == DayCycles.Morning || GameManager.instance.dayCycle == DayCycles.Sunset)
                {
                    //During the Day
                    GameManager.instance.UpdateGameState(GameState.Dialogue);     
                    //Play During the Day
                }
                else if (GameManager.instance.dayCycle == DayCycles.Night || GameManager.instance.dayCycle == DayCycles.Midnight)
                {
                    //Accepting Quest
                    if (!GameManager.instance.isImmunityBoosterDone)
                    {
                        GameManager.instance.UpdateGameState(GameState.Dialogue);
                        //Play accepting quest
                    }
                    //Finished Quest
                    if (GameManager.instance.isImmunityBoosterDone)
                    {
                        GameManager.instance.UpdateGameState(GameState.Dialogue);
                        //Play accepting quest
                    }
                }
            }
            #endregion

            #region Shields Up
            else if (minigame == Minigame.ShieldsUp)
            {
                if (GameManager.instance.dayCycle == DayCycles.Sunrise || GameManager.instance.dayCycle == DayCycles.Morning || GameManager.instance.dayCycle == DayCycles.Sunset)
                {
                    //Accepting Quest
                    if (!GameManager.instance.isShieldsUpDone)
                    {
                        GameManager.instance.UpdateGameState(GameState.Dialogue);
                        //Play accepting quest
                    }
                    //Finished Quest
                    if (GameManager.instance.isShieldsUpDone)
                    {
                        GameManager.instance.UpdateGameState(GameState.Dialogue);
                        //Play accepting quest
                    }
                }
                else if (GameManager.instance.dayCycle == DayCycles.Night || GameManager.instance.dayCycle == DayCycles.Midnight)
                {
                    //During the Night
                    GameManager.instance.UpdateGameState(GameState.Dialogue);
                    //Play During the Day
                }
            }
            #endregion

            #region Werk It
            else if (minigame == Minigame.WerkIt)
            {
                if (GameManager.instance.dayCycle == DayCycles.Sunrise || GameManager.instance.dayCycle == DayCycles.Morning || GameManager.instance.dayCycle == DayCycles.Sunset)
                {
                    //Accepting Quest
                    if (!GameManager.instance.isWerkItDone)
                    {
                        GameManager.instance.UpdateGameState(GameState.Dialogue);
                        //Play accepting quest
                    }
                    //Finished Quest
                    if (GameManager.instance.isWerkItDone)
                    {
                        GameManager.instance.UpdateGameState(GameState.Dialogue);
                        //Play accepting quest
                    }
                }
                else if (GameManager.instance.dayCycle == DayCycles.Night || GameManager.instance.dayCycle == DayCycles.Midnight)
                {
                    //During the Night
                    GameManager.instance.UpdateGameState(GameState.Dialogue);
                    //Play During the Day
                }
            }
            #endregion
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
