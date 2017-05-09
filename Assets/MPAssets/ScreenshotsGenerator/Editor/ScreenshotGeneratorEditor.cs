using UnityEngine;
using UnityEditor;
using System.Collections;

using MPAssets;

[CustomEditor(typeof(ScreenshotGenerator))]
public class ScreenshotGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		ScreenshotGenerator myScript = (ScreenshotGenerator)target;
		if(GUILayout.Button("Take Photos"))
		{
//			EditorGUILayout.ObjectField(myScript.phones, typeof(PhoneInfo[]) ,true, new GUILayoutOption[]{});
			myScript.GenerateScreenShots();
		}
	}
}