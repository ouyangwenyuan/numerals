using UnityEngine;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

#pragma warning disable 0168 // variable declared but not used.

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class MissionCreator : MonoBehaviour
{
		public Sprite defaultMissionBackground, defaultStarImage, defaultLockSprite;
		public static string missionTitlePrefix = "Mission ";
		private static GameObject groupsParent;
		public Color textShadow = new Color (68, 40, 40, 255) / 255.0f;
		public Font defaultFont;
	
		#if UNITY_EDITOR
		public void Awake ()
		{
				EditorApplication.hierarchyWindowChanged = CheckInstances;
		}
	
		private static void InitGroupsParent ()
		{
				groupsParent = GameObject.Find ("Missions");
		
				if (groupsParent == null) {
						groupsParent = new GameObject ("Missions");
				}
		
				if (groupsParent.GetComponent<RectTransform> () == null) {
						groupsParent.AddComponent<RectTransform> ();
				}
		}

		[MenuItem("Tools/Draw Lines Extra/New Mission #m",false,0)]
		static void CreateNewMission ()
		{
				Mission [] missions = GameObject.FindObjectsOfType (typeof(Mission)) as Mission[];

				if (missions.Length == 99) {
						EditorUtility.DisplayDialog ("Limit Reached", "You can't create more missions", "ok");
						return;
				}

				GameObject missionPrefab = Resources.Load ("Prefabs/Mission") as GameObject;	
				if (missionPrefab == null) {
						Debug.LogWarning ("Mission Prefab is undefined");
						return;
				}
				GameObject newMission = Instantiate (missionPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				newMission.transform.localScale = Vector3.one;
				newMission.transform.localPosition = Vector3.zero;
		}
	
		[MenuItem("Tools/Draw Lines Extra/New Mission #m",true,0)]
		static bool CreateNewMissionValidate ()
		{
				return !Application.isPlaying;
		}

		//Hierarchy Window Changed Event
		private void CheckInstances ()
		{
				Group [] groups = GameObject.FindObjectsOfType (typeof(Group)) as Group[];
				if (groups != null) {
						foreach (Group group in groups) {
								if (group.transform.childCount == 0) {
										GameObject.DestroyImmediate (group.gameObject);
								}
						}
				}

				Mission [] missions = GameObject.FindObjectsOfType (typeof(Mission)) as Mission[];
				if (missions != null) {
						
						bool parentNullOrNotGroup;
						int tempId;
						foreach (Mission mission in missions) {
								parentNullOrNotGroup = false;

								if (mission.transform.parent == null) {
										parentNullOrNotGroup = true;
								} else if (mission.transform.parent.GetComponent<Group> () == null || mission.transform.parent.tag != "Group") {
										parentNullOrNotGroup = true;
								}

								int LastMissionID = GetLastMissionID (missions);

								try {
										if (parentNullOrNotGroup || !int.TryParse (mission.gameObject.name.Split ('-') [1], out tempId)) {
												SetupMission (mission.gameObject, LastMissionID + 1);
										}
								} catch (Exception ex) {
										SetupMission (mission.gameObject, LastMissionID + 1);
								}
						}
				}

		}
	
		//Get the Greatest Mission ID from the Hierarchy
		private int GetLastMissionID (Mission[] missions)
		{
				int ID = 0;
		
				if (missions == null) {
						return ID;
				}
		
				int tempId = 0;
				bool parentNullOrNotGroup;
				foreach (Mission mission in missions) {
						parentNullOrNotGroup = false;
			
						if (mission.transform.parent == null) {
								parentNullOrNotGroup = true;
						} else if (mission.transform.parent.GetComponent<Group> () == null || mission.transform.parent.tag != "Group") {
								parentNullOrNotGroup = true;
						}

						if (parentNullOrNotGroup) {
								continue;
						}
						try {
								if (int.TryParse (mission.gameObject.name.Split ('-') [1], out tempId)) {
										if (ID < tempId) {
												ID = tempId;
										}
								}
				
						} catch (Exception ex) {
						}
				}
				return ID;
		}
	
		//Setup the mission
		private void SetupMission (GameObject mission, int ID)
		{
				if (mission == null) {
						return;
				}
				
				InitGroupsParent ();
				bool displayMessage = false;
				RectTransform missionRectTransform = mission.GetComponent<RectTransform> ();
				if (missionRectTransform == null) {
						missionRectTransform = mission.AddComponent (typeof(RectTransform)) as RectTransform;
				}

				GridLayoutGroup groupsGridLayout = groupsParent.GetComponent<GridLayoutGroup> ();
		
				if (groupsGridLayout == null) {
						//Setting up groups grid layout (initial attributes)
						groupsGridLayout = (GridLayoutGroup)groupsParent.AddComponent (typeof(GridLayoutGroup));
						groupsGridLayout.cellSize = new Vector2 (300, 400);
						groupsGridLayout.spacing = new Vector2 (0, 0);
						groupsGridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
						groupsGridLayout.constraintCount = 1;
						ContentSizeFitter missionsCntentSizeFilter = (ContentSizeFitter)groupsParent.AddComponent (typeof(ContentSizeFitter));
						missionsCntentSizeFilter.horizontalFit = missionsCntentSizeFilter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
						displayMessage = true;
				}
		
				Vector2 cellSize = groupsGridLayout.cellSize;
				//Setting up the mission
				CommonUtil.SetSize (missionRectTransform, cellSize);
				mission.GetComponent<Mission> ().ID = ID;
				mission.GetComponent<Mission> ().missionTitle = "New Mission";
				mission.name = "Mission-" + CommonUtil.IntToString (ID);
				mission.tag = "Mission";

				bool parentNullOrNotGroup = false;
				if (mission.transform.parent == null) {
						parentNullOrNotGroup = true;
				} else if (mission.transform.parent.GetComponent<Group> () == null || mission.transform.parent.tag != "Group") {
						parentNullOrNotGroup = true;
				}
				
				//Create Mission Group
				if (parentNullOrNotGroup) {
						GameObject missionGroup = new GameObject ("Group-" + CommonUtil.IntToString (ID));
						missionGroup.tag = "Group";
						missionGroup.transform.SetParent (groupsParent.transform);
						Group groupComponent = missionGroup.AddComponent (typeof(Group)) as Group;
						groupComponent.Index = ID - 1;
						RectTransform groupRectTransform = missionGroup.AddComponent (typeof(RectTransform)) as RectTransform;
						groupRectTransform.pivot = new Vector2 (0.5f, 1);
						missionGroup.transform.localScale = Vector3.one;
						mission.transform.SetParent (missionGroup.transform);
						missionRectTransform.anchoredPosition = Vector2.zero;
						missionRectTransform.anchorMin = Vector2.zero;
						missionRectTransform.anchorMax = Vector2.one;
						missionRectTransform.offsetMax = Vector2.zero;
						missionRectTransform.offsetMin = Vector2.zero;
						mission.transform.localScale = Vector3.one;
				}
			
				//Get mission image
				Image missionImage = mission.GetComponent<Image> ();
				if (missionImage != null) {
						missionImage.preserveAspect = true;
						if (missionImage.sprite == null)
								missionImage.sprite = defaultMissionBackground;
				}
	
				Text textComponent;
				Image imageComponent;

				//Create mission text
				if (mission.transform.Find ("MissionText") == null) {
						GameObject missionText = new GameObject ("MissionText");
						missionText.transform.SetParent (mission.transform);
						RectTransform missionTextRectTransform = missionText.AddComponent (typeof(RectTransform)) as RectTransform;
						missionText.transform.localScale = Vector3.one;
			
						//Set mission title size
						CommonUtil.SetSize (missionTextRectTransform, new Vector2 (300, 65));
						missionTextRectTransform.anchoredPosition = new Vector2 (0, 90);
			
						//Add mission text
						textComponent = (Text)missionText.AddComponent (typeof(Text));
						textComponent.text = "Mission";
						textComponent.fontSize = 35;
						textComponent.fontStyle = FontStyle.Bold;
						textComponent.alignment = TextAnchor.MiddleCenter;
						textComponent.color = Color.white;
						textComponent.fontStyle = FontStyle.Bold;
						if (defaultFont != null)
								textComponent.font = defaultFont;
			
						//Add mission text shadow
						Shadow missionTextShadow = missionText.AddComponent (typeof(Shadow)) as Shadow;
						missionTextShadow.effectColor = textShadow;
				}

				//Create Mission Title
				if (mission.transform.Find ("Title") == null) {
						//Create new title
						GameObject title = new GameObject ("Title");
						title.transform.SetParent (mission.transform);
						RectTransform missionTitleRectTransform = title.AddComponent (typeof(RectTransform)) as RectTransform;
						title.transform.localScale = Vector3.one;
			
						//Set title size
						CommonUtil.SetSize (missionTitleRectTransform, new Vector2 (300, 65));
						missionTitleRectTransform.anchoredPosition = new Vector2 (0, 30);

						//Add title shadow
						Shadow missionTitleShadow = title.AddComponent (typeof(Shadow)) as Shadow;
						missionTitleShadow.effectColor = textShadow;
			
						//Add mission title
						textComponent = (Text)title.AddComponent (typeof(Text));
						textComponent.text = "New Mission";
						textComponent.fontSize = 35;
						textComponent.fontStyle = FontStyle.Bold;
						textComponent.alignment = TextAnchor.MiddleCenter;
						textComponent.color = Color.white;
						textComponent.fontStyle = FontStyle.Bold;
						if (defaultFont != null)
								textComponent.font = defaultFont;
				}

				//Create mission lock
				if (mission.transform.Find ("Lock") == null) {
						GameObject missionLock = new GameObject ("Lock");
						missionLock.transform.SetParent (mission.transform);
						RectTransform missionLockRectTransform = missionLock.AddComponent (typeof(RectTransform)) as RectTransform;
						missionLock.transform.localScale = Vector3.one;
			
						//Set mission lock size
						CommonUtil.SetSize (missionLockRectTransform, new Vector2 (55, 55));
						missionLockRectTransform.anchoredPosition = new Vector2 (0, -95);
			
						//Add mission lock text
						imageComponent = (Image)missionLock.AddComponent (typeof(Image));
						imageComponent.sprite = defaultLockSprite;
						imageComponent.preserveAspect = true;
						imageComponent.enabled = false;
				}

				//Create Mission Score
				if (mission.transform.Find ("Score") == null) {
						GameObject score = new GameObject ("Score");
						score.transform.SetParent (mission.transform);
						RectTransform missionScoreRectTransform = score.AddComponent (typeof(RectTransform)) as RectTransform;
						score.transform.localScale = Vector3.one;

						//Set Score annchored position
						missionScoreRectTransform.anchoredPosition = new Vector2 (0, -128);

						//Set score size
						CommonUtil.SetSize (missionScoreRectTransform, new Vector2 (100, 65));

						//Add StarsCount child
						GameObject starsCount = new GameObject ("StarsCount");
						starsCount.transform.SetParent (score.transform);
						RectTransform starsCountRectTransform = starsCount.AddComponent (typeof(RectTransform)) as RectTransform;
						starsCount.transform.localScale = Vector3.one;
			
						//Set StarsCount size
						CommonUtil.SetSize (starsCountRectTransform, new Vector2 (160, 30));
			
						//Set StarsCount annchored position
						starsCountRectTransform.anchoredPosition = Vector2.zero;

						//Add StarsCount text
						textComponent = (Text)starsCount.AddComponent (typeof(Text));
						textComponent.text = "100 / 100";//default value
						textComponent.color = new Color (50, 50, 50, 255) / 255.0f;
						textComponent.fontSize = 14;
						textComponent.alignment = TextAnchor.MiddleCenter;
						textComponent.fontStyle = FontStyle.Bold;
						textComponent.resizeTextForBestFit = true;
		
						//Add StarIcon child
						GameObject starsIcon = new GameObject ("Star");
						starsIcon.transform.SetParent (mission.transform);
						RectTransform starsIconRectTransform = starsIcon.AddComponent (typeof(RectTransform)) as RectTransform;
						starsIcon.transform.localScale = Vector3.one;
			
						//Set StarIcon annchored position
						starsIconRectTransform.anchoredPosition = new Vector2 (0, -55);
			
						//Set StarIcon size
						CommonUtil.SetSize (starsIconRectTransform, new Vector2 (50, 50));
						Image starIconImage = starsIcon.AddComponent (typeof(Image)) as Image;
						starIconImage.sprite = defaultStarImage;
						starIconImage.preserveAspect = true;
				}

				Debug.Log ("New Mission 'Mission-" + ID + "' has been created successfully");
				if (displayMessage) {
						EditorUtility.DisplayDialog ("Missions Creator Notification ", "Move the Missions Gameobject to the UICanvas, and then set the scale vector to [1,1,1]", "ok");
				}
		}
		#endif
}