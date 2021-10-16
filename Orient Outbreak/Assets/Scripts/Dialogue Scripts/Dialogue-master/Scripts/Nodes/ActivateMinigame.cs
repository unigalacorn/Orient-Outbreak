using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XNode;

namespace Dialogue
{
	public class ActivateMinigame : DialogueBaseNode
	{
		//GameManager gm;

		[SerializeField] private string sceneName;
		//[SerializeField] private string questDescription;
		//[SerializeField] private int currentAmount;
		//[SerializeField] private int requiredAmount;

		public override void Trigger()
		{
			GameManager.instance.UpdateGameState(GameState.Minigame);
			SceneManager.LoadScene(sceneName);
			
			Debug.Log(GameManager.instance.currentState);
			//gm.AddQuest(questName, questDescription, currentAmount, requiredAmount);
			Debug.Log("ENTERED!!");
		}
	}
}