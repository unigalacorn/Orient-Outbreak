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
		public int requiredAmount;
		private int currentAmount;

		public override void Trigger()
		{
			if (questName == QuestName.Antivaxx)
            {
				currentAmount = GameManager.instance.GetItemQuantity(ItemName.Facts);
				GameManager.instance.AddQuest(questName, currentAmount, requiredAmount);
			}
			else if (questName == QuestName.CleanTrash)
            {
				GameManager.instance.AddQuest(questName, 0, requiredAmount);
			}
			else if (questName == QuestName.DeliverFood)
            {
				GameManager.instance.AddQuest(questName, 0, requiredAmount);
			}
			else if (questName == QuestName.LabGown)
            {
				GameManager.instance.AddQuest(questName, 0, requiredAmount);
			}
		}
	}
}