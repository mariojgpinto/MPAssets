using UnityEngine;
using UnityEditor;

public class MPAssets_editor {
	#region PREFAB
	static void CreateObject_UI(string name, GameObject parent = null) {
		Object obj = AssetDatabase.LoadAssetAtPath<Object>("Assets/MPAssets/GUIGenerator/Prefabs/" + name + ".prefab");
		GameObject prefab = PrefabUtility.InstantiatePrefab(obj) as GameObject;
		if (parent != null) {
			prefab.transform.SetParent(parent.transform);
			prefab.transform.localPosition = Vector3.zero;
			prefab.transform.localScale = Vector3.zero;
			prefab.transform.localRotation = Quaternion.Euler(Vector3.zero);
		}
		prefab.name = prefab.name + "_";
	}

	static void CreateObject_Log(GameObject parent = null) {
		Object obj = AssetDatabase.LoadAssetAtPath<Object>("Assets/MPAssets/LogFile/Prefabs/Log.prefab");
		GameObject prefab = PrefabUtility.InstantiatePrefab(obj) as GameObject;
		prefab.transform.SetAsLastSibling();
	}

	static void CreateObject_InputManager(GameObject parent = null) {
		Object obj = AssetDatabase.LoadAssetAtPath<Object>("Assets/MPAssets/InputManager/Prefabs/InputManager.prefab");
		GameObject prefab = PrefabUtility.InstantiatePrefab(obj) as GameObject;
		prefab.transform.SetAsLastSibling();
	}

	static void CreateObject_UtilsScript(string name, GameObject parent = null) {
		if (parent != null) {
			switch (name.ToUpper()) {
				case "FPS":
					parent.AddComponent<MPAssets.FPS>();
					break;
				case "LOOK_FPS":
					if (parent.GetComponent<Camera>() != null)
						parent.AddComponent<MPAssets.MouseLookFPS>();
					else
						Debug.Log("Components must be a camera to attach this script");
					break;
				case "TREMBLE":
					if (parent.GetComponent<Camera>() != null)
						parent.AddComponent<MPAssets.CameraTremble>();
					else
						Debug.Log("Components must be a camera to attach this script");
					break;
				default: break;
			}
		}
	}
	#endregion

	#region UTILS
	[MenuItem("GameObject/MPAssets/Utils/FPS Counter")]
	private static void CreateFPS() {
		CreateObject_UtilsScript("FPS", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/Utils/Camera Mouse FPS")]
	private static void CreateCamera_FPS() {
		CreateObject_UtilsScript("LOOK_FPS", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/Utils/Camera Tremble")]
	private static void CreateCamera_Tremble() {
		CreateObject_UtilsScript("TREMBLE", Selection.activeGameObject);
	}
	#endregion

	#region LOG
	//[MenuItem("GameObject/MPAssets/Log/Log")]
	[MenuItem("GameObject/MPAssets/Log")]
	private static void CreateLog() {
		CreateObject_Log(Selection.activeGameObject);
	}
	#endregion

	#region INPUT_MANAGER
	//[MenuItem("GameObject/MPAssets/InputManager/InputManager")]
	[MenuItem("GameObject/MPAssets/InputManager")]
	private static void CreateInputManager() {
		CreateObject_InputManager(Selection.activeGameObject);
	}
	#endregion

	#region UI
	[MenuItem("GameObject/MPAssets/UI/Panel", priority = 0)]
	private static void CreatePanel() {
		CreateObject_UI("Panel", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/UI/Text")]
	private static void CreateText() {
		CreateObject_UI("Button", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/UI/Image")]
	private static void CreateImage() {
		CreateObject_UI("Button", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/UI/Button")]
	private static void CreateButton() {
		CreateObject_UI("Button", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/UI/InputField")]
	private static void CreateInputField() {
		CreateObject_UI("InputField", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/UI/RawImage")]
	private static void CreateRawImage() {
		CreateObject_UI("Button", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/UI/Toggle")]
	private static void CreateToggle() {
		CreateObject_UI("Button", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/UI/Slider")]
	private static void CreateSlider() {
		CreateObject_UI("Button", Selection.activeGameObject);
	}
	//[MenuItem("GameObject/MPAssets/UI/ScrollBar")]
	//private static void CreateScrollBar() {
	//	CreateObject("ScrollBar", Selection.activeGameObject);
	//}
	[MenuItem("GameObject/MPAssets/UI/ScrollView")]
	private static void CreateScrollView() {
		CreateObject_UI("ScrollView", Selection.activeGameObject);
	}
	[MenuItem("GameObject/MPAssets/UI/Dropdown")]
	private static void CreateDropdown() {
		CreateObject_UI("Dropdown", Selection.activeGameObject);
	}
	#endregion
}
