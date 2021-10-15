using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.AddQuest(QuestName.Antivaxx, "This quest bla blabla", 0, 12);
    }
}
