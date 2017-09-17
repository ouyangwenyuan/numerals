using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

public class ProgressUtil
{
		public static bool DisplayCancableProgressBar(float progress,string title,string info){
			#if UNITY_EDITOR
				return  EditorUtility.DisplayCancelableProgressBar (title, info, progress);
			#else
				return false;
			#endif
		}

		public static void DisplayProgressBar (float progress, string title,string info)
		{
			#if UNITY_EDITOR
				EditorUtility.DisplayProgressBar (title, info, progress);
			#endif

		}

		public static void HideProgressBar(){
			#if UNITY_EDITOR
				EditorUtility.ClearProgressBar ();
			#endif
		}
}
