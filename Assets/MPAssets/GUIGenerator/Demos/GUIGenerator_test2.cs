//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class GUIGenerator_test2 : MonoBehaviour {
//	bool right = false;
//	bool left = false;
//	bool left2 = false;

//	public static float leftX;

//	public Text txt;


//	void OnButton_Full(object sender, ButtonPressedEventArgs e) {
//		switch (e.id) {
//			case "toggleRight_Button": //FULL - TOGGLERIGHT
//				Debug.Log(e.id + " - FULL - BUTTON TOGGLERIGHT");

//				if (right) {
//					GUI_Animation.RemoveToRight(GUI_Controller.instance.gui_Right,1);
//				}
//				else{
//					GUI_Animation.BringBackFromRight(GUI_Controller.instance.gui_Right, 1);
//				}

//				right = !right;

//				break;
//			case "toggleLeft_Button": //FULL - TOGGLELEFT
//				Debug.Log(e.id + " - FULL - BUTTON TOGGLELEFT");

//				if (left) {
//					GUI_Animation.RemoveToTop(GUI_Controller.instance.gui_Left, 1);
//				}
//				else {
//					GUI_Animation.BringBackFromTop(GUI_Controller.instance.gui_Left, 1);
//				}

//				left = !left;

//				break;
//			case "toggleLeft2_Button": //FULL - TOGGLELEFT2
//				Debug.Log(e.id + " - FULL - BUTTON TOGGLELEFT2");

//				if (left2) {
//					GUI_Animation.HideMenu(GUI_Controller.instance.gui_Left);
//				}
//				else {
//					GUI_Animation.ShowMenu(GUI_Controller.instance.gui_Left);
//				}

//				left2 = !left2;

//				break;
//			default: break;
//		}
//	}

//	void AssignEvents() {
//		GUI_Controller.GUI_Full_ButtonPressed += OnButton_Full;

//	}

//	// Use this for initialization
//	void Start () {
//		Debug.Log("Application.dataPath: " + Application.dataPath);
//		AssignEvents();
//	}
	
//	// Update is called once per frame
//	void Update () {
//		string str = "";
//		str += ("L" + GUI_Controller.instance.gui_Left.GetComponent<RectTransform>().anchoredPosition);
//		str += ("\nL" + GUI_Controller.instance.gui_Left.GetComponent<RectTransform>().anchoredPosition3D);
//		str += ("\nL" + GUI_Controller.instance.gui_Left.GetComponent<RectTransform>().offsetMin);
//		str += ("\nL" + GUI_Controller.instance.gui_Left.GetComponent<RectTransform>().offsetMax);

//		txt.text = str;
//	}
//}
