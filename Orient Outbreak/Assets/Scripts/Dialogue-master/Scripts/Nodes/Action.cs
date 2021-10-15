using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
	public class Action : DialogueBaseNode
	{
		public string qname;
		public string qdesc;

		public override void Trigger()
		{
			Debug.Log("ENTERED!!");
		}
	}
}