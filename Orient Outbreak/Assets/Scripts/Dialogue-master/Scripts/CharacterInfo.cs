﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
	[CreateAssetMenu(menuName = "Dialogue/CharacterInfo")]
	public class CharacterInfo : ScriptableObject {
		public string charName;
		public string charTitle;
		public Sprite dialogueSprite;

		public Color color;
	}
}