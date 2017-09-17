using UnityEngine;
using UnityEngine.UI;
using System.Collections;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[RequireComponent(typeof(MissionCreator))]
[DisallowMultipleComponent]
public class Missions : MonoBehaviour
{
		/// <summary>
		/// Whether to enable the lock feature for or not.
		/// </summary>
		public bool enableLockFeature = true;
		
		/// <summary>
		/// Whether to create groups pointers or not.
		/// </summary>
		public bool createGroupsPointers = true;

		/// <summary>
		/// Whether to save the last selected group or not.
		/// </summary>
		public bool saveLastSelectedGroup = true;

		/// <summary>
		/// The pointer prefab.
		/// </summary>
		public GameObject pointerPrefab;

		/// <summary>
		/// The pointers parent.
		/// </summary>
		public Transform pointersParent;

		/// <summary>
		/// The last selected group.
		/// </summary>
		private static int lastSelectedGroup;

		/// <summary>
		/// The sprites sequence for the pairs.
		/// </summary>
		public Sprite [] spritesSequence;

		/// <summary>
		/// The connect sprites sequence for the pairs.
		/// </summary>
		public Sprite [] connectSpritesSequence;

		/// <summary>
		/// The colors sequence for the pairs.
		/// </summary>
		public Color [] colorsSequence;

		// Use this for initialization
		void Awake ()
		{
				DataManager.instance.InitGameData ();
				SetUpMissionsFeatures ();
				ScrollSlider scrollSlider = GameObject.FindObjectOfType<ScrollSlider> ();
				if (scrollSlider != null && saveLastSelectedGroup) {
						scrollSlider.currentGroupIndex = lastSelectedGroup;
				}
		}

		/// <summary>
		/// Set up missions features.
		/// </summary>
		private void SetUpMissionsFeatures ()
		{
				bool setUpLastSelectedLevelHash = false;
				if (LevelsTable.lastSelectedGroup == null) {
						LevelsTable.lastSelectedGroup  = new Hashtable();
						setUpLastSelectedLevelHash = true;
				}

				Mission [] missions = GameObject.FindObjectsOfType<Mission> ();

				Mission mission = null;
				for (int i = 0; i < DataManager.instance.filterdMissionsData.Count; i++) {
						mission = FindMissionById (DataManager.instance.filterdMissionsData [i].ID, missions);
						if (mission != null) {
								if (setUpLastSelectedLevelHash) {
										LevelsTable.lastSelectedGroup.Add (mission.ID,0);
								}
								if (enableLockFeature) {//Enable lock feature
										mission.isLocked = DataManager.instance.filterdMissionsData [i].isLocked;
										if (DataManager.instance.filterdMissionsData [i].isLocked) {
												//mission.GetComponent<Button> ().interactable = false;
												mission.transform.Find ("Lock").gameObject.GetComponent<Image> ().enabled = true;
												mission.transform.Find ("Star").gameObject.GetComponent<Image> ().enabled = false;
												mission.transform.Find ("Score").gameObject.SetActive (false);
										} else {
												mission.transform.Find ("Lock").gameObject.GetComponent<Image> ().enabled = false;
										}
							
								} else {//Disable lock feature
										mission.isLocked = false;
										Transform lockGameObject = mission.transform.Find ("Lock");
										if (lockGameObject != null) {
												Image lockImage = lockGameObject.GetComponent<Image> ();
												lockImage.enabled = false;
										}
								}

								if (createGroupsPointers) {//Create Groups Pointers
										if (mission.transform.parent != null)
												Pointer.CreatePointer (i, mission.transform.parent.gameObject, pointerPrefab, pointersParent);
								}
						}
				}
		}

		/// <summary>
		/// Find the mission component by ID.
		/// </summary>
		/// <returns>The mission component by id.</returns>
		/// <param name="ID">ID of mission.</param>
		/// <param name="missions">Missions Components.</param>
		public Mission FindMissionById (int ID, Mission[] missions)
		{
				if (missions == null) {
						return null;
				}

				foreach (Mission mission in missions) {
						if (mission.ID == ID) {
								return mission;
						}
				}
				return null;
		}

		/// <summary>
		/// Raise the change group event.
		/// </summary>
		/// <param name="currentGroup">Current group.</param>
		public void OnChangeGroup (int currentGroup)
		{
				if (saveLastSelectedGroup) {
					lastSelectedGroup = currentGroup;
				}
		}
}
