using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

namespace DrawLinesEditors
{
		[CustomEditor(typeof(LevelsManager))]
		public class LevelsManagerEditor : Editor
		{
				private static bool showInstructions = true;
				private static float horizontalSpace = 15;
				private static string[] bars = new string[] {"Level Pairs"};
				private static int selectedBar = 0;
				private int selectedLevel, previousLevel = -1;
				private string[] levels;

				public override void OnInspectorGUI ()
				{
						if (Application.isPlaying) {
								return;
						}
						LevelsManager attrib = (LevelsManager)target;//get the target

						EditorGUILayout.Separator ();
						showInstructions = EditorGUILayout.Foldout (showInstructions, "Instructions");
						EditorGUILayout.Separator ();
						if (showInstructions) {
								EditorGUILayout.HelpBox ("* Select number of Rows and Columns to create a Grid of Size equals [Rows x Columns].", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Import Mission' to replace the Mission by new one", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Export Mission' to export the Mission", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Create New Level' to create a new Level for the Mission", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Remove Levels' to remove all Levels in the Mission", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'The Grid' to view the grid of the Level", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Import Level' to replace the Level by new one", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Export Level' to export the Level", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Create New Pair' to create a new pair of elements for the Level ", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Remove Pairs' to remove all pairs in Level", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Remove Level' to remove the Level from the Mission", MessageType.None);
								EditorGUILayout.HelpBox ("* Click on 'Remove Pair' to remove the pair from the Level", MessageType.None);
								EditorGUILayout.HelpBox ("Change Sprites/Colors sequence from Missions Component.", MessageType.Info);
								EditorGUILayout.HelpBox ("Save your changes (ctrl/cmd+s).", MessageType.Info);
								EditorGUILayout.Separator ();
								attrib.numberOfRows = EditorGUILayout.IntSlider ("Number of Rows", attrib.numberOfRows, 2, LevelsManager.rowsLimit);
								EditorGUILayout.Separator ();
								attrib.numberOfCols = EditorGUILayout.IntSlider ("Number of Columns", attrib.numberOfCols, 2, LevelsManager.colsLimit);
						}
						EditorGUILayout.Separator ();

						attrib.allowedMovements = (LevelsManager.Movements)EditorGUILayout.EnumPopup ("Allowed Movements", attrib.allowedMovements);
						EditorGUILayout.Separator ();

						attrib.defaultSprite = EditorGUILayout.ObjectField ("Default Sprite", attrib.defaultSprite, typeof(Sprite), true) as Sprite;
						EditorGUILayout.Separator ();
				
						attrib.firstGridCellBackground = EditorGUILayout.ObjectField ("First GridCell BG", attrib.firstGridCellBackground, typeof(Sprite), true) as Sprite;
						EditorGUILayout.Separator ();

						attrib.secondGridCellBackground = EditorGUILayout.ObjectField ("Second GridCell BG", attrib.secondGridCellBackground, typeof(Sprite), true) as Sprite;
						EditorGUILayout.Separator ();

						attrib.enableSequence = EditorGUILayout.Toggle ("Enable Sequence", attrib.enableSequence);
						EditorGUILayout.Separator ();
						attrib.applyColorOnSprite = EditorGUILayout.Toggle ("Apply Color On Sprite", attrib.applyColorOnSprite);
						EditorGUILayout.Separator ();
						attrib.enablePairsNumber = EditorGUILayout.Toggle ("Enable Pairs Number", attrib.enablePairsNumber);
						EditorGUILayout.Separator ();
						attrib.autoGenerateLevel = EditorGUILayout.Toggle ("Auto Generate Level", attrib.autoGenerateLevel);
						EditorGUILayout.Separator ();
						EditorGUILayout.Separator ();

						if (attrib.previousNumberOfRows == -1) {
								attrib.previousNumberOfRows = attrib.numberOfRows;
						}

						if (attrib.previousNumberOfCols == -1) {
								attrib.previousNumberOfCols = attrib.numberOfCols;
						}

						if (attrib.previousNumberOfCols != attrib.numberOfCols || attrib.previousNumberOfRows != attrib.numberOfRows) {

								if (attrib.levels.Count != 0) {
										bool isOk = EditorUtility.DisplayDialog ("Confirm Message", "Changing grid size leads to reset all levels", "ok", "cancel");
										if (isOk) {
												LevelsManagerUtil.RemoveLevels (attrib);
										} else {
												attrib.numberOfRows = attrib.previousNumberOfRows;
												attrib.numberOfCols = attrib.previousNumberOfCols;
										}
								} else {
										LevelsManagerUtil.RemoveLevels (attrib);
								}
						}

						GUILayout.BeginHorizontal ();
						if (GUILayout.Button ("Support", GUILayout.Width (305), GUILayout.Height (25))) {
								Application.OpenURL ("mailto:freelance.art2014@gmail.com");
						}
						GUILayout.EndHorizontal ();

						GUILayout.BeginHorizontal ();
						if (GUILayout.Button ("Import Mission", GUILayout.Width (150), GUILayout.Height (25))) {
								bool importedSuccessfully = IGS.DrawLines.Repository.RepositoryIE.ImportMission (attrib);
								if (importedSuccessfully) {
										attrib.previousNumberOfCols = attrib.numberOfCols;
										attrib.previousNumberOfRows = attrib.numberOfRows;
										EditorUtility.DisplayDialog ("Importing Mission", "Mission has been imported successfully", "ok");
								}
						}

						if (GUILayout.Button ("Export Mission", GUILayout.Width (150), GUILayout.Height (25))) {
								IGS.DrawLines.Repository.RepositoryIE.ExportMission (attrib, attrib.gameObject.GetComponent<Mission> ().ID);	
						}

						GUILayout.EndHorizontal ();
						
						GUILayout.BeginHorizontal ();

						GUI.backgroundColor = Colors.greenColor;         
						if (GUILayout.Button ("Create New Level", GUILayout.Width (150), GUILayout.Height (25))) {
								Level lvl = LevelsManagerUtil.CreateNewLevel (attrib, true);
								if (lvl != null) {
										lvl.showGridOnCreate = true;
								}
						}
						GUI.backgroundColor = Colors.whiteColor;         

						GUI.backgroundColor = Colors.redColor;         
						if (GUILayout.Button ("Remove Levels", GUILayout.Width (150), GUILayout.Height (25))) {
								if (attrib.levels.Count != 0) {
										bool isOk = EditorUtility.DisplayDialog ("Removing Levels", "Are you sure you want to remove all levels ?", "yes", "cancel");
										if (isOk) {
												LevelsManagerUtil.RemoveLevels (attrib);
												CloseGridWindow ();
										}
								}
						}
						GUI.backgroundColor = Colors.whiteColor;         

						GUILayout.EndHorizontal ();
						if (attrib.enableSequence) {
								GUILayout.BeginHorizontal ();
								if (GUILayout.Button ("Reset Levels Pairs using the Sequence", GUILayout.Width (305), GUILayout.Height (25))) {
										bool isOk = EditorUtility.DisplayDialog ("Reset Pairs using Sequence", "Are you sure you want to reset all pairs in the levels ?", "yes", "cancel");
										if (isOk) {
												LevelsManagerUtil.ResetPairs (attrib);
										}
								}
								GUILayout.EndHorizontal ();
						}
						EditorGUILayout.Separator ();
						GUILayout.Box ("Levels Section", GUILayout.ExpandWidth (true), GUILayout.Height (23));
						EditorGUILayout.Separator ();

						EditorGUILayout.HelpBox ("Move between levels using Selected Level slider", MessageType.Info);

						EditorGUILayout.LabelField ("Number of Levels : " + attrib.levels.Count);
						if (attrib.levels.Count == 0) {
								return;
						} 

						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button ("<", GUILayout.Width (18), GUILayout.Height (15))) {
								if (selectedLevel - 1 >= 0)
										selectedLevel -= 1;
						}

						selectedLevel = selectedLevel + 1;
						selectedLevel = EditorGUILayout.IntSlider ("Selected Level", selectedLevel, 1, attrib.levels.Count); 
						selectedLevel = selectedLevel - 1;
					
						if (GUILayout.Button (">", GUILayout.Width (18), GUILayout.Height (15))) {
								if (selectedLevel + 1 < attrib.levels.Count)
										selectedLevel += 1;
						}

						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Separator ();

						GUILayout.Box ("Level " + (selectedLevel + 1), GUILayout.ExpandWidth (true), GUILayout.Height (23));

						if (attrib.levels [selectedLevel].showGridOnCreate || selectedLevel != previousLevel) {
								previousLevel = selectedLevel;
								DrawLinesEditors.GridWindowEditor.Init (attrib.levels [selectedLevel], "Level " + (selectedLevel + 1) + " Grid", attrib);
								attrib.levels [selectedLevel].showGridOnCreate = false;
						}
			        
						EditorGUILayout.Separator ();
										
						GUI.backgroundColor = Colors.cyanColor;         
						GUILayout.BeginHorizontal ();
						if (GUILayout.Button ("The Grid", GUILayout.Width (110), GUILayout.Height (23))) {
								DrawLinesEditors.GridWindowEditor.Init (attrib.levels [selectedLevel], "Level " + (selectedLevel + 1) + " Grid", attrib);
						}
						GUI.backgroundColor = Colors.whiteColor;         
				
						if (GUILayout.Button ("Import Level", GUILayout.Width (110), GUILayout.Height (23))) {
								Level level = IGS.DrawLines.Repository.RepositoryIE.ImportLevel ();
								if (level != null) {
										attrib.levels [selectedLevel] = level;
										EditorUtility.DisplayDialog ("Importing Level", "Level has been imported successfully", "ok");
								}
						}

						if (GUILayout.Button ("Export Level", GUILayout.Width (110), GUILayout.Height (23))) {
								IGS.DrawLines.Repository.RepositoryIE.ExportLevel (attrib.levels [selectedLevel], selectedLevel, attrib.GetComponent<Mission> ().ID);	
						}

						GUILayout.EndHorizontal ();

						GUILayout.BeginHorizontal ();
						if (GUILayout.Button ("Generate", GUILayout.Width (110), GUILayout.Height (23))) {
								ProgressUtil.DisplayProgressBar (1, "Generating Random Level " + attrib.numberOfRows + "x" + attrib.numberOfCols, "please wait while trying to generate new level...");
								RandomLevelGenerator.Generate (attrib.levels [selectedLevel], attrib);
								DrawLinesEditors.GridWindowEditor.Init (attrib.levels [selectedLevel], "Level " + (selectedLevel + 1) + " Grid",attrib);
								ProgressUtil.HideProgressBar ();
						}

						GUI.backgroundColor = Colors.redColor;         
						if (GUILayout.Button ("Remove Level " + (selectedLevel + 1), GUILayout.Width (110), GUILayout.Height (23))) {
								bool isOk = EditorUtility.DisplayDialog ("Removing Level", "Are you sure you want to remove level " + (selectedLevel + 1) + " ?", "yes", "cancel");
								if (isOk) {
										LevelsManagerUtil.RemoveLevel (selectedLevel, attrib);
										if (attrib.levels.Count == 0) {
												CloseGridWindow ();
										}
										return;
								}
						}
						GUI.backgroundColor = Colors.whiteColor;         

						GUILayout.EndHorizontal ();


						EditorGUILayout.Separator ();

						selectedBar = GUILayout.Toolbar (selectedBar, bars);
							
						switch (selectedBar) {
						case 0:
								ShowLevelPairs (attrib.levels [selectedLevel], selectedLevel, attrib);
								break;
						}

						if (GUI.changed) {
								DirtyUtil.MarkSceneDirty ();
								GridWindowEditor.Refresh ();
						}
				}
				
				private void ShowLevelPairs (Level level, int levelIndex, LevelsManager attrib)
				{
						EditorGUILayout.Separator ();

						GUILayout.BeginHorizontal ();
						GUILayout.Space (horizontalSpace);

						GUI.backgroundColor = Colors.greenColor;         
						if (GUILayout.Button ("Create New Pair", GUILayout.Width (110), GUILayout.Height (23))) {
								if (level.elementsPairs.Count < attrib.numberOfRows * attrib.numberOfCols / 2) {
										LevelsManagerUtil.CreateNewPair (attrib, level, true);
								} else {
										EditorUtility.DisplayDialog ("Limit Reached", "You can't add more pairs", "ok");
								}
						}
						GUI.backgroundColor = Colors.whiteColor;         
			
						GUI.backgroundColor = Colors.redColor; 
						if (level.elementsPairs.Count != 0)
						if (GUILayout.Button ("Remove Pairs", GUILayout.Width (110), GUILayout.Height (23))) {
								bool isOk = EditorUtility.DisplayDialog ("Removing Level Pairs", "Are you sure you want to remove the pairs of Level" + (levelIndex + 1) + " ?", "yes", "cancel");
								if (isOk) {
										LevelsManagerUtil.RemoveLevelPairs (level, attrib);
										return;
								}
						}
		
						GUI.backgroundColor = Colors.whiteColor;
						GUILayout.EndHorizontal ();
						EditorGUILayout.Separator ();

						for (int j = 0; j < level.elementsPairs.Count; j++) {
								GUILayout.BeginHorizontal ();
								GUILayout.Space (horizontalSpace);
								level.elementsPairs [j].showPair = EditorGUILayout.Foldout (level.elementsPairs [j].showPair, "Pair " + (j + 1));
								GUILayout.EndHorizontal ();

								if (level.elementsPairs [j].showPair) {
										EditorGUILayout.Separator ();
										GUILayout.BeginHorizontal ();
										GUILayout.Space (horizontalSpace);
										GUI.backgroundColor = Colors.redColor;        
										if (GUILayout.Button ("Remove Pair " + (j + 1), GUILayout.Width (120), GUILayout.Height (25))) {
												bool isOk = EditorUtility.DisplayDialog ("Removing Pair", "Are you sure you want to remove pair" + (j + 1) + " ?", "yes", "cancel");
												if (isOk) {
														LevelsManagerUtil.RemovePair (j, level, attrib);
														continue;
												}
										}
										GUI.backgroundColor = Colors.whiteColor;         
										GUILayout.EndHorizontal ();

										EditorGUILayout.Separator ();

										if (level.elementsPairs [j].sprite == null) {
												level.elementsPairs [j].sprite = attrib.defaultSprite;
										}
					
										if (level.elementsPairs [j].connectSprite == null) {
												level.elementsPairs [j].connectSprite = level.elementsPairs [j].sprite;
										}

										GUILayout.BeginHorizontal ();
										GUILayout.Space (horizontalSpace);
										level.elementsPairs [j].sprite = EditorGUILayout.ObjectField ("Normal Sprite", level.elementsPairs [j].sprite, typeof(Sprite), true) as Sprite;
										GUILayout.EndHorizontal ();

										EditorGUILayout.Separator ();

										GUILayout.BeginHorizontal ();
										GUILayout.Space (horizontalSpace);
										level.elementsPairs [j].connectSprite = EditorGUILayout.ObjectField ("Connect Sprite", level.elementsPairs [j].connectSprite, typeof(Sprite), true) as Sprite;
										GUILayout.EndHorizontal ();

										EditorGUILayout.Separator ();
										GUILayout.BeginHorizontal ();
										GUILayout.Space (horizontalSpace);
										level.elementsPairs [j].color = EditorGUILayout.ColorField ("Sprite Color", level.elementsPairs [j].color);
										GUILayout.EndHorizontal ();
										EditorGUILayout.Separator ();

										GUILayout.BeginHorizontal ();
										GUILayout.Space (horizontalSpace);
										level.elementsPairs [j].lineColor = EditorGUILayout.ColorField ("Line Color", level.elementsPairs [j].lineColor);
										GUILayout.EndHorizontal ();
										EditorGUILayout.Separator ();

										GUILayout.BeginHorizontal ();
										GUILayout.Space (horizontalSpace);
										level.elementsPairs [j].numberColor = EditorGUILayout.ColorField ("Number Color", level.elementsPairs [j].numberColor);
										GUILayout.EndHorizontal ();

										EditorGUILayout.Separator ();

										GUILayout.BeginHorizontal ();
										GUILayout.Space (horizontalSpace);
										level.elementsPairs [j].firstElement.index = EditorGUILayout.IntSlider ("First Element Index", level.elementsPairs [j].firstElement.index, 0, attrib.numberOfRows * attrib.numberOfCols - 1);
										GUILayout.EndHorizontal ();

										EditorGUILayout.Separator ();

										GUILayout.BeginHorizontal ();
										GUILayout.Space (horizontalSpace);
										level.elementsPairs [j].secondElement.index = EditorGUILayout.IntSlider ("Second Element Index", level.elementsPairs [j].secondElement.index, 0, attrib.numberOfRows * attrib.numberOfCols - 1);
										GUILayout.EndHorizontal ();
										EditorGUILayout.Separator ();
								}
						}
						EditorGUILayout.Separator ();
						GUILayout.Box ("", GUILayout.ExpandWidth (true), GUILayout.Height (2));
				}
		
				public static void CloseGridWindow ()
				{
						if (GridWindowEditor.window != null) {
								GridWindowEditor.window.Close ();
						}
				}
		}
}