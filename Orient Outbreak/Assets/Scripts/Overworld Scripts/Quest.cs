using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    #region Variables and Constructor
    [SerializeField] private QuestName questName;
    [SerializeField] private int currentAmount;
    [SerializeField] private int requiredAmount;
    [SerializeField] private bool isQuestCompleted;

    public Quest(QuestName _questName, int _currentAmount, int _requiredAmount)
    {
        questName = _questName;
        currentAmount = _currentAmount;
        requiredAmount = _requiredAmount;
        isQuestCompleted = false;
    }
    #endregion

    #region Public Getter/Setter
    public QuestName GetQuestName() => questName;               //questName public getter

    public int GetCurrentAmount() => currentAmount;             //currentAmount public getter         

    public int GetRequiredAmount() => requiredAmount;           //requiredAmount public getter

    public void SetCurrentAmount(int _currentAmount) => currentAmount = _currentAmount;     //currentAmount public setter

    public bool AreRequirementsMet() => currentAmount == requiredAmount ? true : false;       //check if requirements are met to complete quest

    public bool IsQuestCompleted() => isQuestCompleted;      //check if quest is completed

    public void CompleteQuest() => isQuestCompleted = true;     //complete quest
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