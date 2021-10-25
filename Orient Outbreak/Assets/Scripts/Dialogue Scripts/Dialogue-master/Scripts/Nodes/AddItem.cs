using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
	public class AddItem : DialogueBaseNode
	{
		public ItemName item;
		public QuestName sideQuest;

		public override void Trigger()
		{
			GameManager.instance.AddToInventory(item);
			GameManager.instance.IncreaseAmount(sideQuest);
		}
	}
}