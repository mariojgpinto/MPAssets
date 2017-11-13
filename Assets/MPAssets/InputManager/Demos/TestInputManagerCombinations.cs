using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MPAssets;

public class TestInputManagerCombinations : MonoBehaviour {
	#region VARIABLES
	
	#endregion

	public void TestCombination_Editor() {
		Debug.Log("Your action on TestCombination_Editor");
	}

	public void TestCombination_Script() {
		Debug.Log("Your action on TestCombination_Script");
	}


	#region UNITY_CALLBACKS
	// Use this for initialization
	void Start () {
		InputManager.AddKeyCombination("Combo from script (Ctrl+F2)", new List<KeyCode>() { KeyCode.LeftControl, KeyCode.F2 }, () => TestCombination_Script());
	}
	#endregion
}
