using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

public class StarsRating
{
		/// <summary>
		/// The minumum Coefficient to calculate Stars Number
		/// </summary>
		private static float minCoefficient = 2f;

		/// <summary>
		/// The maximum Coefficient to calculate Stars Number
		/// </summary>
		private static float maxCoefficient = 3f;


		/// <summary>
		/// Gets the awesome dialog stars rating.
		/// </summary>
		/// <returns>The Win Dialog Stars Rating (Stars Number).</returns>
		/// <param name="time">Time.</param>
		/// <param name="movements">Movements.</param>
		/// <param name="gridSize">Grid size.</param>
		public static WinDialog.StarsNumber GetWinDialogStarsRating (int time, int movements, int gridSize)
		{
				float sum = time + movements;

				if (sum <= gridSize * minCoefficient) {
						return WinDialog.StarsNumber.THREE;
				} else if (sum > gridSize * minCoefficient && sum < gridSize * maxCoefficient) {
						return WinDialog.StarsNumber.TWO;
				}
				return WinDialog.StarsNumber.ONE;
		}

		/// <summary>
		/// Gets the level stars rating.
		/// </summary>
		/// <returns>The Level Stars Rating (Stars Number).</returns>
		/// <param name="time">Time.</param>
		/// <param name="movements">Movements.</param>
		/// <param name="gridSize">Grid size.</param>
		public static TableLevel.StarsNumber GetLevelStarsRating (int time, int movements, int gridSize)
		{
				float sum = time + movements;
		
				if (sum <= gridSize * minCoefficient) {
						return TableLevel.StarsNumber.THREE;
				} else if (sum > gridSize * minCoefficient && sum < gridSize * maxCoefficient) {
						return TableLevel.StarsNumber.TWO;
				}
				return TableLevel.StarsNumber.ONE;
		}
}