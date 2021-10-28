using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
	public class MissionAccomplished : DialogueBaseNode
	{
		public override void Trigger()
		{
			if (GameManager.instance.isImmunityBoosterDone && GameManager.instance.isShieldsUpDone && GameManager.instance.isWerkItDone)
            {
				GameManager.instance.UpdateGameState(GameState.Cutscene);
				GameManager.instance.MissionAcomplished();
            }
		}
	}
}