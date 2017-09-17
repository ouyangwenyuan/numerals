using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

namespace DrawLinesEditors
{
		public class GridWindowEditor : EditorWindow
		{
				private Vector2 scrollPos;
				private static Level level;
				private static LevelsManager levelsManager;
				private static int gridSize;
				private static int gridCellndex;
				private Vector2 offset = new Vector2 (0, 0);
				private Vector2 scale;
				private Vector2 scrollView = new Vector2 (550, 430);
				public static GridWindowEditor window;
				private static string levelTitle;
				private Level.ElementsPair currentElementsPair;
				private static int selectedHelpPath, previousPath;
				private static string[] paths;
				private static bool showHelpPaths = true;
				private static List<int> currentPath = new List<int> ();
				private static bool foldoutPath = true;
				private Texture2D tempTexture;
				private static int minScale = 25, maxScale = 45;
				private static int zoom = maxScale;

				public static void Init (Level lvl, string lvlTitle, LevelsManager attrib)
				{
						levelTitle = lvlTitle;
						levelsManager = attrib;
						gridSize = attrib.numberOfRows * attrib.numberOfCols;
						level = lvl;
						window = (GridWindowEditor)EditorWindow.GetWindow (typeof(GridWindowEditor));
						window.titleContent.text = levelTitle;
						ResetHelpPathParameters ();
						InitHelpPath ();
				}

				public static void Refresh ()
				{
						ResetHelpPathParameters ();
						InitHelpPath ();
				}

				private static void InitHelpPath ()
				{
						if (level != null) {
								paths = new string[level.elementsPairs.Count];
								for (int i = 0; i < level.elementsPairs.Count; i++) {
										paths [i] = "Help Path[" + i + "] between GridCell " + level.elementsPairs [i].firstElement.index + " and GridCell " + level.elementsPairs [i].secondElement.index;
								}
						}
				}

				private static void ResetHelpPathParameters ()
				{
						selectedHelpPath = 0;
						previousPath = -1;
				}

				void OnGUI ()
				{
						if (window == null || level == null || levelsManager == null || Application.isPlaying) {
								return;
						}

						window.Repaint ();

						scrollView = new Vector2 (position.width, position.height);
						scrollPos = EditorGUILayout.BeginScrollView (scrollPos, GUILayout.Width (scrollView.x), GUILayout.Height (scrollView.y));
						EditorGUILayout.Separator ();
						EditorGUILayout.HelpBox ("Edit/Generate/Remove/Import/Export levels from the LevelsManager component.", MessageType.Info);
						EditorGUILayout.Separator ();
						zoom = EditorGUILayout.IntSlider ("Grid Scale", zoom, minScale, maxScale);
						scale.x = scale.y = zoom;
						GridGUILayout ();
						HelpPathsGUILayout ();
						EditorGUILayout.EndScrollView ();
				}

				private void GridGUILayout ()
				{
						EditorGUILayout.Separator ();
						GUILayout.Space (5);
						for (int i = 0; i < levelsManager.numberOfRows; i++) {

								GUILayout.BeginHorizontal ();
								GUILayout.Space (5);

								for (int j = 0; j < levelsManager.numberOfCols; j++) {
										gridCellndex = i * levelsManager.numberOfCols + j;

										getElementsPairOfGridCell (gridCellndex);
										GUI.backgroundColor = GetGridCellLineColor (gridCellndex);

										tempTexture = currentElementsPair != null ? (currentElementsPair.sprite != null ? currentElementsPair.sprite.texture : null) : null;

										if (GUILayout.Button (tempTexture, GUILayout.Width (scale.x), GUILayout.Height (scale.y))) {
												EditorUtility.DisplayDialog ("GridCell", "GridCell of index " + gridCellndex, "ok");
										}
										GUI.backgroundColor = Color.white;

										GUILayout.Space (offset.x);
								}
								GUILayout.EndHorizontal ();
								GUILayout.Space (offset.y);

								GUI.contentColor = Color.white;
								GUILayout.BeginHorizontal ();
								GUILayout.Space (5);
								for (int j = 0; j < levelsManager.numberOfCols; j++) {
										gridCellndex = i * levelsManager.numberOfCols + j;

										GUILayout.TextField (gridCellndex + "", GUILayout.Width (scale.x), GUILayout.Height (20));
										GUILayout.Space (offset.x);
								}
								GUILayout.EndHorizontal ();
						}
						EditorGUILayout.Separator ();
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Space (scrollView.x / 2 - 50);
						EditorGUILayout.LabelField (levelsManager.numberOfRows + "x" + levelsManager.numberOfCols + " Grid");
						EditorGUILayout.EndHorizontal ();
				}

				private void HelpPathsGUILayout ()
				{
						if (level.elementsPairs.Count == 0) {
								return;
						}
						EditorGUILayout.Separator ();
						EditorGUILayout.Separator ();

						GUILayout.Box ("", GUILayout.ExpandWidth (true), GUILayout.Height (2));
						EditorGUILayout.HelpBox ("Help Paths must be in the correct form.", MessageType.Info);
						EditorGUILayout.Separator ();

						showHelpPaths = EditorGUILayout.Toggle ("Show/Hide Help Paths", showHelpPaths);
				
						if (!(selectedHelpPath >= 0 && selectedHelpPath < level.elementsPairs.Count)) {
								ResetHelpPathParameters ();
						}

						if (selectedHelpPath != previousPath) {
								currentPath = level.elementsPairs [selectedHelpPath].helpPath;
								previousPath = selectedHelpPath;
						} 

						GUI.backgroundColor = level.elementsPairs [selectedHelpPath].lineColor;
						selectedHelpPath = EditorGUILayout.Popup ("Selected Help Path", selectedHelpPath, paths); 
						GUI.backgroundColor = Colors.whiteColor;

						EditorGUILayout.Separator ();

						EditorGUILayout.BeginHorizontal ();

						GUI.backgroundColor = Colors.greenColor;
						if (GUILayout.Button ("+", GUILayout.Width (35), GUILayout.Height (35))) {
								if (currentPath.Count < gridSize) {
										currentPath.Add (0);
								}
						}
						GUI.backgroundColor = Colors.whiteColor;

						GUI.backgroundColor = Colors.redColor;
						if (GUILayout.Button ("-", GUILayout.Width (35), GUILayout.Height (35))) {
								if (currentPath.Count > 0) {
										currentPath.RemoveAt (currentPath.Count - 1);
								}
						}
						GUI.backgroundColor = Colors.whiteColor;

						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.Separator ();
						if (currentPath != null) {
								foldoutPath = EditorGUILayout.Foldout (foldoutPath, "Path (" + currentPath.Count + ")");
								if (foldoutPath) {
										EditorGUILayout.BeginVertical ();
										for (int i = 0; i < currentPath.Count; i++) {
												EditorGUILayout.BeginHorizontal ();
												GUILayout.Space (25);
												currentPath [i] = EditorGUILayout.IntSlider ("[" + i + "]", currentPath [i], 0, gridSize - 1);
												EditorGUILayout.EndHorizontal ();
										}
										EditorGUILayout.EndVertical ();
								}
						}
						EditorGUILayout.Separator ();
						EditorGUILayout.Separator ();
				}

				private Color GetGridCellLineColor (int index)
				{
						Color color = Colors.whiteColor;
						if (showHelpPaths) {
								//If grid cell one of the Help path , then get its color
								foreach (Level.ElementsPair elementPair in level.elementsPairs) {
										if (elementPair.helpPath != null)
												foreach (int i in elementPair.helpPath) {
														if (i == index) {
																color = elementPair.lineColor;
																color.a = 0.5f;
																return color;
														}
												}
								}
						}
						return color;
				}

				private void getElementsPairOfGridCell (int gridCellndex)
				{
						if (level == null) {
								return;
						}

						currentElementsPair = null;

						foreach (Level.ElementsPair elementsPair in level.elementsPairs) {

								if (elementsPair.firstElement.index == gridCellndex || elementsPair.secondElement.index == gridCellndex) {
										if (levelsManager.applyColorOnSprite)
												GUI.contentColor = elementsPair.color;
										else
												GUI.contentColor = Color.white;

										currentElementsPair = elementsPair;
										break;
								}
						}
				}
		}
}