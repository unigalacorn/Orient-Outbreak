using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;

    [Header("Game State")]
    public GameState currentState;

    [Header("Player")]
    [Space]
    [SerializeField] private SpriteRenderer playerSprite;
    public List<Quest> questList = new List<Quest>();
    public List<Item> inventory = new List<Item>();

    [Header("Day and Night Cycle")]
    public DayCycles dayCycle;
    public int day;
    public float cycleCurrentTime;
    public float cycleMaxTime = 60;
    #endregion

    // Testing
    [Header("Flags")]
    #region Flags
    public bool hasInteractedWithTestNPC1 = false;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        //Initialize Singleton
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateGameState(GameState.Exploration);     //Temp
        day = 1;       //temp
        SetDayCycle((int)DayCycles.Morning); // start with sunrise state
    }
    #endregion

    #region Game Manager
    public void UpdateGameState(GameState newState) //methods that updates the state of the game
    {
        if (currentState != newState)
        {
            currentState = newState;

            switch (currentState)
            {
                case GameState.Cutscene:
                    break;
                case GameState.Dialogue:
                    break;
                case GameState.Exploration:
                    playerSprite.enabled = true;
                    break;
                case GameState.Minigame:
                    playerSprite.enabled = false;
                    break;
                case GameState.Victory:
                    //Insert Victory Screen
                    break;
            }
        }
    }
    #endregion

    #region Day and Night Cycle
    public void SetDayCycle(int _cycle) => dayCycle = (DayCycles)_cycle;
    public void SetDay(int _day) => day = _day;
    public void SetCycleCurrentTime(float _time) => cycleCurrentTime = _time;
    #endregion

    #region Quest System
    public void AddQuest(QuestName questName, string questDescription, int currentAmount, int requiredAmount)
    {
        questList.Add(new Quest(questName, questDescription, currentAmount, requiredAmount));
    }

    public bool DoesQuestExist(QuestName questName)     //Method that checks if questName is in questList
    {
        for (int i = 0; i < questList.Count; i ++)
        {
            if (questList[i].GetQuestName() == questName)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsQuestFinished(QuestName questName) //Method that finds index of quest name and checks if it is finished
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].GetQuestName() == questName)
            {
                return questList[i].IsQuestCompleted();
            }
        }

        return false;
    }
    #endregion

    #region Inventory System
    public void AddToInventory(ItemName itemName)   //Method that adds an item to inventory
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            //Find index and increase itemName's quantity
            if (inventory[i].GetItemName() == itemName)
            {
                inventory[i].SetItemQuantity(inventory[i].GetItemQuantity() + 1);
                return;
            }
        }
    }
    #endregion
}

public enum GameState
{
    Cutscene,
    Dialogue,
    Exploration,
    Minigame,
    Victory
}