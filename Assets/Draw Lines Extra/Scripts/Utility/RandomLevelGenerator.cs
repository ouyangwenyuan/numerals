using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

public class RandomLevelGenerator
{
		public static void Generate (Level level, LevelsManager attrib)
		{
				int gridSize = attrib.numberOfRows * attrib.numberOfCols;
				// int randomPairsNumber = Random.Range (Mathf.FloorToInt (gridSize / 3), Mathf.FloorToInt (gridSize / 2.0f + 1));
				int randomPairsNumber = Random.Range(attrib.numberOfRows-2,attrib.numberOfRows + 2);
				LevelsManagerUtil.RemoveLevelPairs (level, attrib);
				Level.ElementsPair pair = null;
				GridUtil.RandomPath randomPath;
				int count = 0;

				while (count < randomPairsNumber) {
						count++;
						randomPath = GridUtil.GetRandomPath (level, attrib, gridSize);
		
						if (randomPath.path.Count != 0) {
								pair = LevelsManagerUtil.CreateNewPair (attrib, level, true);
								pair.firstElement.index = randomPath.firstIndex;
								pair.secondElement.index = randomPath.secondIndex;
								pair.helpPath = randomPath.path;
						} else {
								break;
						}
				}

				bool completeGrid = false;
				bool timeOut = false;
				int tries = 0;

				while (!completeGrid) {
						tries++;

						if (tries > gridSize) {
								timeOut = true;
								break;
						}

						GridUtil.ClosePair cp;
						//Try to fill not used cells
						for (int i = 0; i <gridSize; i++) {
								if (!GridUtil.GridCellUsed (level.elementsPairs, i)) {
										cp = GridUtil.GetClosePairElement (i, attrib, level.elementsPairs);
										if (GridUtil.GetGridPath (GridUtil.FindPath (level.elementsPairs, cp.element.index, i, gridSize, attrib.previousNumberOfRows, attrib.previousNumberOfCols), cp.element.index, i).Count != 0) {
												GridUtil.ShiftToGridCell (cp, i, attrib);
										}
								}
						}

						completeGrid = true;
						for (int i = 0; i <gridSize; i++) {
								if (!GridUtil.GridCellUsed (level.elementsPairs, i)) {
										completeGrid = false;
										break;
								}
						}
				}

				if (timeOut) {
						Generate (level, attrib);
				} 
		}
}