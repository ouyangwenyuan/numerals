using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved
 
public class GridUtil
{
		public static System.Random random = new System.Random ();
	
		public static void Shuffle<T> (List<T> listToShuffle, int numberOfTimesToShuffle = 3)
		{
				List<T> newList = new List<T> ();
				int index;
				for (int i = 0; i < numberOfTimesToShuffle; i++) {
						while (listToShuffle.Count > 0) {
								index = random.Next (listToShuffle.Count);
								newList.Add (listToShuffle [index]);
								listToShuffle.RemoveAt (index);
						}
			
						listToShuffle.AddRange (newList);
						newList.Clear ();
				}
		}

		public static void ShiftToGridCell (ClosePair cp, int index, LevelsManager attrib)
		{
				if (cp == null) {
						return;
				}
		
				if (cp.pair.helpPath == null) {
						return;
				}
		
				int i1 = cp.element.index / attrib.previousNumberOfCols;
				int j1 = cp.element.index - (i1 * attrib.previousNumberOfCols);
		
				int i2 = index / attrib.previousNumberOfCols;
				int j2 = index - (i2 * attrib.previousNumberOfCols);
		
				int iDiff = i1 - i2;
				int jDiff = j1 - j2;
		
				int currentj = j1;
				int currentIndex;
		
				bool addAtFront = false;
				if (cp.pair.helpPath [0] == cp.element.index) {
						addAtFront = true;
				}
		
				for (int i = 1; i <= Mathf.Abs(jDiff); i++) {
						currentj = (int)(j1 + (-1 * i * Mathf.Sign (jDiff)));
						currentIndex = i1 * attrib.previousNumberOfCols + currentj;
			
						if (addAtFront) {
								cp.pair.helpPath.Insert (0, currentIndex);
						} else {
								cp.pair.helpPath.Insert (cp.pair.helpPath.Count, currentIndex);
						}
				}
		
				int currenti = i1;
				for (int i = 1; i <= Mathf.Abs(iDiff); i++) {    
						currenti = (int)(i1 + (-1 * i * Mathf.Sign (iDiff)));
						currentIndex = currenti * attrib.previousNumberOfCols + currentj;
			
						if (addAtFront) {
								cp.pair.helpPath.Insert (0, currentIndex);
						} else {
								cp.pair.helpPath.Insert (cp.pair.helpPath.Count, currentIndex);
						}
				}
		
				cp.element.index = index;
		}
	
		public static ClosePair GetClosePairElement (int notUsedIndex, LevelsManager attrib, List<Level.ElementsPair> pairs)
		{
				ClosePair cp = new ClosePair ();
		
				Level.Element element;
				float minDistance = Mathf.Infinity, currentDistance;
				foreach (Level.ElementsPair pair in pairs) {
						//First Element
						element = pair.firstElement;
						currentDistance = CalculateDistanceBetween (notUsedIndex, element.index, attrib);
						if (currentDistance < minDistance) {
								minDistance = currentDistance;
								cp.pair = pair;
								cp.element = element;
						}
			
						//Second Element
						element = pair.secondElement;
						currentDistance = CalculateDistanceBetween (notUsedIndex, element.index, attrib);
						if (currentDistance < minDistance) {
								minDistance = currentDistance;
								cp.pair = pair;
								cp.element = element;
						}
				}
				return cp;
		}
	
		public static int CalculateDistanceBetween (int index1, int index2, LevelsManager attrib)
		{
				int i1 = index1 / attrib.previousNumberOfCols;
				int j1 = index1 - (i1 * attrib.previousNumberOfCols);
		
				int i2 = index2 / attrib.previousNumberOfCols;
				int j2 = index2 - (i2 * attrib.previousNumberOfCols);
		
				return (Mathf.Abs (i2 - i1) + Mathf.Abs (j2 - j1));
		}
	
		public static RandomPath GetRandomPath (Level level, LevelsManager attrib, int gridSize)
		{
				RandomPath randomPath = new RandomPath ();
		
				List<int> indexes = new List<int> ();
				for (int i = 0; i < gridSize; i++) {
						if (!GridCellUsed (level.elementsPairs, i)) {
								indexes.Add (i);
						}
				}
		
				Shuffle (indexes, 1);
		
				List<int> path;
				for (int i = 0; i < indexes.Count; i++) {
						for (int j = i+1; j < indexes.Count; j++) {
								path = GetGridPath (FindPath (level.elementsPairs, indexes [i], indexes [j], gridSize, attrib.numberOfRows, attrib.numberOfCols), indexes [i], indexes [j]);
								if (path.Count != 0) {
										randomPath.path = path;
										randomPath.firstIndex = indexes [i];
										randomPath.secondIndex = indexes [j];
										return randomPath;
								}
						}
				} 
				return randomPath;
		}
	
		public static bool GridCellUsed (List<Level.ElementsPair> pairs, int index)
		{
				foreach (Level.ElementsPair pair in pairs) {
						if (pair.helpPath != null)
								foreach (int helpIndex in pair.helpPath) {
										if (helpIndex == index) {
												return true;
										}
								}	
				}
				return false;
		}
	
		public static Hashtable FindPath (List<Level.ElementsPair> pairs, int fromIndex, int toIndex, int gridSize, int numberOfRows, int numberOfColumns)
		{
				Hashtable path = new Hashtable ();
		
				if (!(fromIndex >= 0 && fromIndex < gridSize) || !(toIndex >= 0 && toIndex < gridSize)) {
						return path;
				}
		
				int index = fromIndex;
				try {
						Queue<int> queue = new Queue<int> ();
						queue.Enqueue (index);
			
						int i;
						int j;
						while (queue.Count != 0) {
								index = queue.Dequeue ();
				
								if (index == toIndex) {
										break;
								} 
				
								i = index / numberOfColumns;
								j = index - (i * numberOfColumns);
				
								Enqueue (i, j + 1, numberOfRows, numberOfColumns, index, pairs, queue, path);
								Enqueue (i, j - 1, numberOfRows, numberOfColumns, index, pairs, queue, path);
				
								Enqueue (i - 1, j, numberOfRows, numberOfColumns, index, pairs, queue, path);
								Enqueue (i + 1, j, numberOfRows, numberOfColumns, index, pairs, queue, path);
						}
			
				} catch (System.Exception ex) {
						Debug.LogException (ex);
				}
		
				return path;
		}
	
		public static void Enqueue (int i, int j, int numberOfRows, int numberOfColumns, int index, List<Level.ElementsPair> pairs, Queue<int> queue, Hashtable path)
		{
				if ((i >= 0 && i < numberOfRows) && (j >= 0 && j < numberOfColumns)) {
						int currentIndex = (int)(i * numberOfColumns + j);
			
						if (!InHashPath (path, currentIndex) && !GridCellUsed (pairs, currentIndex)) {
								path.Add (currentIndex, index);
								queue.Enqueue (currentIndex);
						}
				}
		}
	
		public static bool InHashPath (Hashtable path, int index)
		{
				if (path.ContainsKey (index)) {
						return true;
				}
				return false;
		}
	
		public static List<int> GetGridPath (Hashtable hashPath, int fromIndex, int toIndex)
		{
				List<int> path = new List<int> ();
				if (hashPath.Count == 0) {
						return path;
				}
		
				int currentKey = toIndex;
				path.Add (currentKey);
				while (currentKey != fromIndex) {
						if (hashPath.ContainsKey (currentKey)) {
								currentKey = (int)hashPath [currentKey];
								path.Insert (0, currentKey);
						} else {
								path.Clear ();
								return path;
						}
				}
				return path;
		}
	
		public class RandomPath
		{
				public int firstIndex = -1;
				public int secondIndex = -1;
				public List<int> path = new List<int> ();
		}
	
		public class ClosePair
		{
				public Level.ElementsPair pair;
				public Level.Element element;
		}
}
