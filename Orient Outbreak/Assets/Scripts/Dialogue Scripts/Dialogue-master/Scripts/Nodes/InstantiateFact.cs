using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
	public class InstantiateFact : DialogueBaseNode
	{
		public GameObject tornPaper;

		[TextAreaAttribute(20,25)]
		public string factText;

		public override void Trigger()
		{
			GameObject go = Instantiate(tornPaper);
			SetFactTextUI setFactUIScript = go.GetComponent<SetFactTextUI>();

			setFactUIScript.SetFactText(factText);
		}
	}
}