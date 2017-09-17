using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

/// <summary>
/// A level used with LevelsManager Component.
/// When you create a new level using Inspector ,you will create new instace of this class
/// </summary>
[System.Serializable]
public class Level
{
		/// <summary>
		/// The elements pairs list.
		/// </summary>
		public List<ElementsPair> elementsPairs = new List<ElementsPair> ();

		/// <summary>
		/// Whether to show the grid on create the level.
		/// </summary>
		public bool showGridOnCreate = false;

		/// </summary>
		[System.Serializable]
		public class ElementsPair
		{
				/// <summary>
				/// Whether the pair is visible(used with inspector only).
				/// </summary>
				public bool showPair;

				/// <summary>
				/// The sprite of the pair.
				/// </summary>
				public Sprite sprite;

				/// <summary>
				/// The connect sprite.
				/// </summary>
				public Sprite connectSprite;

				/// <summary>
				/// The color of the pair.
				/// </summary>
				public Color color = Color.red;

		    	/// <summary>
		   	 	/// The color of the line.
		    	/// </summary>
				public Color lineColor;

				/// <summary>
				/// The color of the number.
				/// </summary>
				public Color numberColor;

				/// <summary>
				/// The first element of the pair.
				/// </summary>
				public Element firstElement = new Element ();

				/// <summary>
				/// The second element of the pair.
				/// </summary>
				public Element secondElement = new Element ();

				/// <summary>
				/// The help path.
				/// </summary>
				public List<int> helpPath = new List<int>();
		}

		/// <summary>
		/// Element lass.
		/// </summary>
		[System.Serializable]
		public class Element
		{
				/// <summary>
				/// The index of the element in the Grid.
				/// </summary>
				public int index;
		}
}