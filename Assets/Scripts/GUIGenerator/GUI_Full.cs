using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class GUI_Full : MonoBehaviour {
	// FULL ---------------------------------------------
	public static GameObject				full_Panel_gameObject;
	public static UnityEngine.UI.Image		full_Panel;


	// TOGGLERIGHT ---------------------------------------------
	public static GameObject				toggleRight_Button_gameObject;
	public static UnityEngine.UI.Button		toggleRight_Button;

	public static GameObject				toggleRight_label_Text_gameObject;
	public static UnityEngine.UI.Text		toggleRight_label_Text;

	// TOGGLELEFT ---------------------------------------------
	public static GameObject				toggleLeft_Button_gameObject;
	public static UnityEngine.UI.Button		toggleLeft_Button;

	public static GameObject				toggleLeft_label_Text_gameObject;
	public static UnityEngine.UI.Text		toggleLeft_label_Text;

	// TOGGLELEFT2 ---------------------------------------------
	public static GameObject				toggleLeft2_Button_gameObject;
	public static UnityEngine.UI.Button		toggleLeft2_Button;

	public static GameObject				toggleLeft2_label_Text_gameObject;
	public static UnityEngine.UI.Text		toggleLeft2_label_Text;

	// LOG ---------------------------------------------
	public static GameObject				log_Text_gameObject;
	public static UnityEngine.UI.Text		log_Text;

	// MYIMAGE ---------------------------------------------
	public static GameObject				myImage_RawImage_gameObject;
	public static UnityEngine.UI.RawImage	myImage_RawImage;

	// MYSCROLL ---------------------------------------------
	public static GameObject				myScroll_Scroll_gameObject;
	public static UnityEngine.UI.ScrollRect	myScroll_Scroll;



	public static void UpdateValues(GameObject panel){
		full_Panel_gameObject = panel.gameObject;
		full_Panel = full_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		toggleRight_Button_gameObject = full_Panel_gameObject.transform.Find("Button_ToggleRight").gameObject;
		toggleRight_Button = toggleRight_Button_gameObject.GetComponent<UnityEngine.UI.Button>();

		toggleRight_label_Text_gameObject = toggleRight_Button_gameObject.transform.Find("Text_Label").gameObject;
		toggleRight_label_Text = toggleRight_label_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		toggleLeft_Button_gameObject = full_Panel_gameObject.transform.Find("Button_ToggleLeft").gameObject;
		toggleLeft_Button = toggleLeft_Button_gameObject.GetComponent<UnityEngine.UI.Button>();

		toggleLeft_label_Text_gameObject = toggleLeft_Button_gameObject.transform.Find("Text_Label").gameObject;
		toggleLeft_label_Text = toggleLeft_label_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		toggleLeft2_Button_gameObject = full_Panel_gameObject.transform.Find("Button_ToggleLeft2").gameObject;
		toggleLeft2_Button = toggleLeft2_Button_gameObject.GetComponent<UnityEngine.UI.Button>();

		toggleLeft2_label_Text_gameObject = toggleLeft2_Button_gameObject.transform.Find("Text_Label").gameObject;
		toggleLeft2_label_Text = toggleLeft2_label_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		log_Text_gameObject = full_Panel_gameObject.transform.Find("Text_log").gameObject;
		log_Text = log_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		myImage_RawImage_gameObject = full_Panel_gameObject.transform.Find("RawImage_MyImage").gameObject;
		myImage_RawImage = myImage_RawImage_gameObject.GetComponent<UnityEngine.UI.RawImage>();

		myScroll_Scroll_gameObject = full_Panel_gameObject.transform.Find("Scroll_MyScroll").gameObject;
		myScroll_Scroll = myScroll_Scroll_gameObject.GetComponent<UnityEngine.UI.ScrollRect>();
	}
}
