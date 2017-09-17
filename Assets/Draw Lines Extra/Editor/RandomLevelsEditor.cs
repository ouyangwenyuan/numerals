using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

public class RandomLevelsEditor: EditorWindow
{
		private Vector2 scrollPos;
		private Vector2 scale;
		private Vector2 scrollView = new Vector2 (550, 430);
		private static RandomLevelsEditor window;
		private static GameObject[] missions;
		private static List<int> levelsCounts;
		private static List<bool> active;
		private static bool showInstructions = true;
		
		[MenuItem("Tools/Draw Lines Extra/Random Levels Generator #g",false,1)]
		static void  GenerateRandomMission ()
		{
				levelsCounts = new List<int> ();
				active = new List<bool> ();

				missions = CommonUtil.FindGameObjectsOfTag ("Mission");
				for(int i = 0 ; i <missions.Length;i++) {
						levelsCounts.Add (48);
						active.Add (true);
				}
				window = (RandomLevelsEditor)EditorWindow.GetWindow (typeof(RandomLevelsEditor));
				window.titleContent.text = "Generate Random Levels";
				window.Show ();
		}
	
		[MenuItem("Tools/Draw Lines Extra/Random Levels Generator #g",true,1)]
		static bool GenerateRandomMissionValidate ()
		{
				return !Application.isPlaying && GameObject.FindObjectOfType<LevelsManager> () != null;
		}

		void OnGUI ()
		{
				if (window == null || missions.Length == 0 || Application.isPlaying) {
						return;
				}
		
				scrollView = new Vector2 (position.width, position.height);
				scrollPos = EditorGUILayout.BeginScrollView (scrollPos, GUILayout.Width (scrollView.x), GUILayout.Height (scrollView.y));
				EditorGUILayout.Separator ();
				EditorGUILayout.Separator ();
				showInstructions = EditorGUILayout.Foldout (showInstructions, "Instructions");
				EditorGUILayout.Separator ();
				if (showInstructions) {
						EditorGUILayout.HelpBox ("Select number of levels for each Mission.", MessageType.Info);
						EditorGUILayout.HelpBox ("Click on Generate button to generate random levels.", MessageType.Info);
				}
				EditorGUILayout.Separator ();

				LevelsManager lm;
				for (int i = 0; i <missions.Length; i++) {
						lm = missions [i].GetComponent<LevelsManager> ();
						GUI.contentColor = Colors.yellowColor;         
						GUILayout.Box ("Mission " + (i + 1) + " - " + lm.numberOfRows + "x" + lm.numberOfCols, GUILayout.ExpandWidth (true), GUILayout.Height (20));
						GUI.contentColor = Colors.whiteColor;
						active [i] = EditorGUILayout.Toggle ("Active", active [i]);
						levelsCounts [i] = EditorGUILayout.IntSlider ("Number of Levels", levelsCounts [i], 1, 500);
				}
				EditorGUILayout.Separator ();
					if (GUILayout.Button ("Generate", GUILayout.Width (120), GUILayout.Height (25))) {
						bool isOk = EditorUtility.DisplayDialog ("Generate Random Levels", "Random Levels Generator will clear the current levels in the checked Missions as well as generate new random levels", "ok","cancel");
						if (isOk) {
								GenerateLevels ();
						}
				}
				EditorGUILayout.Separator ();
				EditorGUILayout.EndScrollView ();
				
				window.Repaint ();
		}
	
		void OnInspectorUpdate ()
		{
				Repaint ();
		}
	
		private static void GenerateLevels ()
		{
				int count = 0;
				LevelsManager lm;
				for (int j = 0; j < missions.Length; j++) {
						if (!active [j]) {
								continue;
						}
						lm = missions [j].GetComponent<LevelsManager> ();
						lm.levels.Clear ();
						count += levelsCounts [j];
						for (int i = 0; i <levelsCounts[j]; i++) {
								ProgressUtil.DisplayProgressBar (i * 1.0f / levelsCounts [j], "[" + lm.numberOfRows + "x" + lm.numberOfCols + "] Generating Random Level " + (i + 1), "please wait while trying to generate new level...");
								LevelsManagerUtil.CreateNewLevel (lm, false);
						}
						ProgressUtil.HideProgressBar ();
				}
				DirtyUtil.MarkSceneDirty ();
				EditorUtility.DisplayDialog ("Done", count + " random levels have been generated successfully", "ok");
		}
}