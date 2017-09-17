using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved
using System.Collections.Generic;

#pragma warning disable 0168 // variable declared but not used.

[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(LevelsManager))]

/// <summary>
/// Important Note : Keep the IDs of Missions in an incremental sequence such as : 1,2,3,4,5,6,7..etc
/// </summary>
public class Mission : MonoBehaviour
{
		/// <summary>
		/// Mission ID.
		/// </summary>
		public int ID = -1;

		/// <summary>
		/// The selected Mission.
		/// </summary>
		public static Mission selectedMission;

		/// <summary>
		/// The color of the mission.
		/// </summary>
		public Color missionColor = Color.white;

		/// <summary>
		/// The title of the mission .
		/// </summary>
		public string missionTitle = "New Mission";

		/// <summary>
		/// Number of Rows.
		/// </summary>
		[HideInInspector]
		public int rowsNumber;

		/// <summary>
		/// Number of Columns.
		/// </summary>
		[HideInInspector]
		public int colsNumber;

		/// <summary>
		/// The LevelsManager Component.
		/// </summary>
		[HideInInspector]
		public LevelsManager levelsManagerComponent;

		/// <summary>
		/// Whether the mission is locked or not.
		/// </summary>
		[HideInInspector]
		public bool isLocked;

		void Awake ()
		{
				InitMission ();
		}

		void Start ()
		{
				SetStarsScore ();
		}

		/// <summary>
		/// Inits the mission.
		/// </summary>
		private void InitMission ()
		{
				//Set mission ID
				SetMissionID ();

				//Add onClick event
				GetComponent<Button> ().onClick.AddListener (() => GameObject.FindObjectOfType<UIEvents> ().MissionButtonEvent (GetComponent<Mission> ()));

				//Setting up the LevelsManager Component
				levelsManagerComponent = GetComponent<LevelsManager> ();
		
				Debug.Log ("Setting up Mission <b>" + missionTitle + "</b> of ID " + ID);

				if (levelsManagerComponent != null) {
						if (string.IsNullOrEmpty (missionTitle) || missionTitle == "New Mission") {
								//Define the Title of the Mission
								missionTitle = levelsManagerComponent.numberOfRows + "x" + levelsManagerComponent.numberOfCols;
						}
				}

				//Get the Number of Rows from LevelsManager Component
				rowsNumber = levelsManagerComponent.numberOfRows;
				//Get the Number of Columns from LevelsManager Component
				colsNumber = levelsManagerComponent.numberOfCols;
				
				Transform missionTitleTransform = transform.Find ("Title");
		
				//Setting up the Title of the Mission
				if (missionTitleTransform != null) {
						Text uiText = missionTitleTransform.GetComponent<Text> ();
						if (uiText != null)
								uiText.text = missionTitle;
				}
		}

		/// <summary>
		/// Set the ID of Mission.
		/// </summary>
		private void SetMissionID ()
		{
				string[] subStrings = name.Split ('-');
				if (subStrings == null) {
						InvalidNameMessage ();
						return;
				}

				if (subStrings.Length != 2) {
						InvalidNameMessage ();
				} else {
						bool validName = int.TryParse (subStrings [1], out ID);
						if (!validName) {
								InvalidNameMessage ();
						}
				}
		}

		/// <summary>
		/// Print Invalid message name in the console.
		/// </summary>
		private void InvalidNameMessage ()
		{
				Debug.LogError ("Invalid Mission Name , Run the scene again...");
		}

		/// <summary>
		/// Set the stars score of the mission.
		/// </summary>
		public void SetStarsScore ()
		{
				Transform score = transform.Find ("Score");	
				//Set the mission score
				if (score != null) {
						Transform starsCount = score.Find ("StarsCount");
						if (starsCount != null) {
								starsCount.GetComponent<Text> ().text = GetStarsCount (this);
						}
				}
		}
	
		/// <summary>
		/// Get the score count.
		/// </summary>
		/// <returns>The score count.</returns>
		public static string GetStarsCount (Mission mission)
		{
				string result = "";
				try {
						int totalStarsCount = mission.levelsManagerComponent.levels.Count * 3;
						int currentStarsCount = 0;
						DataManager.MissionData missionData = DataManager.FindMissionDataById (mission.ID, DataManager.instance.filterdMissionsData);
			
						List<DataManager.LevelData> levelsData = missionData.levelsData;
						foreach (DataManager.LevelData lvl in levelsData) {
								if (lvl.starsNumber == TableLevel.StarsNumber.ONE) {
										currentStarsCount += 1;
								} else if (lvl.starsNumber == TableLevel.StarsNumber.TWO) {
										currentStarsCount += 2;
								} else if (lvl.starsNumber == TableLevel.StarsNumber.THREE) {
										currentStarsCount += 3;
								}
						}
						result = currentStarsCount + " / " + totalStarsCount;
				} catch (Exception ex) {
						//Catch the Exception
				}
				return result;
		}
}