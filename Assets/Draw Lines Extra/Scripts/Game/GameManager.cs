using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

#pragma warning disable 0168 // variable declared but not used.

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
		/// <summary>
		/// The grid cell prefab.
		/// </summary>
		public GameObject gridCellPrefab;
		[Range(0,1)]
		/// <summary>
	   /// The grid cell cover alpha.
	   /// </summary>
		public float gridCellCoverAlpha = 0.6f;

		/// <summary>
		/// The grid cell z-position.
		/// </summary>
		[Range(-500,500)]
		public float gridCellZPosition = 0;
	
		/// <summary>
		/// The grid line prefab.
		/// </summary>
		public GameObject gridLinePrefab;
	
		/// <summary>
		/// The grid line z-position.
		/// </summary>
		[Range(-50,50)]
		public float gridLineZPosition = -2;

		/// <summary>
		/// The grid line width factor.
		/// </summary>
		[Range(0.001f,0.008f)]
		public float gridLineWidthFactor = 0.003f;

		/// <summary>
		/// The cell content prefab.
		/// </summary>
		public GameObject cellContentPrefab;

		/// <summary>
		/// The cell content scale factor.
		/// </summary>
		[Range(1,5)]
		public float cellContentScaleFactor = 1;

		/// <summary>
		/// The cell content z-position.
		/// </summary>
		[Range(-500,500)]
		public float cellContentZPosition = -5;

		/// <summary>
		/// The dragging element prefab.
		/// </summary>
		public GameObject draggingElementPrefab;
		[Range(0,1)]
		/// <summary>
	   /// The dragging element alpha.
	   /// </summary>
		public float draggingElementAlpha = 0.47f;

		/// <summary>
		/// The dragging element z-position.
		/// </summary>
		[Range(-50,50)]
		public float draggingElementZPosition = 0;

		/// <summary>
		/// The dragging element scale factor.
		/// </summary>
		[Range(0.1f,1)]
		public float draggingElementScaleFactor = 0.8f;

		/// <summary>
		/// The dragging element parent
		/// </summary>
		public Transform draggingElementParent ;

		/// <summary>
		/// The grid.
		/// </summary>
		public GameObject grid;

		/// <summary>
		/// The grid cells rect transform.
		/// </summary>
		public RectTransform gridCellsRectTransform;

		/// <summary>
		/// The grid cells transform.
		/// </summary>
		public Transform gridCellsTransform;

		/// <summary>
		/// The grid cell cover prefab.
		/// </summary>
		public GameObject gridCellCoverPrefab;

		/// <summary>
		/// The grid cells content parent.
		/// </summary>
		public Transform gridCellsContentParent;

		/// <summary>
		/// The grid container reference.
		/// </summary>
		public RectTransform gridContainer;

		/// <summary>
		/// The grid cells attachments reference.
		/// </summary>
		public RectTransform gridCellsAttachments;

		/// <summary>
		/// The grid cells cover parent.
		/// </summary>
		public Transform gridCellsCoverParent;

		/// <summary>
		/// The grid lines transfrom.
		/// </summary>
		public Transform gridLinesTransfrom;

		/// <summary>
		/// The level text.
		/// </summary>
		public Text levelText;

		/// <summary>
		/// The mission text.
		/// </summary>
		public Text missionText;

		/// <summary>
		/// The score text.
		/// </summary>
		public Text levelBestScoreText;

		/// <summary>
		/// The movements text.
		/// </summary>
		public Text movementsText;

		/// <summary>
		/// The flows text.
		/// </summary>
		public Text flowsText;

		/// <summary>
		/// The help count text.
		/// </summary>
		public Text helpCountText;

		/// <summary>
		/// The grid cells in the grid.
		/// </summary>
		public static GridCell[] gridCells;

		/// <summary>
		/// The grid lines in the grid.
		/// </summary>
		public static Line[] gridLines;

		/// <summary>
		/// The suggested line.
		/// </summary>
		private Line suggestedLine;

		/// <summary>
		/// The number of rows of the grid.
		/// </summary>
		public static int numberOfRows;

		/// <summary>
		/// The number of columns of the grid.
		/// </summary>
		public static int numberOfColumns;

		/// <summary>
		/// The water bubble sound effect.
		/// </summary>
		public AudioClip waterBubbleSFX;

		/// <summary>
		/// The level locked sound effect.
		/// </summary>
		public AudioClip levelLockedSFX;

		/// <summary>
		/// The connected sound effect.
		/// </summary>
		public AudioClip connectedSFX;

		/// <summary>
		/// The next button image.
		/// </summary>
		public Image nextButtonImage;

		/// <summary>
		/// The back button image.
		/// </summary>
		public Image backButtonImage;

		/// <summary>
		/// The Star on/off sprite.
		/// </summary>
		public Sprite starOn, starOff;

		/// <summary>
		/// The win dialog.
		/// </summary>
		public WinDialog winDialog;

		/// <summary>
		/// The need help dialog.
		/// </summary>
		public Dialog needHelpDialog;

		/// <summary>
		/// The movements counter.
		/// </summary>
		public static int movements;

		/// <summary>
		/// The current level.
		/// </summary>
		public static Level currentLevel;

		/// <summary>
		/// The current title of the current level.
		/// </summary>
		private string currentLevelTitle;

		/// <summary>
		/// The current level score.
		/// </summary>
		private int currentLevelScore;

		/// <summary>
		/// The timer reference.
		/// </summary>
		public Timer timer;

		/// <summary>
		/// The cursor traget.
		/// </summary>
		public Image cursorTraget;

		/// <summary>
		/// The level stars.
		/// </summary>
		public Image[] levelStars;

		/// <summary>
		/// Whether to enable suggested line or not.
		/// </summary>
		public bool enableSuggestedLine = true;

		/// <summary>
		/// Whether to enable cursor target or not.
		/// </summary>
		public bool enableCursorTarget = true;

		/// <summary>
		/// current line in the grid.
		/// </summary>
		private Line currentLine;

		/// <summary>
		/// Temp ray cast hit 2d for ray casting.
		/// </summary>
		private RaycastHit2D tempRayCastHit2D;

		/// <summary>
		/// Temp click position.
		/// </summary>
		private Vector3 tempClickPosition;

		/// <summary>
		/// The current(selected) grid cell.
		/// </summary>
		private GridCell currentGridCell;

		/// <summary>
		/// The previdous grid cell.
		/// </summary>
		private GridCell previousGridCell;
		
		/// <summary>
		/// The size of the grid.
		/// </summary>
		private Vector2 gridSize;

		/// <summary>
		/// Temp point.
		/// </summary>
		private Vector3 tempPoint;

		/// <summary>
		/// Temp scale.
		/// </summary>
		private Vector3 tempScale;

		/// <summary>
		/// Temp color.
		/// </summary>
		private Color tempColor;

		/// <summary>
		/// Temp collider 2d.
		/// </summary>
		private Collider2D tempCollider2D;

		/// <summary>
		/// Temporary image.
		/// </summary>
		private Image tempImage;

		/// <summary>
		/// Whether the dragging element is rendering(dragging)
		/// </summary>
		private bool drawDraggingElement;

		/// <summary>
		///Whether the current click is moving 
		/// </summary>
		private bool clickMoving;

		/// <summary>
		/// Whether the GameManager is running.
		/// </summary>
		[HideInInspector]
		public static bool
				isRunning;

		/// <summary>
		/// The dragging element reference.
		/// </summary>
		private GameObject draggingElement;

		/// <summary>
		/// The dragging element image.
		/// </summary>
		private Image draggingElementImage;

		/// <summary>
		/// The temporary animator.
		/// </summary>
		private Animator tempAnimator;

		/// <summary>
		/// The help's counter.
		/// </summary>
		private static int helpCount;

		/// <summary>
		/// Whether the game started or not
		/// </summary>
		private static bool gameStarted;

		/// <summary>
		/// The effects audio source.
		/// </summary>
		private AudioSource effectsAudioSource;

		/// <summary>
		/// The current mission data.
		/// </summary>
		private DataManager.MissionData currentMissionData;

		/// <summary>
		/// The current level data.
		/// </summary>
		private DataManager.LevelData currentLevelData;

		void Start ()
		{
				//Setting up the references
				if (nextButtonImage == null) {
						nextButtonImage = GameObject.Find ("NextButton").GetComponent<Image> ();
				}
		
				if (backButtonImage == null) {
						backButtonImage = GameObject.Find ("BackButton").GetComponent<Image> ();
				}
		
				if (effectsAudioSource == null) {
						effectsAudioSource = GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1];
				}

				if (movementsText == null) {
						movementsText = GameObject.Find ("Movements").GetComponent<Text> ();
				}

				if (levelText == null) {
						levelText = GameObject.Find ("GameLevel").GetComponent<Text> ();
				}

				if (missionText == null) {
						missionText = GameObject.Find ("MissionTitle").GetComponent<Text> ();
				}

				if (levelBestScoreText == null) {
						levelBestScoreText = GameObject.Find ("LevelBestScore").GetComponent<Text> ();
				}

				if (flowsText == null) {
						flowsText = GameObject.Find ("Flows").GetComponent<Text> ();
				}

				if (helpCountText == null) {
						helpCountText = GameObject.Find ("HelpCount").GetComponent<Text> ();
				}

				if (grid == null) {
						grid = GameObject.Find ("Grid");
				}

				if (gridCellsTransform == null) {
						gridCellsTransform = grid.transform.Find ("GridCells").transform;
				}

				if (gridCellsRectTransform == null) {
						gridCellsRectTransform = gridCellsTransform.GetComponent<RectTransform> ();
				}

				if (gridLinesTransfrom == null) {
						gridLinesTransfrom = GameObject.Find ("GridLines").transform;
				}

				if (winDialog == null) {
						winDialog = GameObject.FindObjectOfType<WinDialog> ();
				}

				if (needHelpDialog == null) {
						needHelpDialog = GameObject.Find ("NeedHelpDialog").GetComponent<Dialog> ();
				}

				if (timer == null) {
						timer = GameObject.Find ("Time").GetComponent<Timer> ();
				}

				if (cursorTraget == null) {
						cursorTraget = GameObject.Find ("CursorTarget").GetComponent<Image> ();
				}

				//Reset help count
				if (!gameStarted) {
						gameStarted = true;
						ResetHelpCount (false);
				} else {
						SetHelpCountText ();
				}

				try {
						//Setting up Attributes
						numberOfRows = Mission.selectedMission.rowsNumber;
						numberOfColumns = Mission.selectedMission.colsNumber;
						//levelText.color = Mission.selectedMission.missionColor;
						missionText.text = Mission.selectedMission.missionTitle;
						grid.name = numberOfRows + "x" + numberOfRows + "-Grid";
						//get current Mission data
						currentMissionData = DataManager.FindMissionDataById (Mission.selectedMission.ID, DataManager.instance.filterdMissionsData);
				} catch (Exception ex) {
						Debug.Log (ex.Message);
				}

				//Make gridCellsAttachments similar as the gridContainer
				CommonUtil.SetRectTransformAs (gridCellsAttachments, gridContainer);
				//Make gridCellsCoverParent similar as the grid
				CommonUtil.SetRectTransformAs (gridCellsCoverParent.GetComponent<RectTransform> (), grid.GetComponent<RectTransform>());

				//Create New level (the selected level)
				CreateNewLevel ();
		}

		/// <summary>
		/// When the GameObject becomes invisible.
		/// </summary>
		void OnDisable ()
		{
				//stop the timer
				if (timer != null)
						timer.Stop ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				GameLoop ();
		}

		/// <summary>
		/// The Game's loop.
		/// </summary>
		private void GameLoop ()
		{
				if (!isRunning) {
						return;
				}
		
				if (gridLines == null || gridCells == null) {
						return;
				}
		
				if (Input.GetMouseButtonDown (0)) {
						RayCast (Input.mousePosition, ClickType.Began);
				} else if (Input.GetMouseButtonUp (0)) {
						Release (currentLine);
				}
		
				if (clickMoving) {
						RayCast (Input.mousePosition, ClickType.Moved);
				} else {
						RayCast (Input.mousePosition, ClickType.None);
				}
		
				if (drawDraggingElement) {
						if (!draggingElementImage.enabled) {
								draggingElementImage.enabled = true;
						}
			
						DrawDraggingElement (Input.mousePosition);
				} else {
						if (draggingElementImage.enabled) {
								draggingElementImage.enabled = false;
						}
				}
		}

		/// <summary>
		/// On Click Release event.
		/// </summary>
		/// <param name="line">The current Line.</param>
		private void Release (Line line)
		{
				clickMoving = false;
				drawDraggingElement = false;
				draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter> ().emit = false;
				draggingElementImage.enabled = false;

				if (line != null) {
						if (!line.completedLine) {
								line.ClearPath ();
								currentLevelScore++;
						}
				}

				previousGridCell = null;
				currentGridCell = null;
				currentLine = null;
				if (enableSuggestedLine) {
						StartCoroutine ("SetUpNewSuggestedLine");
				}
		}

		/// <summary>
		/// Raycast the click (touch) on the screen.
		/// </summary>
		/// <param name="clickPosition">The position of the click (touch).</param>
		/// <param name="clickType">The type of the click(touch).</param>
		private void RayCast (Vector3 clickPosition, ClickType clickType)
		{
				tempClickPosition = Camera.main.ScreenToWorldPoint (clickPosition);
				tempRayCastHit2D = Physics2D.Raycast (tempClickPosition, Vector2.zero);
				tempCollider2D = tempRayCastHit2D.collider;

				if (tempCollider2D != null) {
						//When a ray hit a grid cell
						if (tempCollider2D.tag == "GridCell") {
								currentGridCell = tempCollider2D.GetComponent<GridCell> ();
								EnableCursorTraget (currentGridCell.transform.position);
								if (clickType == ClickType.Began) {
										previousGridCell = currentGridCell;
										drawDraggingElement = true;
										draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter> ().emit = true;
										GridCellClickBegan ();
										DisableCursorTarget ();
								} else if (clickType == ClickType.Moved) {
										DisableCursorTarget ();
										drawDraggingElement = true;
										GridCellClickMoved ();
								}
						} else {
								DisableCursorTarget ();
						}
				} else {
						DisableCursorTarget ();
				}
		}

		/// <summary>
		/// When a click(touch) began on the GridCell.
		/// </summary>
		private void GridCellClickBegan ()
		{
				//If the current grid cell is not currently used and it's empty,then ignore it
				if (currentGridCell.isEmpty && !currentGridCell.currentlyUsed) {
						Debug.Log ("Current grid cell of index " + currentGridCell.index + " is Ignored [Reason : Empty,Not Currently Used]");
						return;
				} 

				//If the grid cell is currently used
				if (currentGridCell.currentlyUsed) {

						if (currentGridCell.gridLineIndex == -1) {
								return;
						}

						//If the grid line of the current grid cell is completed,then reset the grid cells of the line
						if (gridLines [currentGridCell.gridLineIndex].completedLine) {
								Debug.Log ("Reset Grid cells of the Line Path of the index " + currentGridCell.gridLineIndex);
								gridLines [currentGridCell.gridLineIndex].completedLine = false;
								SetFlowsText ();
								Release (gridLines [currentGridCell.gridLineIndex]);
								return;
						} 

				} else if (!currentGridCell.isEmpty) {//If the grid is not currently used and it's not empty
						clickMoving = true;
						OnNewGridCell (currentGridCell, previousGridCell, false);
				}
		}

		/// <summary>
		/// On new grid cell.
		/// </summary>
		/// <param name="currentGridCell">Current grid cell.</param>
		/// <param name="previousGridCell">Previous grid cell.</param>
		/// <param name="increaseMovements">If set to <c>true</c> increase movements.</param>
		private void OnNewGridCell (GridCell currentGridCell, GridCell previousGridCell, bool increaseMovements)
		{
				if (currentLine == null) {
						currentLine = gridLines [currentGridCell.gridLineIndex];
				}

				if (increaseMovements) {
						//Increase the movements counter
						IncreaseMovements ();
				}

				//Setting up the attributes for the current grid cell
				currentGridCell.currentlyUsed = true;
				currentGridCell.gridLineIndex = previousGridCell.gridLineIndex;

				if (currentGridCell.gridLineIndex == -1) {
						return;
				}

				if (currentGridCell.isEmpty)
						currentGridCell.tragetIndex = previousGridCell.tragetIndex;

				Debug.Log ("New GridCell of Index " + currentGridCell.index + " added to the Line Path of index " + currentLine.index);

				//Add the current grid cell index to the current traced grid cells list
				currentLine.path.Add (currentGridCell.index);

				//Determine the New Line Point
				tempPoint = currentGridCell.transform.position;
				tempPoint.z = gridLineZPosition;

				//Set dragging Element color
				tempColor = currentLevel.elementsPairs [gridCells [currentLine.GetFirstPathElement ()].elementPairIndex].color;
				tempColor.a = gridCellCoverAlpha;
				draggingElementImage.color = tempColor;

				//Add the position of the New Line Point to the current line
				gridLines [currentGridCell.gridLineIndex].AddPoint (tempPoint);
		}

		/// <summary>
		/// When a click(touch) moved over the GridCell.
		/// </summary>
		private void GridCellClickMoved ()
		{
				if (currentLine == null) {
						Debug.Log ("Current Line is undefined");
						return;
				}

				if (currentGridCell == null) {
						Debug.Log ("Current GridCell is undefined");
						return;
				}

				if (previousGridCell == null) {
						Debug.Log ("Previous GridCell is undefined");
						return;
				}
				
				if (enableSuggestedLine) {
						DisableSuggestedLine ();
						StopCoroutine ("SetUpNewSuggestedLine");
				}

				if (currentGridCell.index == previousGridCell.index) {
						return;
				}
	
				//If the current grid cell is not adjacent of the previous grid cell,then ignore it
				if (!previousGridCell.OneOfAdjacents (currentGridCell.index)) {
						Debug.Log ("Current grid cell of index " + currentGridCell.index + " is Ignored [Reason : Not Adjacent Of Previous GridCell " + previousGridCell.index);
						return;
				}

				//If the current grid cell is currently used
				if (currentGridCell.currentlyUsed) {
					
						if (currentGridCell.gridLineIndex == -1) {
								return;
						}

						if (currentGridCell.gridLineIndex == previousGridCell.gridLineIndex) {
							
								gridLines [currentGridCell.gridLineIndex].RemoveElements (currentGridCell.index);
								previousGridCell = currentGridCell;
								Debug.Log ("Remove some Elements from the Line Path of index " + currentGridCell.gridLineIndex);
								//Increase the movements counter
								IncreaseMovements ();
								return;//skip next
						} else {
								Debug.Log ("Clear the Line Path of index " + currentGridCell.gridLineIndex);
								gridLines [currentGridCell.gridLineIndex].ClearPath ();
								currentLevelScore++;
						}
				}

				//If the current grid cell is not empty or it's not a partner of the previous grid cell
				if (!currentGridCell.isEmpty && currentGridCell.index != previousGridCell.tragetIndex) {
						Debug.Log ("Current grid cell of index " + currentGridCell.index + " is Ignored [Reason : Not the wanted Traget]");
						return;//skip next
				}

				OnNewGridCell (currentGridCell, previousGridCell, true);

				bool pathCompleted = false;

				if (!currentGridCell.isEmpty) {
						//Path is completed
						if (previousGridCell.tragetIndex == currentGridCell.index) {
								OnCompletePath (currentLine, currentLevel.elementsPairs [gridCells [currentLine.GetFirstPathElement ()].elementPairIndex].lineColor);
								pathCompleted = true;
								return;
						}
				}

				if (!pathCompleted) {
						//Play the water bubble sound effect at the center of the unity world
						if (waterBubbleSFX != null && effectsAudioSource != null) {
								CommonUtil.PlayOneShotClipAt (waterBubbleSFX, Vector3.zero, effectsAudioSource.volume);
						}
				}

				previousGridCell = currentGridCell;
		}

		/// <summary>
		/// Refreshs(Reset) the grid.
		/// </summary>
		public void RefreshGrid ()
		{
				movements = 0;
				currentLevelScore = 0;
				if (movementsText != null)
						SetMovementsText ();

				timer.Stop ();

				if (gridLines != null) {
						for (int i = 0; i <gridLines.Length; i++) {
								if (gridLines [i].completedLine)
										gridLines [i].ClearPath ();
						}
				}

				SetFlowsText ();
				currentLine = null;
				currentGridCell = previousGridCell = null;
				timer.Start ();
		}

		/// <summary>
		/// Draw(Drag) the dragging element.
		/// </summary>
		/// <param name="clickPosition">Click position.</param>
		private void DrawDraggingElement (Vector3 clickPosition)
		{
				if (draggingElement == null) {
						return;
				}

				tempClickPosition = Camera.main.ScreenToWorldPoint (clickPosition);
				tempClickPosition.z = draggingElementZPosition;
				draggingElement.transform.position = tempClickPosition;
		}

		/// <summary>
		/// Create a new level.
		/// </summary>
		private void CreateNewLevel ()
		{
				try {
						StopAllCoroutines ();
						currentLevelScore = 0;
						movements = 0;
						currentLevelTitle = "Level " + TableLevel.selectedLevel.ID;
						levelText.text = currentLevelTitle;
						currentLevel = Mission.selectedMission.levelsManagerComponent.levels [TableLevel.selectedLevel.ID - 1];
						currentLevelData = currentMissionData.FindLevelDataById (TableLevel.selectedLevel.ID);
						ResetGameContents ();
						BuildTheGrid ();
						SettingUpPairs ();
						SettingUpNextBackAlpha ();
						SetCurrentLevelBestScoreText (currentLevelData.bestScore);
						SetMovementsText ();
						SetFlowsText ();
						SetLevelStars ();
						timer.Stop ();
						timer.Start ();
						isRunning = true;
						if (enableSuggestedLine) {
								StartCoroutine ("SetUpNewSuggestedLine");
						}
						AdsManager.instance.HideAdvertisment ();
				} catch (Exception ex) {
						Debug.LogWarning ("Make sure you have selected a level, and there are no empty references in GameManager component");
				}
		}

		/// <summary>
		/// Build the grid.
		/// </summary>
		private void BuildTheGrid ()
		{
				Debug.Log ("Setting up the Grid " + numberOfRows + "x" + numberOfColumns);
		
				//Calculate the size of the grid cell
				Vector3 gridCellSize = new Vector2 (gridCellsRectTransform.rect.width / numberOfColumns, gridCellsRectTransform.rect.height / numberOfRows);

				Vector3 gridCellPosition = Vector3.zero;
				gridCellPosition.z = gridCellZPosition;
		
				gridCells = new GridCell[numberOfRows * numberOfColumns];
				GameObject gridCell, gridCellCover;
				GridCell gridCellComponent;
				RectTransform rectTransform;
				int gridCellIndex;
				float x, y;//grid cell x,y offset

				//Set cursor target size
				CommonUtil.SetSize (cursorTraget.GetComponent<RectTransform> (), gridCellSize);

				for (int i = 0; i < numberOfRows; i++) {
						for (int j = 0; j < numberOfColumns; j++) {
				
								//Calculate grid cell index
								gridCellIndex = i * numberOfColumns + j;
				
								//Create a new grid cell
								gridCell = Instantiate (gridCellPrefab) as GameObject;

								//Set the background of the grid cell
								SetGridCellBackground (gridCell, i, j);

								//Name the new grid cell
								gridCell.name = "GridCell-" + gridCellIndex;
								//Set the new grid cell parent
								gridCell.transform.SetParent (gridCellsTransform);
								//Set the scale of the new grid cell to one
								gridCell.transform.localScale = Vector3.one;
								//Set the position of the grid cell
								gridCell.transform.localPosition = gridCellPosition;
								//Set the collider size for the new grid cell
								gridCell.GetComponent<BoxCollider2D> ().size = gridCellSize;

								//Move and size the new grid cell
								rectTransform = gridCell.GetComponent<RectTransform> ();
								x = -gridCellsRectTransform.rect.width / 2 + gridCellSize.x * j;
								y = gridCellsRectTransform.rect.height / 2 - gridCellSize.y * (i + 1);
								rectTransform.offsetMin = new Vector2 (x, y);
				
								x = rectTransform.offsetMin.x + gridCellSize.x;
								y = rectTransform.offsetMin.y + gridCellSize.y;
								rectTransform.offsetMax = new Vector2 (x, y);

								//Create grid cell cover
								gridCellCover = Instantiate (gridCellCoverPrefab) as GameObject;
								gridCellCover.name = gridCell.name + " Cover";
								//Set the grid cell cover parent
								gridCellCover.transform.SetParent (gridCellsCoverParent);
								//Set the position of the grid cell cover
								gridCellCover.transform.localScale = Vector3.one;
								//Set the scale of the grid cell cover to one
								gridCellCover.transform.localPosition = gridCellPosition;
								//Set the offset of the grid cell cover
								gridCellCover.GetComponent<RectTransform> ().offsetMin = rectTransform.offsetMin;
								gridCellCover.GetComponent<RectTransform> ().offsetMax = rectTransform.offsetMax;

								//Get GridCell component
								gridCellComponent = gridCell.GetComponent<GridCell> ();
								//Set the grid cell index
								gridCellComponent.index = gridCellIndex;
								//Set the grid cell cover reference
								gridCellComponent.cover = gridCellCover.GetComponent<Image> ();
								gridCells [gridCellIndex] = gridCellComponent;
				
								//Define the adjacents of the grid cell
								gridCellComponent.DefineAdjacents (i, j, Mission.selectedMission.levelsManagerComponent.allowedMovements);
						}
				}
		}

		/// <summary>
		/// Set the grid cell background.
		/// </summary>
		/// <param name="gridCell">Grid cell.</param>
		/// <param name="i">The row's index.</param>
		/// <param name="j">The column's index</param>
		private void SetGridCellBackground (GameObject gridCell, int i, int j)
		{
				if (gridCell == null) {
						return;
				}

				if (i % 2 == 0) {
						if (j % 2 == 0) {
								gridCell.GetComponent<Image> ().sprite = Mission.selectedMission.levelsManagerComponent.firstGridCellBackground;
						} else {
								gridCell.GetComponent<Image> ().sprite = Mission.selectedMission.levelsManagerComponent.secondGridCellBackground;
						}
				} else {
						if (j % 2 == 0) {
								gridCell.GetComponent<Image> ().sprite = Mission.selectedMission.levelsManagerComponent.secondGridCellBackground;
						} else {
								gridCell.GetComponent<Image> ().sprite = Mission.selectedMission.levelsManagerComponent.firstGridCellBackground;
						}
				}
		}

		/// <summary>
		/// Set the level stars.
		/// </summary>
		private void SetLevelStars ()
		{
				levelStars [0].sprite = starOn;
				levelStars [1].sprite = starOn;
				levelStars [2].sprite = starOn;

				if (TableLevel.selectedLevel.starsNumber == TableLevel.StarsNumber.ZERO) {
						if (levelStars [0] != null) {
								levelStars [0].sprite = starOff;
						}
			
						if (levelStars [1] != null) {
								levelStars [1].sprite = starOff;
						}
			
						if (levelStars [2] != null) {
								levelStars [2].sprite = starOff;
						}
				} else if (TableLevel.selectedLevel.starsNumber == TableLevel.StarsNumber.ONE) {
						if (levelStars [1] != null) {
								levelStars [1].sprite = starOff;
						}
						if (levelStars [2] != null) {
								levelStars [2].sprite = starOff;
						}
				} else if (TableLevel.selectedLevel.starsNumber == TableLevel.StarsNumber.TWO) {
						if (levelStars [2] != null) {
								levelStars [2].sprite = starOff;
						}
				}
		}

		/// <summary>
		/// Setting up the cells pairs.
		/// </summary>
		private void SettingUpPairs ()
		{
				Debug.Log ("Setting up the Cells Pairs");
	
				if (currentLevel == null) {
						Debug.Log ("level is undefined");
						return;
				}
			
				if (Mission.selectedMission.levelsManagerComponent.randomShuffleOnBuild) {
						//Random Shuffle on Build
				}

				Text numberText;
				Level.ElementsPair elementsPair = null;
				Transform gridCellTransform;
				GridCell gridCell;
				Vector3 gridCellPosition = Vector3.zero;
				Vector3 gridLinePostion = Vector3.zero;
				GameObject firstElement = null;
				GameObject secondElement = null;
				RectTransform gridCellRectTransform = null;
				RectTransform elementRectTransform;
				float x, y;

				gridLines = new Line[currentLevel.elementsPairs.Count];
				gridLinePostion.z = gridLineZPosition;

				for (int i = 0; i <currentLevel.elementsPairs.Count; i++) {
			
						elementsPair = currentLevel.elementsPairs [i];

						//Setting up the First Element
						gridCell = gridCells [elementsPair.firstElement.index];
						gridCellRectTransform = gridCell.GetComponent<RectTransform> ();
						gridCell.gridLineIndex = i;
						gridCell.elementPairIndex = i;
						gridCell.isEmpty = false;
						gridCell.tragetIndex = elementsPair.secondElement.index;
						gridCellTransform = gridCell.gameObject.transform;
						gridCellPosition = gridCellTransform.localPosition;

						Vector2 cellContentsSize = new Vector2 (gridCellRectTransform.rect.width, gridCellRectTransform.rect.height);

						firstElement = Instantiate (cellContentPrefab) as GameObject;
						firstElement.transform.SetParent (gridCellTransform);

						firstElement.name = "Pair" + (i + 1) + "-FirstElement";
						firstElement.GetComponent<Image> ().sprite = elementsPair.sprite;

						firstElement.transform.localPosition = new Vector3 (gridCellPosition.x, gridCellPosition.y, cellContentZPosition);
						firstElement.transform.localScale = Vector3.one;
						elementRectTransform = firstElement.GetComponent<RectTransform> ();

						//Move and size the first element
						x = -gridCellRectTransform.rect.width / 2.0f;
						y = (gridCellRectTransform.rect.height / 2.0f - cellContentsSize.y);
						elementRectTransform.offsetMin = new Vector2 (x / cellContentScaleFactor, y / cellContentScaleFactor);
			
						x = elementRectTransform.offsetMin.x + cellContentsSize.x / cellContentScaleFactor;
						y = elementRectTransform.offsetMin.y + cellContentsSize.y / cellContentScaleFactor;
						elementRectTransform.offsetMax = new Vector2 (x, y);

						if (Mission.selectedMission.levelsManagerComponent.applyColorOnSprite) {
								firstElement.GetComponent<Image> ().color = elementsPair.color;//apply the sprite color
						} else {
								firstElement.GetComponent<Image> ().color = Color.white;//apply the white color
						}
				
						//Change parent from GridCell to gridCellsContentParent
						firstElement.transform.SetParent (gridCellsContentParent);

						numberText = firstElement.transform.Find ("Number").GetComponent<Text> ();
						numberText.text = (i + 1).ToString ();
						numberText.color = elementsPair.numberColor; 

						if (!Mission.selectedMission.levelsManagerComponent.enablePairsNumber) {
								numberText.gameObject.SetActive (false);
						}

						gridCell.content = firstElement.transform;

						//Setting up the Second Element
						gridCell = gridCells [elementsPair.secondElement.index];
						gridCellRectTransform = gridCell.GetComponent<RectTransform> ();
						gridCell.gridLineIndex = i;
						gridCell.elementPairIndex = i;
						gridCell.isEmpty = false;
						gridCell.tragetIndex = elementsPair.firstElement.index;
						gridCellTransform = gridCell.gameObject.transform;
						gridCellPosition = gridCellTransform.localPosition;
		
						cellContentsSize = new Vector2 (gridCellRectTransform.rect.width, gridCellRectTransform.rect.height);
			
						secondElement = Instantiate (cellContentPrefab) as GameObject;
						secondElement.transform.SetParent (gridCellTransform);
			
						secondElement.name = "Pair" + (i + 1) + "-SecondElement";
						secondElement.GetComponent<Image> ().sprite = elementsPair.sprite;
			
						secondElement.transform.localPosition = new Vector3 (gridCellPosition.x, gridCellPosition.y, cellContentZPosition);
						secondElement.transform.localScale = Vector3.one;
						elementRectTransform = secondElement.GetComponent<RectTransform> ();

						//Move and size the second element
						x = -gridCellRectTransform.rect.width / 2.0f;
						y = (gridCellRectTransform.rect.height / 2.0f - cellContentsSize.y);
						elementRectTransform.offsetMin = new Vector2 (x / cellContentScaleFactor, y / cellContentScaleFactor);
			
						x = elementRectTransform.offsetMin.x + cellContentsSize.x / cellContentScaleFactor;
						y = elementRectTransform.offsetMin.y + cellContentsSize.y / cellContentScaleFactor;
						elementRectTransform.offsetMax = new Vector2 (x, y);

						//Change parent from GridCell to gridCellsContentParent
						secondElement.transform.SetParent (gridCellsContentParent);

						if (Mission.selectedMission.levelsManagerComponent.applyColorOnSprite) {
								secondElement.GetComponent<Image> ().color = elementsPair.color;//apply the sprite color
						} else {
								secondElement.GetComponent<Image> ().color = Color.white;//apply the white color
						}
			
						numberText = secondElement.transform.Find ("Number").GetComponent<Text> ();
						numberText.text = (i + 1).ToString ();
						numberText.color = elementsPair.numberColor; 
			
						if (!Mission.selectedMission.levelsManagerComponent.enablePairsNumber) {
								numberText.gameObject.SetActive (false);
						}

						gridCell.content = secondElement.transform;
						
						//Create Grid Line
						CreateGridLine (gridCellRectTransform.rect.size.y * gridLineWidthFactor, elementsPair.lineColor, "Line " + elementsPair.secondElement.index + "-" + elementsPair.secondElement.index, gridCells [elementsPair.firstElement.index], gridCells [elementsPair.secondElement.index], gridLinePostion, i);
				}
		
				Color tempColor = Mission.selectedMission.missionColor;
				tempColor.a = 120 / 255.0f;
		
				CreateDraggingElement (tempColor, gridCellRectTransform.rect.size * draggingElementScaleFactor);
		}

		
		/// <summary>
		/// Setting up Grid Lines
		/// </summary>
		/// <param name="lineWidth">Line width.</param>
		/// <param name="lineColor">Line color.</param>
		/// <param name="name">Name.</param>
		/// <param name="pos">Position.</param>
		/// <param name="index">Index.</param>
		private void CreateGridLine (float lineWidth, Color lineColor, string name, GridCell firstGridCell, GridCell secondGridCell, Vector3 pos, int index)
		{
				GameObject gridLine = Instantiate (gridLinePrefab, pos, Quaternion.identity) as GameObject;
				gridLine.transform.parent = gridLinesTransfrom;
				gridLine.name = name;
				Line line = gridLine.GetComponent<Line> ();
				line.pointZPosition = pos.z;
				line.SetWidth (lineWidth);
				line.SetColor (lineColor);
				line.firstGridCell = firstGridCell;
				line.secondGridCell = secondGridCell;
				if (gridLines != null) {
						gridLines [index] = line;
						gridLines [index].index = index;
				}
		}
	
		/// <summary>
		/// Creates the dragging element.
		/// </summary>
		/// <param name="color">Color of the dragging element.</param>
		/// <param name="Size">Size of the dragging element.</param>
		private void CreateDraggingElement (Color color, Vector2 size)
		{
				GameObject currentDraggingElement = GameObject.Find ("DraggingElement");
				if (draggingElement == null) {
						draggingElement = Instantiate (draggingElementPrefab) as GameObject;
						if (draggingElementParent != null)
								draggingElement.transform.SetParent (draggingElementParent);
						draggingElement.name = "DraggingElement";
						draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter> ().emit = false;
				} else {
						draggingElement = currentDraggingElement;
						draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter> ().emit = false;
				}
		
				draggingElement.transform.localScale = Vector3.one;
				CommonUtil.SetSize (draggingElement.GetComponent<RectTransform> (), size);
				draggingElementImage = draggingElement.GetComponent<Image> ();
				draggingElementImage.color = color;
				draggingElementImage.enabled = false;
		}

		/// <summary>
		/// Go to the next level.
		/// </summary>
		public void NextLevel ()
		{
					if (TableLevel.selectedLevel.ID >= 1 && TableLevel.selectedLevel.ID < LevelsTable.levels.Count) {
						//Get the next level and check if it's locked , then do not load the next level
						DataManager.MissionData currentMissionData = DataManager.FindMissionDataById (Mission.selectedMission.ID, DataManager.instance.filterdMissionsData);//Get the current mission
						if (TableLevel.selectedLevel.ID + 1 <= currentMissionData.levelsData.Count) {
								DataManager.LevelData nextLevelData = currentMissionData.FindLevelDataById (TableLevel.selectedLevel.ID + 1);///Get the next level
								if (nextLevelData.isLocked) {
										//Play lock sound effectd
										if (levelLockedSFX != null && effectsAudioSource != null) {
												CommonUtil.PlayOneShotClipAt (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
										}
										//Skip the next
										return;
								}
						}
						TableLevel.selectedLevel = LevelsTable.levels [TableLevel.selectedLevel.ID];//Set the selected level
						CreateNewLevel ();//Create new level

				} else {
						//Play lock sound effectd
						if (levelLockedSFX != null && effectsAudioSource != null) {
								CommonUtil.PlayOneShotClipAt (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
						}
				}
		}

		//// <summary>
		/// Back to the previous level.
		/// </summary>
		public void PreviousLevel ()
		{
				if (TableLevel.selectedLevel.ID > 1 && TableLevel.selectedLevel.ID <= LevelsTable.levels.Count) {
			          TableLevel.selectedLevel = LevelsTable.levels [TableLevel.selectedLevel.ID - 2];
						CreateNewLevel ();
				} else {
						//Play lock sound effectd
						if (levelLockedSFX != null && effectsAudioSource != null) {
								CommonUtil.PlayOneShotClipAt (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
						}
				}
		}

		/// <summary>
		/// On path complete event.
		/// </summary>
		/// <param name="currentLine">Current line.</param>
		/// <param name="coverColor">Grid Cell cover color.</param>
		private void OnCompletePath (Line currentLine, Color coverColor)
		{
				Debug.Log ("Path completed between [GridCell " + (currentLine.GetFirstPathElement ()) + " and GridCell " + (currentLine.GetLastPathElement ()) + "]");

				currentLine.completedLine = true;
				GridCell gridCell = null;
				SetFlowsText ();
				currentLevelScore++;

				for (int i = 0; i < currentLine.path.Count; i++) {
						gridCell = gridCells [currentLine.path [i]];

						if (i == 0 || i == currentLine.path.Count - 1) {
								//Setup the connect sprite
								gridCell.content.GetComponent<Image> ().sprite = currentLevel.elementsPairs [gridCell.elementPairIndex].connectSprite;
						}

						EnableGridCellCover (gridCell, coverColor);
				}

				//Play the connected sound effect at the center of the unity world
				if (connectedSFX != null && effectsAudioSource != null) {
						CommonUtil.PlayOneShotClipAt (connectedSFX, Vector3.zero, effectsAudioSource.volume);
				}
			
				Release (null);
				CheckLevelComplete ();
		}

		/// <summary>
		/// Checks Wheter the level is completed.
		/// </summary>
		private void CheckLevelComplete ()
		{
				if (gridLines == null) {
						return;
				}
		
				bool isLevelComplete = true;
		
				for (int i = 0; i < gridLines.Length; i++) {
						if (!gridLines [i].completedLine) {
								isLevelComplete = false;
								break;
						}
				}
		
				//All grid cells must be used, comment to disable this feature
				for (int i = 0; i < gridCells.Length; i++) {
						if (!gridCells [i].currentlyUsed) {
								isLevelComplete = false;
								break;
						}
				}
		
				if (isLevelComplete) {
						timer.Stop ();
						isRunning = false;
			
						try {

								//Save the stars level
								if (currentLevelData.ID == currentMissionData.levelsData.Count) {
										if (currentMissionData.ID + 1 <= DataManager.instance.filterdMissionsData.Count) {
												//Unlock the next mission
												DataManager.MissionData nextMissionData = DataManager.FindMissionDataById (currentMissionData .ID + 1, DataManager.instance.filterdMissionsData);
												nextMissionData.isLocked = false;
										}
								}
				
								currentLevelData.starsNumber = StarsRating.GetLevelStarsRating (timer.timeInSeconds, GameManager.movements, gridCells.Length);
								SetCurrentLevelBestScore ();

								TableLevel.selectedLevel.starsNumber = currentLevelData.starsNumber;
								if (currentLevelData .ID + 1 <= currentMissionData.levelsData.Count) {
										//Unlock the next level
										DataManager.LevelData nextLevelData = currentMissionData.FindLevelDataById (TableLevel.selectedLevel.ID + 1);
										nextLevelData.isLocked = false;
								}

								DataManager.instance.SaveMissions (DataManager.instance.filterdMissionsData);
						} catch (Exception ex) {
								Debug.LogError (ex.Message);
						}
			
						//Show the black area
						BlackArea.Show ();

						//Show the win dialog
						winDialog.SetLevelTitle (currentLevelTitle);
						winDialog.SetScore (currentLevelScore);
						winDialog.SetBestScore (currentLevelData.bestScore);
						winDialog.Show ();
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_SHOW_WIN_DIALOG);
						Debug.Log ("Level " + TableLevel.selectedLevel.ID +" completed");
				}
		}

		/// <summary>
		/// Get a Help.
		/// </summary>
		public void Help ()
		{
				if (gridLines == null || !isRunning) {
						return;
				}

				Debug.Log ("I need help...");

				//Skip next if the helpcount is zero
				if (helpCount == 0) {
						isRunning = false;
						//Show NeedHelp dialog when the user click on the help button and the helpcount is zero
						needHelpDialog.Show ();
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_SHOW_NEED_HELP_DIALOG);
						return;
				}

				try {
						List<int> selectedElementsPairs = new List<int> ();
			
						bool skip;//whehter to skip the elements pair or not
						
						//Get all not used elements paris
						for (int i = 0; i < currentLevel.elementsPairs.Count; i++) {
								if (currentLevel.elementsPairs [i].helpPath.Count == 0) {
										continue;
								}

								skip = false;
								for (int j = 0; j < currentLevel.elementsPairs[i].helpPath.Count; j++) {
										if (gridCells [currentLevel.elementsPairs [i].helpPath [j]].currentlyUsed) {
												skip = true;
												break;
										}
								}

								if (!skip)
										selectedElementsPairs.Add (i);
						}

						//Select random elements pair
						Level.ElementsPair selectedElementsPair = null;
						if (selectedElementsPairs.Count != 0)
								selectedElementsPair = currentLevel.elementsPairs [selectedElementsPairs [UnityEngine.Random.Range (0, selectedElementsPairs.Count)]];

						//Select random help path
						List<int> selectedPath = new List<int> ();
						if (selectedElementsPair != null)
								selectedPath = selectedElementsPair.helpPath;
						else
								return;
					
						helpCount--;
						SetHelpCountText ();

						//Show NeedHelp dialog when the helpcount is zero
						if (helpCount == 0 && GetInCompletedLinesCount () != gridLines.Length - 1) {
								isRunning = false;
								needHelpDialog.Show ();
								AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_SHOW_NEED_HELP_DIALOG);
						}
			
						int gridLineIndex = gridCells [selectedElementsPair.firstElement.index].gridLineIndex;
						currentLine = gridLines [gridLineIndex];

						//Draw the help path
						for (int i = 0; i <selectedPath.Count; i++) {
								OnNewGridCell (gridCells [selectedPath [i]], gridCells [selectedPath [i - 1 >= 0 ? i - 1 : i]], false);
						}
						OnCompletePath (currentLine, selectedElementsPair.lineColor);

				} catch (Exception ex) {
						//Catch the exception
				}
		}

		
		/// <summary>
		/// Enable the grid cell cover.
		/// </summary>
		/// <param name="gridCell">Grid cell.</param>
		/// <param name="color">Color.</param>
		private void EnableGridCellCover (GridCell gridCell, Color color)
		{
				if (gridCell == null) {
						return;
				}

				//Setting up the color of the cover of the grid cell
				color.a = gridCellCoverAlpha;
				gridCell.SetCoverColor (color);
		}

		/// <summary>
		/// Setting up alpha value for the next and back buttons.
		/// </summary>
		private void SettingUpNextBackAlpha ()
		{
				if (TableLevel.selectedLevel.ID == 1) {
						backButtonImage.GetComponent<Button> ().interactable = false;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				} else if (TableLevel.selectedLevel.ID == LevelsTable.levels.Count) {
						backButtonImage.GetComponent<Button> ().interactable = true;
						nextButtonImage.GetComponent<Button> ().interactable = false;
				} else {
						backButtonImage.GetComponent<Button> ().interactable = true;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				}
		}

		/// <summary>
		/// Resets the game contents.
		/// </summary>
		private void ResetGameContents ()
		{
				GameObject [] gridCells = GameObject.FindGameObjectsWithTag ("GridCell");
				GridCell gridCellComponent;
				foreach (GameObject gridCellObj in gridCells) {
						gridCellComponent = gridCellObj.GetComponent<GridCell> ();
						if (gridCellComponent.content != null)
								DestroyImmediate (gridCellComponent.content.gameObject);
						if (gridCellComponent.cover)
								DestroyImmediate (gridCellComponent.cover.gameObject);
						DestroyImmediate (gridCellObj);
				}

				GameObject [] gridLines = GameObject.FindGameObjectsWithTag ("GridLine");
				foreach (GameObject gridLine in gridLines) {
						Destroy (gridLine);
				}
		}
		
		/// <summary>
		/// Reset the help count.
		/// </summary>
		public void ResetHelpCount (bool showAd)
		{
				if (showAd) {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_RENEW_HELP_COUNT);
				}
				helpCount = 3;
				SetHelpCountText ();
		}
		
		/// <summary>
		/// Get the incompleted grid lines count.
		/// </summary>
		/// <returns>The completed grid lines count.</returns>
		private int GetInCompletedLinesCount ()
		{
				int count = 0;
				if (gridLines == null) {
						return count;
				}

				foreach (Line line in gridLines) {
						if (line.completedLine) {
								count++;
						}
				}
				return count;
		}

		/// <summary>
		/// Increase the movements counter.
		/// </summary>
		private void IncreaseMovements ()
		{
				movements++;
				SetMovementsText ();
		}

	
		/// <summary>
		/// Enable the suggested line.
		/// </summary>
		private void EnableSuggestedLine ()
		{
				if (suggestedLine != null) {
						Animator tempAnimator = suggestedLine.firstGridCell.content.GetComponent<Animator> ();
						if (tempAnimator != null) {
								tempAnimator.SetTrigger ("isRunning");
								tempAnimator = suggestedLine.secondGridCell.content.GetComponent<Animator> ();
								tempAnimator.SetTrigger ("isRunning");
						}
				}
		}

		/// <summary>
		/// Disable the suggested line.
		/// </summary>
		private void DisableSuggestedLine ()
		{
				if (suggestedLine != null) {
						Animator tempAnimator = suggestedLine.firstGridCell.content.GetComponent<Animator> ();
						if (tempAnimator != null) {
								tempAnimator.ResetTrigger ("isRunning");
								tempAnimator = suggestedLine.secondGridCell.content.GetComponent<Animator> ();
								tempAnimator.ResetTrigger ("isRunning");
						}
				}
		}

		/// <summary>
		/// Enable the cursor traget.
		/// </summary>
		/// <param name="postion">Postion.</param>
		public void EnableCursorTraget (Vector3 postion)
		{
				if (!enableCursorTarget) {
						return;
				}
				cursorTraget.enabled = true;
				cursorTraget.transform.position = postion;
		}
		
		/// <summary>
		/// Disable the cursor target.
		/// </summary>
		public void DisableCursorTarget ()
		{
				cursorTraget.enabled = false;
		}

		/// <summary>
		/// Set the best score for current level.
		/// </summary>
		public void SetCurrentLevelBestScore ()
		{
				if (currentLevelData.bestScore == Mathf.Infinity) {
						currentLevelData.bestScore = currentLevelScore;
						SetCurrentLevelBestScoreText (currentLevelScore);
				} else {
						if (currentLevelData.bestScore > currentLevelScore) {
								currentLevelData.bestScore = currentLevelScore;
								SetCurrentLevelBestScoreText (currentLevelScore);
						}
				}

		}

		/// <summary>
		/// Sets up new suggested line.
		/// </summary>
		/// <returns>The up new suggested line.</returns>
		private IEnumerator SetUpNewSuggestedLine ()
		{
				yield return new WaitForSeconds (1f);

				DisableSuggestedLine ();
				suggestedLine = null;

				if (gridLines != null) {
						foreach (Line line in gridLines) {
								if (!line.completedLine) {
										suggestedLine = line;
										break;
								}
						}
						EnableSuggestedLine ();
				}
		}

		/// <summary>
		/// Set the movements text .
		/// </summary>
		private void SetMovementsText ()
		{
				movementsText.text = movements.ToString ();
		}

		/// <summary>
		/// Set the flows text.
		/// </summary>
		private void SetFlowsText ()
		{
				if (gridLines == null) {
						return;
				}
				flowsText.text = GetInCompletedLinesCount ().ToString () + "/" + gridLines.Length;
		}

		/// <summary>
		/// Set the level best score text in seconds.
		/// </summary>
		private void SetCurrentLevelBestScoreText (float value)
		{
				if (value == Mathf.Infinity) {
						levelBestScoreText.text = "-";

				} else {
						levelBestScoreText.text = value.ToString ();
				}
		}

		/// <summary>
		/// Set the help count text.
		/// </summary>
		private void SetHelpCountText ()
		{
				helpCountText.text = helpCount.ToString ();
		}

		/// <summary>
		/// Get the help count.
		/// </summary>
		/// <returns>The help count.</returns>
		public static int GetHelpCount ()
		{
				return helpCount;
		}

		public enum ClickType
		{
				Began,
				Moved,
				Ended,
				None
		}
}