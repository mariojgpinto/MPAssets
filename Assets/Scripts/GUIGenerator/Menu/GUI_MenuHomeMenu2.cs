using UnityEngine;
using System;
using System.IO;
using System.Collections;

using MPAssets;

public class GUI_MenuHomeMenu2 : MonoBehaviour {
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

	public static GameObject				imageSlideShow_buttons_next_text_Other_36_gameObject;

	public static GameObject				imageSlideShow_buttons_previous_Button_gameObject;
	public static UnityEngine.UI.Button		imageSlideShow_buttons_previous_Button;

	public static GameObject				imageSlideShow_buttons_previous_text_Other_37_gameObject;

	public static GameObject				imageSlideShow_buttons_back_Button_gameObject;
	public static UnityEngine.UI.Button		imageSlideShow_buttons_back_Button;

	public static GameObject				imageSlideShow_buttons_back_text_Other_38_gameObject;

	// INPUT ---------------------------------------------
	public static GameObject				input_Panel_gameObject;
	public static UnityEngine.UI.Image		input_Panel;

	public static GameObject				input_name_Text_gameObject;
	public static UnityEngine.UI.Text		input_name_Text;

	public static GameObject				input_name_InputField_gameObject;
	public static UnityEngine.UI.InputField	input_name_InputField;

	public static GameObject				input_name_placeholder_Other_39_gameObject;

	public static GameObject				input_name_text_Other_40_gameObject;

	public static GameObject				input_email_Text_gameObject;
	public static UnityEngine.UI.Text		input_email_Text;

	public static GameObject				input_email_InputField_gameObject;
	public static UnityEngine.UI.InputField	input_email_InputField;

	public static GameObject				input_email_placeholder_Other_41_gameObject;

	public static GameObject				input_email_text_Other_42_gameObject;

	// LOGO ---------------------------------------------
	public static GameObject				logo_Image_gameObject;
	public static UnityEngine.UI.Image		logo_Image;

	// MYDROPDOWN ---------------------------------------------
	public static GameObject				myDropdown_Dropdown_gameObject;
	public static UnityEngine.UI.Dropdown	myDropdown_Dropdown;

	public static GameObject				myDropdown_label_Text_gameObject;
	public static UnityEngine.UI.Text		myDropdown_label_Text;

	public static GameObject				myDropdown_arrow_Image_gameObject;
	public static UnityEngine.UI.Image		myDropdown_arrow_Image;

	public static GameObject				myDropdown_template_Panel_gameObject;
	public static UnityEngine.UI.Image		myDropdown_template_Panel;

	public static GameObject				myDropdown_template_viewport_Panel_gameObject;
	public static UnityEngine.UI.Image		myDropdown_template_viewport_Panel;

	public static GameObject				myDropdown_template_viewport_content_Panel_gameObject;
	public static UnityEngine.UI.Image		myDropdown_template_viewport_content_Panel;

	public static GameObject				myDropdown_template_viewport_content_item_Panel_gameObject;
	public static UnityEngine.UI.Image		myDropdown_template_viewport_content_item_Panel;

	public static GameObject				myDropdown_template_viewport_content_item_background_Panel_gameObject;
	public static UnityEngine.UI.Image		myDropdown_template_viewport_content_item_background_Panel;

	public static GameObject				myDropdown_template_viewport_content_item_checkmark_Image_gameObject;
	public static UnityEngine.UI.Image		myDropdown_template_viewport_content_item_checkmark_Image;

	public static GameObject				myDropdown_template_viewport_content_item_label_Text_gameObject;
	public static UnityEngine.UI.Text		myDropdown_template_viewport_content_item_label_Text;

	public static GameObject				myDropdown_template_scrollbar_Panel_gameObject;
	public static UnityEngine.UI.Image		myDropdown_template_scrollbar_Panel;

	public static GameObject				myDropdown_template_scrollbar_slidingArea_Panel_gameObject;
	public static UnityEngine.UI.Image		myDropdown_template_scrollbar_slidingArea_Panel;

	public static GameObject				myDropdown_template_scrollbar_slidingArea_handle_Image_gameObject;
	public static UnityEngine.UI.Image		myDropdown_template_scrollbar_slidingArea_handle_Image;

	// MYSLIDER ---------------------------------------------
	public static GameObject				mySlider_Slider_gameObject;
	public static UnityEngine.UI.Slider		mySlider_Slider;

	public static GameObject				mySlider_background_Image_gameObject;
	public static UnityEngine.UI.Image		mySlider_background_Image;

	public static GameObject				mySlider_fillArea_Panel_gameObject;
	public static UnityEngine.UI.Image		mySlider_fillArea_Panel;

	public static GameObject				mySlider_fillArea_fill_Image_gameObject;
	public static UnityEngine.UI.Image		mySlider_fillArea_fill_Image;

	public static GameObject				mySlider_handleSlideArea_Panel_gameObject;
	public static UnityEngine.UI.Image		mySlider_handleSlideArea_Panel;

	public static GameObject				mySlider_handleSlideArea_handle_Panel_gameObject;
	public static UnityEngine.UI.Image		mySlider_handleSlideArea_handle_Panel;



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

		imageSlideShow_buttons_next_text_Other_36_gameObject = imageSlideShow_buttons_next_Button_gameObject.transform.Find("Text").gameObject;

		imageSlideShow_buttons_previous_Button_gameObject = imageSlideShow_buttons_Panel_gameObject.transform.Find("Button_Previous").gameObject;
		imageSlideShow_buttons_previous_Button = imageSlideShow_buttons_previous_Button_gameObject.GetComponent<UnityEngine.UI.Button>();

		imageSlideShow_buttons_previous_text_Other_37_gameObject = imageSlideShow_buttons_previous_Button_gameObject.transform.Find("Text").gameObject;

		imageSlideShow_buttons_back_Button_gameObject = imageSlideShow_buttons_Panel_gameObject.transform.Find("Button_Back").gameObject;
		imageSlideShow_buttons_back_Button = imageSlideShow_buttons_back_Button_gameObject.GetComponent<UnityEngine.UI.Button>();

		imageSlideShow_buttons_back_text_Other_38_gameObject = imageSlideShow_buttons_back_Button_gameObject.transform.Find("Text").gameObject;

		input_Panel_gameObject = homeMenu2_Panel_gameObject.transform.Find("Panel_Input").gameObject;
		input_Panel = input_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		input_name_Text_gameObject = input_Panel_gameObject.transform.Find("Text_Name").gameObject;
		input_name_Text = input_name_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		input_name_InputField_gameObject = input_Panel_gameObject.transform.Find("InputField_Name").gameObject;
		input_name_InputField = input_name_InputField_gameObject.GetComponent<UnityEngine.UI.InputField>();

		input_name_placeholder_Other_39_gameObject = input_name_InputField_gameObject.transform.Find("Placeholder").gameObject;

		input_name_text_Other_40_gameObject = input_name_InputField_gameObject.transform.Find("Text").gameObject;

		input_email_Text_gameObject = input_Panel_gameObject.transform.Find("Text_Email").gameObject;
		input_email_Text = input_email_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		input_email_InputField_gameObject = input_Panel_gameObject.transform.Find("InputField_Email").gameObject;
		input_email_InputField = input_email_InputField_gameObject.GetComponent<UnityEngine.UI.InputField>();

		input_email_placeholder_Other_41_gameObject = input_email_InputField_gameObject.transform.Find("Placeholder").gameObject;

		input_email_text_Other_42_gameObject = input_email_InputField_gameObject.transform.Find("Text").gameObject;

		logo_Image_gameObject = homeMenu2_Panel_gameObject.transform.Find("Image_Logo").gameObject;
		logo_Image = logo_Image_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_Dropdown_gameObject = homeMenu2_Panel_gameObject.transform.Find("Dropdown_MyDropdown").gameObject;
		myDropdown_Dropdown = myDropdown_Dropdown_gameObject.GetComponent<UnityEngine.UI.Dropdown>();

		myDropdown_label_Text_gameObject = myDropdown_Dropdown_gameObject.transform.Find("Text_Label").gameObject;
		myDropdown_label_Text = myDropdown_label_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		myDropdown_arrow_Image_gameObject = myDropdown_Dropdown_gameObject.transform.Find("Image_Arrow").gameObject;
		myDropdown_arrow_Image = myDropdown_arrow_Image_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_template_Panel_gameObject = myDropdown_Dropdown_gameObject.transform.Find("Panel_Template").gameObject;
		myDropdown_template_Panel = myDropdown_template_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_template_viewport_Panel_gameObject = myDropdown_template_Panel_gameObject.transform.Find("Panel_Viewport").gameObject;
		myDropdown_template_viewport_Panel = myDropdown_template_viewport_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_template_viewport_content_Panel_gameObject = myDropdown_template_viewport_Panel_gameObject.transform.Find("Panel_Content").gameObject;
		myDropdown_template_viewport_content_Panel = myDropdown_template_viewport_content_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_template_viewport_content_item_Panel_gameObject = myDropdown_template_viewport_content_Panel_gameObject.transform.Find("Panel_Item").gameObject;
		myDropdown_template_viewport_content_item_Panel = myDropdown_template_viewport_content_item_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_template_viewport_content_item_background_Panel_gameObject = myDropdown_template_viewport_content_item_Panel_gameObject.transform.Find("Panel_Background").gameObject;
		myDropdown_template_viewport_content_item_background_Panel = myDropdown_template_viewport_content_item_background_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_template_viewport_content_item_checkmark_Image_gameObject = myDropdown_template_viewport_content_item_Panel_gameObject.transform.Find("Image_Checkmark").gameObject;
		myDropdown_template_viewport_content_item_checkmark_Image = myDropdown_template_viewport_content_item_checkmark_Image_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_template_viewport_content_item_label_Text_gameObject = myDropdown_template_viewport_content_item_Panel_gameObject.transform.Find("Text_Label").gameObject;
		myDropdown_template_viewport_content_item_label_Text = myDropdown_template_viewport_content_item_label_Text_gameObject.GetComponent<UnityEngine.UI.Text>();

		myDropdown_template_scrollbar_Panel_gameObject = myDropdown_template_Panel_gameObject.transform.Find("Panel_Scrollbar").gameObject;
		myDropdown_template_scrollbar_Panel = myDropdown_template_scrollbar_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_template_scrollbar_slidingArea_Panel_gameObject = myDropdown_template_scrollbar_Panel_gameObject.transform.Find("Panel_SlidingArea").gameObject;
		myDropdown_template_scrollbar_slidingArea_Panel = myDropdown_template_scrollbar_slidingArea_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		myDropdown_template_scrollbar_slidingArea_handle_Image_gameObject = myDropdown_template_scrollbar_slidingArea_Panel_gameObject.transform.Find("Image_Handle").gameObject;
		myDropdown_template_scrollbar_slidingArea_handle_Image = myDropdown_template_scrollbar_slidingArea_handle_Image_gameObject.GetComponent<UnityEngine.UI.Image>();

		mySlider_Slider_gameObject = homeMenu2_Panel_gameObject.transform.Find("Slider_MySlider").gameObject;
		mySlider_Slider = mySlider_Slider_gameObject.GetComponent<UnityEngine.UI.Slider>();

		mySlider_background_Image_gameObject = mySlider_Slider_gameObject.transform.Find("Image_Background").gameObject;
		mySlider_background_Image = mySlider_background_Image_gameObject.GetComponent<UnityEngine.UI.Image>();

		mySlider_fillArea_Panel_gameObject = mySlider_Slider_gameObject.transform.Find("Panel_FillArea").gameObject;
		mySlider_fillArea_Panel = mySlider_fillArea_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		mySlider_fillArea_fill_Image_gameObject = mySlider_fillArea_Panel_gameObject.transform.Find("Image_Fill").gameObject;
		mySlider_fillArea_fill_Image = mySlider_fillArea_fill_Image_gameObject.GetComponent<UnityEngine.UI.Image>();

		mySlider_handleSlideArea_Panel_gameObject = mySlider_Slider_gameObject.transform.Find("Panel_HandleSlideArea").gameObject;
		mySlider_handleSlideArea_Panel = mySlider_handleSlideArea_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();

		mySlider_handleSlideArea_handle_Panel_gameObject = mySlider_handleSlideArea_Panel_gameObject.transform.Find("Panel_Handle").gameObject;
		mySlider_handleSlideArea_handle_Panel = mySlider_handleSlideArea_handle_Panel_gameObject.GetComponent<UnityEngine.UI.Image>();
	}
}
