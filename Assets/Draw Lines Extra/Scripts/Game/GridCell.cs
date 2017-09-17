using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[DisallowMultipleComponent]
public class GridCell : MonoBehaviour
{      
		/// <summary>
		/// The content of the grid cell.
		/// </summary>
		[HideInInspector]
		public Transform content;

		/// <summary>
		/// The top cover of the grid cell.
		/// </summary>
		[HideInInspector]
		public Image cover;

		/// <summary>
		/// Whether the GridCell is used.
		/// </summary>
		public bool currentlyUsed;

		/// <summary>
		/// Whether the GridCell is empty.
		/// </summary>
		public bool isEmpty = true;

		/// <summary>
		/// The index of the GridCell in the Grid.
		/// </summary>
		public int index;

		/// <summary>
		/// The index of the traget(partner).
		/// </summary>
		public int tragetIndex = -1;

		/// <summary>
		/// The index of the grid line.
		/// </summary>
		public int gridLineIndex = -1;

		/// <summary>
		/// The index of the element(dots) pair.
		/// </summary>
		public int elementPairIndex = -1;

		/// <summary>
		/// The surrounded adjacents of the GridCell.
		/// (Contains the indexes of the adjacents (neighbours))
		/// </summary>
		public List<int> adjacents = new List<int> ();

		/// <summary>
		/// Define the adjacents of the GridCell.
		/// </summary>
		/// <param name="i">The index of the Row such that 0=< i < NumberOfRows </para>.</param>
		/// <param name="j">The index of the Column such that 0=< j < NumberOfColumns .</param>
		/// <param name="allowedMovements">Number of allowed movements .</param>
		public void DefineAdjacents (int i, int j,LevelsManager.Movements allowedMovements)
		{
				if (adjacents == null) {
						adjacents = new List<int> ();
				}
				
				if (allowedMovements == LevelsManager.Movements.FOUR_MOVEMENTS || allowedMovements == LevelsManager.Movements.EIGHT_MOVEMENTS) {
						AddAdjacent (new Vector2 (i, j + 1));//Right Adjacent
						AddAdjacent (new Vector2 (i, j - 1));//Left Adjacent
						AddAdjacent (new Vector2 (i - 1, j));//Upper Adjacent
						AddAdjacent (new Vector2 (i + 1, j));//Lower Adjacent
				}

				if (allowedMovements == LevelsManager.Movements.EIGHT_MOVEMENTS) {
						AddAdjacent (new Vector2 (i - 1, j + 1));//Right Upper Adjacent
						AddAdjacent (new Vector2 (i - 1, j - 1));//Left Upper Adjacent
						AddAdjacent (new Vector2 (i + 1, j + 1));//Right Lower Adjacent
						AddAdjacent (new Vector2 (i + 1, j - 1));//Left Lower Adjacent
				}
		}

		/// <summary>
		/// Adds the adjacent index (GridCell index) to the Adjacents List.
		/// </summary>
		/// <param name="adjacent">Adjacent vector (i,j).</param>
		private void AddAdjacent (Vector2 adjacent)
		{
				if ((adjacent.x >= 0 && adjacent.x < GameManager.numberOfRows) && (adjacent.y >= 0 && adjacent.y < GameManager.numberOfColumns)) {
						adjacents.Add ((int)(adjacent.x * GameManager.numberOfColumns + adjacent.y));//Convert from (i,j) to Array index
				}
		}

		/// <summary>
		/// Check if the given adjacent index is one of the Adjacents or Not.
		/// </summary>
		/// <param name="adjacentIndex">an Adjacent index.</param>
		public bool OneOfAdjacents (int adjacentIndex)
		{
				if (adjacents == null) {
						return false;
				}

				if (adjacents.Contains (adjacentIndex)) {
						return true;
				}
				return false;
		}

		/// <summary>
		/// Reset Attributes
		/// </summary>
		public void Reset ()
		{
				currentlyUsed = false;
				if (isEmpty) {
						tragetIndex = -1;
						gridLineIndex = -1;
				}

				if (cover != null) {
						cover.color = Color.white;
						cover.enabled = false;
				} else {
					Debug.LogWarning("Undefined Cover");
				}
		}

		/// <summary>
		/// Set the color of the cover.
		/// </summary>
		/// <param name="color">Color.</param>
		public void SetCoverColor(Color color){
			if (cover != null) {
				cover.color = color;
				cover.enabled = true;
			}
		}
}