using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using MPAssets;

[CustomEditor(typeof(GUIGenerator))]
public class GUIGeneratorEditor : Editor
{
	static GUIGeneratorEditor instance;
	static GUIGenerator myScript;

	static int selected = 0;
	static bool isPersistent = true;
	static List<string> options = new List<string>();

	public GameObject myCanvas;
	//static int selectedFocus = 0;
	static List<string> optionsFocus = new List<string>();
	static GUIGeneratorEditor() {
		EditorApplication.hierarchyWindowChanged += HierarchyChanged;

		//instance = this;
	}

	private static void GetFocusHierarchy() {
		optionsFocus.Clear();
		for (int i = 0; i < instance.myCanvas.transform.childCount; ++i) {
			optionsFocus.Add(instance.myCanvas.transform.GetChild(i).gameObject.name);
		}
	}
	void Start() {
		instance = this;
	}

	void OnEnable() {
	//	//Debug.Log("OnEnable");
		instance = this;
		myCanvas = GameObject.Find("Canvas");
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

		isPersistent = EditorGUILayout.Toggle("Persitent UI", isPersistent);

		EditorGUILayout.LabelField("(Change only if MPAssets folder path is changed.)");
		myScript.directory_DataPath = EditorGUILayout.TextField("Data Path", myScript.directory_DataPath);

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

					GenerateFiles(isPersistent);
				}
				else{
					Debug.Log("NO");
				}
			}
			else{
				GenerateFiles();
			}
		}

		if (GUILayout.Button("Generate Animation File")) {
			if (myScript.AnimationFileExists()) {
				string str = "Animation file already exists.";

				str += "\n\nDo you wish to proceed?";

				if (EditorUtility.DisplayDialog("Files in Conflict", str, "Yes", "No")) {
					Debug.Log("YES");

					myScript.activeScreen = selected;

					GenerateAnimationFile();
				}
				else {
					Debug.Log("NO");
				}
			}
			else {
				GenerateAnimationFile();
			}
		}

		//GUILayout.Space(20);

		//GUILayout.BeginHorizontal();

		//selectedFocus = EditorGUILayout.Popup("Focus Screen", selectedFocus, optionsFocus.ToArray());

		//if (GUILayout.Button("Update Focus Hierarchy", EditorStyles.miniButtonRight)) {
		//	GetFocusHierarchy();
		//}
		//GUILayout.EndHorizontal();

		//if (GUILayout.Button("Focus Camera")) {
		//	string oldValue = optionsFocus[selectedFocus];

		//	myCanvas.transform.position = myCanvas.transform.FindChild(oldValue).localPosition;
		//	// 
		//}
		EditorUtility.SetDirty(myScript);
	}

	void GenerateFiles(bool isPersitent = true){
		myScript.GenerateFiles(isPersitent);
		
		EditorUtility.DisplayDialog("Generation Successful", "Files Generated!", "Ok");
	}

	void GenerateAnimationFile() {
		myScript.GenerateAnimationFile();

		EditorUtility.DisplayDialog("Generation Successful", "Animation File Generated!", "Ok");
	}
}