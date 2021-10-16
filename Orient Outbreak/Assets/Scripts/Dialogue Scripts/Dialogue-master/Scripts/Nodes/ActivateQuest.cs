using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
	public class ActivateQuest : DialogueBaseNode
	{
		//GameManager gm;

		public QuestName questName;
		public string questDescription;
		public int currentAmount;
		public int requiredAmount;

		public override void Trigger()
		{
			GameManager.instance.hasInteractedWithTestNPC1 = true; 
			Debug.Log(GameManager.instance.hasInteractedWithTestNPC1);
			GameManager.instance.AddQuest(questName, questDescription, currentAmount, requiredAmount);
			Debug.Log("ENTERED!!");
		}
	}
}