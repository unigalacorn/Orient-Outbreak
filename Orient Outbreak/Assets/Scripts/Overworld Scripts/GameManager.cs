using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;

    [Header("Cutscene")]
    [SerializeField] private GameObject firstCutscene;
    [SerializeField] private GameObject missionAcomplishedCutscene;

    [Header("Singletons")]
    [SerializeField] private GameObject[] singletonArray;

    [Header("Game State")]
    public GameState currentState;

    [Header("Main Quest Flags")]
    //Immunity Booster
    public bool isImmunityBoosterDone = false;
    public bool isImmunityBoosterSuccess = false;
    public bool isImmunityBoosterFailed = false;
    [Space]
    //Shields Up
    public bool isShieldsUpDone = false;
    public bool isShieldsUpSuccess = false;
    public bool isShieldsUpFailed = false;
    [Space]
    //WerkIt
    public bool isWerkItDone = false;
    public bool isWerkItSuccess = false;
    public bool isWerkItFailed = false;
    [Space]
    //Deliver food
    public bool isFoodGiven = false;

    [Header("Items")]
    public bool fact1 = false;
    public bool fact2 = false;
    public bool fact3 = false;
    public bool fact4 = false;
    public bool fact5 = false;
    public bool fact6 = false;
    public bool fact7 = false;
    public bool fact8 = false;
    public bool fact9 = false;
    [Space]
    public bool trash1 = false;
    public bool trash2 = false;
    public bool trash3 = false;
    public bool trash4 = false;
    [Space]
    public bool labcoat = false;

    [Header("Player")]
    [Space]
    [SerializeField] private Animator playerAnim;
    public List<Quest> questList = new List<Quest>();
    public List<Item> inventory = new List<Item>();

    [Header("Day and Night Cycle")]
    public DayCycles dayCycle;
    public int day;
    public float cycleCurrentTime;
    public float cycleMaxTime = 60;
    #endregion

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

        UpdateGameState(GameState.Cutscene);     //Temp
    }

    private void Start()
    {
        firstCutscene.SetActive(true);
        day = 1;       //temp
        SetDayCycle((int)DayCycles.Morning); // start with sunrise state
    }
    #endregion

    #region Game Manager

    public void StartGame()
    {
        UpdateGameState(GameState.Exploration);
    }

    public void MissionAcomplished()
    {
        missionAcomplishedCutscene.SetActive(true);
    }

    public void EndGame()
    {
        SceneManager.LoadScene("MainMenuScene");

        for (int i = 0; i < singletonArray.Length; i++)
        {
            Destroy(singletonArray[i].gameObject);
        }
    }

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
                    playerAnim.SetFloat("Speed", 0);
                    break;
                case GameState.Exploration:
                    break;
                case GameState.Minigame:
                    break;
                case GameState.Victory:
                    //Insert Victory Screen
                    break;
            }
        }

        Debug.Log(newState);
    }
    #endregion

    #region Day and Night Cycle
    public void SetDayCycle(int _cycle) => dayCycle = (DayCycles)_cycle;
    public void SetDay(int _day) => day = _day;
    public void SetCycleCurrentTime(float _time) => cycleCurrentTime = _time;
    #endregion

    #region Quest System
    public void AddQuest(QuestName questName, int currentAmount, int requiredAmount)
    {
        questList.Add(new Quest(questName, currentAmount, requiredAmount));
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

    public void IncreaseAmount(QuestName questName)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].GetQuestName() == questName)
            {
                questList[i].IncreaseCurrentAmount();
            }
        }
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

    public bool AreRequirementsMet(QuestName questName) //Method that finds index of quest name and checks if it is finished
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].GetQuestName() == questName)
            {
                return questList[i].AreRequirementsMet();
            }
        }

        return false;
    }

    public void TurnInQuest(QuestName questName)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].GetQuestName() == questName)
            {
                questList[i].CompleteQuest();
            }
        }
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

    public int GetItemQuantity(ItemName itemName)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            //Find index and increase itemName's quantity
            if (inventory[i].GetItemName() == itemName)
            {
                return inventory[i].GetItemQuantity();
            }
        }

        return 0;
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

public enum Minigame
{
    ImmunityBooster,
    ShieldsUp,
    WerkIt
}