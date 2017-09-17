using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[DisallowMultipleComponent]
public class LevelsManager : MonoBehaviour
{
		public Sprite defaultSprite;
		public Sprite firstGridCellBackground;
		public Sprite secondGridCellBackground;
		public readonly static int rowsLimit = 15;
		public readonly static int colsLimit = 15;
		public int numberOfCols = 5;
		public int numberOfRows = 5;
		public bool applyColorOnSprite;
		public bool enablePairsNumber;
		public bool randomShuffleOnBuild;
		public bool enableSequence = true;
		public bool autoGenerateLevel = true;

		public List<Level> levels = new List<Level> ();
		public Movements allowedMovements = Movements.FOUR_MOVEMENTS;
		[HideInInspector]
		public int previousNumberOfRows = -1;
		[HideInInspector]
		public int previousNumberOfCols = -1;

		public enum Movements
		{
				FOUR_MOVEMENTS,
				EIGHT_MOVEMENTS
		}
}