using UnityEngine;
using System;
using System.IO;
using System.Collections;

using MPAssets;

public class GUI_OtherMenuHomeMenu2 : MonoBehaviour {
	// HOMEMENU2 ---------------------------------------------
	public static GameObject				homeMenu2_Panel_gameObject;
	public static UnityEngine.UI.Image		homeMenu2_Panel;


	// IMAGESLIDESHOW ---------------------------------------------
	public static GameObject				imageSlideShow_Panel_gameObject;
	public static UnityEngine.UI.Image		imageSlideShow_Panel;

	public static GameObject				imageSlideShow_photo_Image_gameObject;
	public static UnityEngine.UI.Image		imageSlideShow_photo_Image;

	public static GameObject				imageSlideShow_buttons_Panel_gameObject;
	public static UnityEngine.UI.Image		imageSlideShow_buttons_Panel;

	public static GameObject				imageSlideShow_buttons_next_Button_gameObject;
	public static UnityEngine.UI.Button		imageSlideShow_buttons_next_Button;

	public static GameObject				imageSlideShow_buttons_next_text_Other_22_gameObject;

	public static GameObject				imageSlideShow_buttons_previous_Button_gameObject;
	public static UnityEngine.UI.Button		imageSlideShow_buttons_previous_Button;

	public static GameObject				imageSlideShow_buttons_previous_text_Other_23_gameObject;

	public static GameObject				imageSlideShow_buttons_back_Button_gameObject;
	public static UnityEngine.UI.Button		imageSlideShow_buttons_back_Button;

	public static GameObject				imageSlideShow_buttons_back_text_Other_24_gameObject;

	public static GameObject				imageSlideShow_myToggle_Toggle_gameObject;
	public static UnityEngine.UI.Toggle		imageSlideShow_myToggle_Toggle;

	public static GameObject				imageSlideShow_myToggle_background_Image_gameObject;
	public static UnityEngine.UI.Image		imageSlideShow_myToggle_background_Image;

	public static GameObject				imageSlideShow_myToggle_background_checkmark_Image_gameObject;
	public static UnityEngine.UI.Image		imageSlideShow_myToggle_background_checkmark_Image;

	public static GameObject				imageSlideShow_myToggle_label_Text_gameObject;
	public static UnityEngine.UI.Text		imageSlideShow_myToggle_label_Text;

	// INPUT ---------------------------------------------
	public static GameObject				input_Panel_gameObject;
	public static UnityEngine.UI.Image		input_Panel;

	public static GameObject				input_name_Text_gameObject;
	public static UnityEngine.UI.Text		input_name_Text;

	public static GameObject				input_name_InputField_gameObject;
	public static UnityEngine.UI.InputField	input_name_InputField;

	public static GameObject				input_name_placeholder_Other_25_gameObject;

	public static GameObject				input_name_text_Other_26_gameObject;

	public static GameObject				input_email_Text_gameObject;
	public static UnityEngine.UI.Text		input_email_Text;

	public static GameObject				input_email_InputField_gameObject;
	public static UnityEngine.UI.InputField	input_email_InputField;

	public static GameObject				input_email_placeholder_Other_27_gameObject;

	public static GameObject				input_email_text_Other_28_gameObject;

	// LOGO ---------------------------------------------
	public static GameObject				logo_Image_gameObject;
	public static UnityEngine.UI.Image		logo_Image;



	public static void UpdateValues(GameObject panel){
		homeMenu2_Panel_gameObject = panel.gameObject;
		homeMenu2_Panel = homeMenu2_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		imageSlideShow_Panel_gameObject = homeMenu2_Panel_gameObject.transform.Find("Panel_ImageSlideShow").gameObject;
		imageSlideShow_Panel = imageSlideShow_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		imageSlideShow_photo_Image_gameObject = imageSlideShow_Panel_gameObject.transform.Find("Image_Photo").gameObject;
		imageSlideShow_photo_Image = imageSlideShow_photo_Image_gameObject.GetComponent<UnityEngine.UI.Image>();

		imageSlideShow_buttons_Panel_gameObject = imageSlideShow_Panel_gameObject.transform.Find("Panel_Buttons").gameObject;
		imageSlideShow_buttons_Panel = imageSlideShow_buttons_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		imageSlideShow_buttons_next_Button_gameObject = imageSlideShow_buttons_Panel_gameObject.transform.Find("Button_Next").gameObject;
		imageSlideShow_buttons_next_Button = imageSlideShow_buttons_next_Button_gameObject.GetComponent<UnityEngine.UI.Button>();

		imageSlideShow_buttons_next_text_Other_22_gameObject = imageSlideShow_buttons_next_Button_gameObject.transform.Find("Text").gameObject;

		imageSlideShow_buttons_previous_Button_gameObject = imageSlideShow_buttons_Panel_gameObject.transform.Find("Button_Previous").gameObject;
		imageSlideShow_buttons_previous_Button = imageSlideShow_buttons_previous_Button_gameObject.GetComponent<UnityEngine.UI.Button>();

		imageSlideShow_buttons_previous_text_Other_23_gameObject = imageSlideShow_buttons_previous_Button_gameObject.transform.Find("Text").gameObject;

		imageSlideShow_buttons_back_Button_gameObject = imageSlideShow_buttons_Panel_gameObject.transform.Find("Button_Back").gameObject;
		imageSlideShow_buttons_back_Button = imageSlideShow_buttons_back_Button_gameObject.GetComponent<UnityEngine.UI.Button>();

		imageSlideShow_buttons_back_text_Other_24_gameObject = imageSlideShow_buttons_back_Button_gameObject.transform.Find("Text").gameObject;

		imageSlideShow_myToggle_Toggle_gameObject = imageSlideShow_Panel_gameObject.transform.Find("Toggle_MyToggle").gameObject;
		imageSlideShow_myToggle_Toggle = imageSlideShow_myToggle_Toggle_gameObject.GetComponent<UnityEngine.UI.Toggle>();

		imageSlideShow_myToggle_background_Image_gameObject = imageSlideShow_myToggle_Toggle_gameObject.transform.Find("Image_Background").gameObject;
		imageSlideShow_myToggle_background_Image = imageSlideShow_myToggle_background_Image_gameObject.GetComponent<UnityEngine.UI.Image>();

		imageSlideShow_myToggle_background_checkmark_Image_gameObject = imageSlideShow_myToggle_background_Image_gameObject.transform.Find("Image_Checkmark").gameObject;
		imageSlideShow_myToggle_background_checkmark_Image = imageSlideShow_myToggle_background_checkmark_Image_gameObject.GetComponent<UnityEngine.UI.Image>();

		imageSlideShow_myToggle_label_Text_gameObject = imageSlideShow_myToggle_Toggle_gameObject.transform.Find("Text_Label").gameObject;
		imageSlideShow_myToggle_label_Text = imageSlideShow_myToggle_label_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		input_Panel_gameObject = homeMenu2_Panel_gameObject.transform.Find("Panel_Input").gameObject;
		input_Panel = input_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		input_name_Text_gameObject = input_Panel_gameObject.transform.Find("Text_Name").gameObject;
		input_name_Text = input_name_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		input_name_InputField_gameObject = input_Panel_gameObject.transform.Find("InputField_Name").gameObject;
		input_name_InputField = input_name_InputField_gameObject.GetComponent<UnityEngine.UI.InputField>();

		input_name_placeholder_Other_25_gameObject = input_name_InputField_gameObject.transform.Find("Placeholder").gameObject;

		input_name_text_Other_26_gameObject = input_name_InputField_gameObject.transform.Find("Text").gameObject;

		input_email_Text_gameObject = input_Panel_gameObject.transform.Find("Text_Email").gameObject;
		input_email_Text = input_email_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		input_email_InputField_gameObject = input_Panel_gameObject.transform.Find("InputField_Email").gameObject;
		input_email_InputField = input_email_InputField_gameObject.GetComponent<UnityEngine.UI.InputField>();

		input_email_placeholder_Other_27_gameObject = input_email_InputField_gameObject.transform.Find("Placeholder").gameObject;

		input_email_text_Other_28_gameObject = input_email_InputField_gameObject.transform.Find("Text").gameObject;

		logo_Image_gameObject = homeMenu2_Panel_gameObject.transform.Find("Image_Logo").gameObject;
		logo_Image = logo_Image_gameObject.GetComponent<UnityEngine.UI.Image>();
	}
}
