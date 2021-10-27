using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
	public class InstantiatePanel : DialogueBaseNode
	{
		public GameObject panel;

		public override void Trigger()
		{
			Instantiate(panel);
		}
	}
}