using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionSample : MonoBehaviour
{
    Condition isQuestDone = new Condition();
    public bool check;

    public string doThis(string thing)
    {
        return "did";
    }


    //bool isQuestDone = true;
    public bool IsQuest1Done(Condition isQuestDone)
    {

        return true;
    }

    private bool chayk()
    {
        return true;
    }

    public Condition IsQuest2Done(bool isfinished)
    {
        return null;
    }
}


