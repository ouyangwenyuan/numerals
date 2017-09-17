using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DirtyUtil
{
		public static void MarkSceneDirty ()
		{
			#if UNITY_5 && UNITY_EDITOR
				if(!EditorApplication.isSceneDirty){
					EditorApplication.MarkSceneDirty(); 
				}
			#endif
		}
}