using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class GUI_Left : MonoBehaviour {
	// LEFT ---------------------------------------------
	public static GameObject				left_Panel_gameObject;
	public static UnityEngine.UI.Image		left_Panel;




	public static void UpdateValues(GameObject panel){
		left_Panel_gameObject = panel.gameObject;
		left_Panel = left_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();
	}
}
