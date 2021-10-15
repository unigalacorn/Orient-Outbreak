using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    #region Variables and Constructor
    [SerializeField] private QuestName questName;
    [SerializeField] private string questDescription;
    [SerializeField] private int currentAmount;
    [SerializeField] private int requiredAmount;

    public Quest(QuestName _questName, string _questDescription, int _currentAmount, int _requiredAmount)
    {
        questName = _questName;
        questDescription = _questDescription;
        currentAmount = _currentAmount;
        requiredAmount = _requiredAmount;
    }
    #endregion

    #region Public Getter/Setter
    public QuestName GetQuestName() => questName;               //questName public getter

    public string GetQuestDescription() => questDescription;    //questDescription public getter

    public int GetCurrentAmount() => currentAmount;             //currentAmount public getter         

    public int GetRequiredAmount() => requiredAmount;           //requiredAmount public getter

    public void SetCurrentAmount(int _currentAmount) => currentAmount = _currentAmount;     //currentAmount public setter

    public bool isQuestCompleted() => currentAmount == requiredAmount ? true : false;       //check if quest is completed
    #endregion
}

public enum QuestName 
{ 
    MainQuest,
    Antivaxx,
    DeliverFood,
    CleanTrash,
    LabGown,
    FixLine
}