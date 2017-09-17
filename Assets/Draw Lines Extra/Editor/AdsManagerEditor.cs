using UnityEngine;
using System.Collections;
using UnityEditor;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved
 
[CustomEditor (typeof(AdsManager))]
public class AdsManagerEditor :  Editor
{
		public override void OnInspectorGUI ()
		{
				if (Application.isPlaying) {
					return;
				}
				AdsManager attrib = (AdsManager)target;//get the target
				EditorGUILayout.Separator ();
				EditorGUILayout.HelpBox ("Manage the Advertismens.", MessageType.Info);
				EditorGUILayout.Separator ();
				EditorGUILayout.BeginHorizontal ();

				GUILayout.Label ("Event");
				GUILayout.Space (40);
				GUILayout.Label ("Ad Type");
				GUILayout.Space (20);
				GUILayout.Label ("Ad Position");
				GUILayout.Label ("Active");

				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.Separator ();
				foreach (AdsManager.AdEvent adEvent in attrib.adEvents) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.EnumPopup (adEvent.evt);
						adEvent.type = (AdsManager.AdEvent.Type)EditorGUILayout.EnumPopup (adEvent.type);
						EditorGUI.BeginDisabledGroup(adEvent.type != AdsManager.AdEvent.Type.BANNER);
						adEvent.adPostion = (GoogleMobileAds.Api.AdPosition)EditorGUILayout.EnumPopup (adEvent.adPostion);
						EditorGUI.EndDisabledGroup();
				
						adEvent.isEnabled = EditorGUILayout.Toggle (adEvent.isEnabled);
						EditorGUILayout.EndHorizontal ();
				}
				EditorGUILayout.Separator ();

				if (GUI.changed) {
					DirtyUtil.MarkSceneDirty ();
				}
		}
}