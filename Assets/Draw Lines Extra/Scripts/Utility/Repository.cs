using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

namespace IGS.DrawLines.Repository
{
#if UNITY_EDITOR
			
		public class RepositoryIE
		{
				private static readonly string defaultMissionsDirectory = Application.dataPath + "/Draw Lines Extra/Repository/Missions";
				private static readonly string defaultLevelsDirectory = Application.dataPath + "/Draw Lines Extra/Repository/Levels";

				public static Level ImportLevel ()
				{
						Level level = null;
						string directory = Directory.Exists (defaultLevelsDirectory) ? defaultLevelsDirectory : Application.dataPath;
						string path = EditorUtility.OpenFilePanel (
								              "Import Level",
								              directory,
								              "xml");
				
						if (path.Length != 0) {
								RepositoryLevel repositoryLevel = FileManager.LoadDataFromXMLFile<RepositoryLevel> (path);
								if (repositoryLevel != null) {
										level = RepositoryUtil.RepositoryLevelToLevel (repositoryLevel);
								}
						}

						return level;
				}

				public static void ExportLevel (Level level, int levelIndex, int missionID)
				{
						if (level == null) {
								return;
						}

						string directory = Directory.Exists (defaultLevelsDirectory) ? defaultLevelsDirectory : Application.dataPath;
						string path = EditorUtility.SaveFilePanel (
								              "Export level",
								              directory,
								              "level-" + missionID + "-" + (levelIndex + 1),
								              "xml");

						if (path.Length != 0) {
								RepositoryLevel repositoryLevel = RepositoryUtil.LevelToRepositoryLevel (level);
								if (repositoryLevel != null) {
										FileManager.SaveDataToXMLFile (repositoryLevel, path);
										EditorUtility.RevealInFinder (path);
								}
						}
				}

				public static bool ImportMission (LevelsManager levelsManager)
				{
						if (levelsManager == null) {
								return false;
						}

						string directory = Directory.Exists (defaultMissionsDirectory) ? defaultMissionsDirectory : Application.dataPath;
						string path = EditorUtility.OpenFilePanel (
								              "Import Mission",
								              directory,
								              "xml");

						if (path.Length != 0) {
								try {
										RepositoryMission repositoryMission = FileManager.LoadDataFromXMLFile<RepositoryMission> (path);

										levelsManager.numberOfRows = repositoryMission.numberOfRows;
										levelsManager.numberOfCols = repositoryMission.numberOfCols;

										levelsManager.applyColorOnSprite = repositoryMission.applyColorOnSprite;
										levelsManager.enableSequence = repositoryMission.enableSequence;
										levelsManager.enablePairsNumber = repositoryMission.enablePairsNumber;
										levelsManager.autoGenerateLevel = repositoryMission.autoGenerateLevel;

										if(!string.IsNullOrEmpty(repositoryMission.defaultSprite))
											levelsManager.defaultSprite = AssetDatabase.LoadAssetAtPath (repositoryMission.defaultSprite, typeof(Sprite)) as Sprite;
										if(!string.IsNullOrEmpty(repositoryMission.firstGridCellBackground))
											levelsManager.firstGridCellBackground = AssetDatabase.LoadAssetAtPath (repositoryMission.firstGridCellBackground, typeof(Sprite)) as Sprite;
										if(!string.IsNullOrEmpty(repositoryMission.secondGridCellBackground))
											levelsManager.secondGridCellBackground = AssetDatabase.LoadAssetAtPath (repositoryMission.secondGridCellBackground, typeof(Sprite)) as Sprite;

											levelsManager.allowedMovements = repositoryMission.allowedMovements;

										levelsManager.levels.Clear();
										foreach (RepositoryLevel repositoryLevel in repositoryMission.repositoryLevels) {
												if (repositoryLevel != null) {
														Level level = RepositoryUtil.RepositoryLevelToLevel (repositoryLevel);
														if (level != null)
																levelsManager.levels.Add (level);
												}
										}
								} catch (Exception ex) {
										Debug.LogError (ex.Message);
										return false;
								}

								return true;
						}

						return false;
				}

				public static void ExportMission (LevelsManager levelsManager, int missionID)
				{
						if (levelsManager == null) {
								return;
						}

						string directory = Directory.Exists (defaultMissionsDirectory) ? defaultMissionsDirectory : Application.dataPath;
						string path = EditorUtility.SaveFilePanel (
								              "Export Mission",
								              directory,
								              "mission-" + missionID,
								              "xml");

						if (path.Length != 0) {
								RepositoryMission repositoryMission = new RepositoryMission ();

								repositoryMission.numberOfRows = levelsManager.numberOfRows;
								repositoryMission.numberOfCols = levelsManager.numberOfCols;

								repositoryMission.applyColorOnSprite = levelsManager.applyColorOnSprite;
								repositoryMission.enableSequence = levelsManager.enableSequence;
								repositoryMission.enablePairsNumber = levelsManager.enablePairsNumber;
								repositoryMission.autoGenerateLevel = levelsManager.autoGenerateLevel;

								repositoryMission.defaultSprite = AssetDatabase.GetAssetPath (levelsManager.defaultSprite);
								repositoryMission.firstGridCellBackground = AssetDatabase.GetAssetPath (levelsManager.firstGridCellBackground);
								repositoryMission.secondGridCellBackground = AssetDatabase.GetAssetPath (levelsManager.secondGridCellBackground);

								repositoryMission.allowedMovements = levelsManager.allowedMovements;

								foreach (Level level in levelsManager.levels) {
										if (level != null) {
												RepositoryLevel repositoryLevel = RepositoryUtil.LevelToRepositoryLevel (level);
												if (repositoryLevel != null)
														repositoryMission.repositoryLevels.Add (repositoryLevel);
										}
								}

								FileManager.SaveDataToXMLFile (repositoryMission, path);
								EditorUtility.RevealInFinder (path);
						}
				}
		}

		public class RepositoryUtil
		{
				public static Level RepositoryLevelToLevel (RepositoryLevel repositoryLevel)
				{
						Level level = null;
						if (repositoryLevel != null) {
								level = new Level ();
								level.elementsPairs = new List<Level.ElementsPair> ();

								foreach (RepositoryLevel.ElementsPair repositoryElementPair in repositoryLevel.elementsPairs) {

										if (repositoryElementPair == null) {
												continue;
										}

										Level.ElementsPair elementsPair = new Level.ElementsPair ();
										elementsPair.showPair = false;

										if (!string.IsNullOrEmpty (repositoryElementPair.color))
												elementsPair.color = CommonUtil.StringRGBAToColor (repositoryElementPair.color);

										if (!string.IsNullOrEmpty (repositoryElementPair.lineColor))
												elementsPair.lineColor = CommonUtil.StringRGBAToColor (repositoryElementPair.lineColor);

										if (!string.IsNullOrEmpty (repositoryElementPair.numberColor))
											elementsPair.numberColor = CommonUtil.StringRGBAToColor (repositoryElementPair.numberColor);

										if (!string.IsNullOrEmpty (repositoryElementPair.sprite))
												elementsPair.sprite = AssetDatabase.LoadAssetAtPath (repositoryElementPair.sprite, typeof(Sprite)) as Sprite;

										if (!string.IsNullOrEmpty (repositoryElementPair.connectSprite))
												elementsPair.connectSprite = AssetDatabase.LoadAssetAtPath (repositoryElementPair.connectSprite, typeof(Sprite)) as Sprite;

										elementsPair.firstElement = new Level.Element ();
										if (repositoryElementPair.firstElement != null)
												elementsPair.firstElement.index = repositoryElementPair.firstElement.index;

										elementsPair.secondElement = new Level.Element ();
										if (repositoryElementPair.secondElement != null)
												elementsPair.secondElement.index = repositoryElementPair.secondElement.index;

										if(elementsPair.helpPath!=null)
											elementsPair.helpPath = repositoryElementPair.helpPath;

										level.elementsPairs.Add (elementsPair);
								}
						}
						return level;
				}

				public static RepositoryLevel LevelToRepositoryLevel (Level level)
				{
						RepositoryLevel repositoryLevel = null;
						if (level != null) {
								repositoryLevel = new RepositoryLevel ();

								repositoryLevel.elementsPairs = new List<RepositoryLevel.ElementsPair> ();

								foreach (Level.ElementsPair elementPair in level.elementsPairs) {
										RepositoryLevel.ElementsPair repositoryLevelElementsPair = new RepositoryLevel.ElementsPair ();

										repositoryLevelElementsPair.color = elementPair.color.r + "," + elementPair.color.g + "," + elementPair.color.b + "," + elementPair.color.a;
										repositoryLevelElementsPair.lineColor = elementPair.lineColor.r + "," + elementPair.lineColor.g + "," + elementPair.lineColor.b + "," + elementPair.lineColor.a;
										repositoryLevelElementsPair.numberColor = elementPair.numberColor.r + "," + elementPair.numberColor.g + "," + elementPair.numberColor.b + "," + elementPair.numberColor.a;

										repositoryLevelElementsPair.sprite = AssetDatabase.GetAssetPath (elementPair.sprite);
										repositoryLevelElementsPair.connectSprite = AssetDatabase.GetAssetPath (elementPair.connectSprite);

										repositoryLevelElementsPair.firstElement = new RepositoryLevel.Element ();
										repositoryLevelElementsPair.firstElement.index = elementPair.firstElement.index;
										repositoryLevelElementsPair.secondElement = new RepositoryLevel.Element ();
										repositoryLevelElementsPair.secondElement.index = elementPair.secondElement.index;
										repositoryLevelElementsPair.helpPath = elementPair.helpPath;

										repositoryLevel.elementsPairs.Add (repositoryLevelElementsPair);
								}
						}
						return repositoryLevel;
				}
		}

		[System.Serializable]
		public class RepositoryLevel
		{
				public List<ElementsPair> elementsPairs = new List<ElementsPair> ();

				public class ElementsPair
				{
						public string sprite;
						public string connectSprite;
						public string color;
						public string lineColor;
						public string numberColor;
						public Element firstElement = new Element ();
						public Element secondElement = new Element ();
						public List<int> helpPath = new List<int>();
				}

				public class Element
				{
						public int index;
				}
		}

		public class RepositoryMission
		{
				public string defaultSprite;
				public string firstGridCellBackground;
				public string secondGridCellBackground;
				public int numberOfCols;
				public int numberOfRows;
				public LevelsManager.Movements allowedMovements = LevelsManager.Movements.FOUR_MOVEMENTS;
				public bool applyColorOnSprite;
				public bool enablePairsNumber;
				public bool enableSequence = true;
				public bool autoGenerateLevel;
				public List<RepositoryLevel> repositoryLevels = new List<RepositoryLevel> ();
		}
		#endif
}