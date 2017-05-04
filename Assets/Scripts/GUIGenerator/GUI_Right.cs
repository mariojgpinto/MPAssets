using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class GUI_Right : MonoBehaviour {
	// RIGHT ---------------------------------------------
	public static GameObject				right_Panel_gameObject;
	public static UnityEngine.UI.Image		right_Panel;




	public static void UpdateValues(GameObject panel){
		right_Panel_gameObject = panel.gameObject;
		right_Panel = right_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();
	}
}
