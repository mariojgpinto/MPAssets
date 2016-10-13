using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GUIGenerator))]
public class GUIGeneratorEditor : Editor
{
	static GUIGeneratorEditor instance;
	static GUIGenerator myScript;

	static int selected = 0;
	static List<string> options = new List<string>();

	static GUIGeneratorEditor() {
		EditorApplication.hierarchyWindowChanged += HierarchyChanged;

		//instance = this;
	}

	void Start() {
		instance = this;
	}

	void OnEnable() {
	//	//Debug.Log("OnEnable");
		instance = this;
	//	EditorApplication.hierarchyWindowChanged += HierarchyChanged;
	}

	//void OnDisable() {
	//	//Debug.Log("OnDisable");
	//	EditorApplication.hierarchyWindowChanged -= HierarchyChanged;
	//}

	private static void HierarchyChanged() {
		//Debug.Log("HierarchyChanged! " + options.Count + " - " + selected);
				
		if (options.Count > selected) {
			string oldValue = options[selected];

			myScript = (GUIGenerator)instance.target;
			options = myScript.GetMainPanels();

			if (options[selected] != oldValue) {
				for (int i = 0; i < options.Count; ++i) {
					if (options[i] == oldValue) {
						selected = i;
						break;
					}
				}
			}
		}
		else {
			options = myScript.GetMainPanels();
		}
	}

	public override void OnInspectorGUI()
	{ 
		DrawDefaultInspector();
		
		myScript = (GUIGenerator)target;		

		GUILayout.BeginHorizontal();

		selected = EditorGUILayout.Popup("Main Screen", selected, options.ToArray());
		
		if (GUILayout.Button("Update Hierarchy", EditorStyles.miniButtonRight)) {
			HierarchyChanged();
		}
		GUILayout.EndHorizontal();
		
		GUILayout.Space(20);

		if (GUILayout.Button("Generate Files"))
		{
			List<string> files = myScript.ExistingFiles();

			if(files.Count > 0){
				string str = "Files in conflict:";
				for(int i = 0 ; i < files.Count ; ++i){
					str += "\n  - " + files[i];
				}
				str += "\n\nDo you wish to proceed?";

				if(EditorUtility.DisplayDialog("Files in Conflict", str, "Yes", "No")){
					Debug.Log("YES");

					myScript.activeScreen = selected;

					GenerateFiles();
				}
				else{
					Debug.Log("NO");
				}
			}
			else{
				GenerateFiles();
			}
		}
	}

	void GenerateFiles(){
		myScript.GenerateFiles();
		
		EditorUtility.DisplayDialog("Generation Successful", "Files Generated!", "Ok");
	}
}