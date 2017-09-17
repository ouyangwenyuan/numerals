using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved
 
public class LevelsManagerUtil
{
		private static Color color;
		private static Sprite sprite = null, connectSprite = null;
		private static Missions missions;

		public static Level CreateNewLevel (LevelsManager attrib, bool showProgress)
		{
				if (attrib == null) {
						return null;
				}
		
				Level lvl = new Level ();
				attrib.levels.Add (lvl);
				if (attrib.autoGenerateLevel) {
						if (showProgress)
								ProgressUtil.DisplayProgressBar (1, "Generating Random Level " + attrib.numberOfRows + "x" + attrib.numberOfCols, "please wait while trying to generate new level...");
						RandomLevelGenerator.Generate (lvl, attrib);
						if (showProgress)
								ProgressUtil.HideProgressBar ();
				}

				return lvl;
		}
	
		public static Level.ElementsPair CreateNewPair (LevelsManager attrib, Level lvl, bool createResources)
		{
				if (lvl == null) {
						return null;
				}
				Level.ElementsPair elementsPair = new Level.ElementsPair ();
				if (createResources) {
						SetElementPairAttributes (elementsPair, lvl.elementsPairs.Count, attrib);
				}
				lvl.elementsPairs.Add (elementsPair);
				return elementsPair;
		}
	
		public static void RemoveLevels (LevelsManager attrib)
		{
				if (attrib == null) {
						return;
				}
				attrib.levels.Clear ();
				attrib.previousNumberOfRows = attrib.numberOfRows;
				attrib.previousNumberOfCols = attrib.numberOfCols;
		}
	
		public static void RemoveLevel (int index, LevelsManager attrib)
		{
				if (!(index >= 0 && index < attrib.levels.Count)) {
						return;
				}

				attrib.levels.RemoveAt (index);
		}

		public static void RemoveLevelPairs (Level level, LevelsManager attrib)
		{
				if (level == null) {
						return;
				}
				level.elementsPairs.Clear ();
		}
	
		public static void RemovePair (int index, Level level, LevelsManager attrib)
		{
				if (level == null) {
						return;
				}
		
				if (!(index >= 0 && index < level.elementsPairs.Count)) {
						return;
				}
		
				level.elementsPairs.RemoveAt (index);
		}
	
		public static void ResetPairs (LevelsManager attrib)
		{
				for (int i = 0; i < attrib.levels.Count; i++) {
						for (int j = 0; j < attrib.levels [i].elementsPairs.Count; j++) {
								SetElementPairAttributes (attrib.levels [i].elementsPairs [j], j, attrib);
						}
				}
		}
	
		public static void SetElementPairAttributes (Level.ElementsPair elementsPair, int index, LevelsManager attrib)
		{
				if (elementsPair == null) {
						return;
				}
		
				if (attrib.enableSequence) {
						//Set color & sprite from the sequence	
						if (missions == null) {
								missions = GameObject.FindObjectOfType<Missions> ();
						}

						//Get color from colors sequence
						if (index >= 0 && index < missions.colorsSequence.Length) {
								color = missions.colorsSequence [index];
						} else {
								color = CommonUtil.GetRandomColor ();
								//Debug.LogWarning ("You should increase the Colors Sequence in the <b>Missions Component</b>");
						}
			
						//Get sprite from sprites sequence
						if (index >= 0 && index < missions.spritesSequence.Length) {
								sprite = missions.spritesSequence [index];
						} else {
								sprite = attrib.defaultSprite;
								//Debug.LogWarning ("You should increase the Sprites Sequence in the <b>Missions Component</b>");
						}
			
						//Get connect sprite from connect sprites sequence
						if (index >= 0 && index < missions.connectSpritesSequence.Length) {
								connectSprite = missions.connectSpritesSequence [index];
						} else {
								connectSprite = attrib.defaultSprite;
								//Debug.LogWarning ("You should increase the Connect Sprites Sequence in the <b>Missions Component</b>");
						}
				} else {
						sprite = attrib.defaultSprite;
						connectSprite = attrib.defaultSprite;
						color = CommonUtil.GetRandomColor ();
				}
		
				elementsPair.color = color;
				elementsPair.lineColor = color;
				elementsPair.numberColor = new Color (1 - color.r, 1 - color.g, 1 - color.b, 1);
				elementsPair.sprite = sprite;
				elementsPair.connectSprite = connectSprite;
		}
}