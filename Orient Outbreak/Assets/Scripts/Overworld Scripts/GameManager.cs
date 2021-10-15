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

    #region Quest System
    public void AddQuest(QuestName questName, string questDescription, int currentAmount, int requiredAmount)
    {
        questList.Add(new Quest(questName, questDescription, currentAmount, requiredAmount));
    }

    //Check if quest is in quest list

    //Check if quest is finished
    #endregion

    #region Inventory System
    public void AddToInventory(ItemName itemName)   //Method that adds an item to inventory
    {
        bool itemExist = false;

        //Check if itemName is already an Item in inventory
        for (int i = 0; i < inventory.Count; i++)
        {
            //If itemName exists in inventory, increment itemQuantity of existing Item 
            if (inventory[i].GetItemName() == itemName)
            {
                itemExist = true;
                inventory[i].SetItemQuantity(inventory[i].GetItemQuantity() + 1);
                return;
            }
        }

        if (!itemExist) //If itemName does not exist in inventory, create a new Item and add it to inventory
        {
            inventory.Add(new Item(itemName, 1));
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