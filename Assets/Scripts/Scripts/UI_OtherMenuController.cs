using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OtherMenuController : MonoBehaviour {
	#region VARIABLES

	#endregion

	void OnButton_HomeMenu1(object sender, ButtonPressedEventArgs e) {
		switch (e.id) {
			case "imageSlideShow_buttons_next_Button": //HOMEMENU1 - NEXT
				Debug.Log(e.id + " - HOMEMENU1 - BUTTON NEXT");
				GUI_Animation.SwitchMenus(GUI_OtherMenuController.instance.gui_HomeMenu1, GUI_OtherMenuController.instance.gui_HomeMenu2, GUI_Animation.ANIMANTION.SHOW_HIDE);
				break;
			case "imageSlideShow_buttons_previous_Button": //HOMEMENU1 - PREVIOUS
				Debug.Log(e.id + " - HOMEMENU1 - BUTTON PREVIOUS");
				GUI_Animation.SwitchMenus(GUI_OtherMenuController.instance.gui_HomeMenu1, GUI_OtherMenuController.instance.gui_HomeMenu2, GUI_Animation.ANIMANTION.SCALE);
				break;
			case "imageSlideShow_buttons_back_Button": //HOMEMENU1 - BACK
				Debug.Log(e.id + " - HOMEMENU1 - BUTTON BACK");
				GUI_Animation.SwitchMenus(GUI_OtherMenuController.instance.gui_HomeMenu1, GUI_OtherMenuController.instance.gui_HomeMenu2, GUI_Animation.ANIMANTION.FADE);
				break;
			default:
				break;
		}
	}

	void OnButton_HomeMenu2(object sender, ButtonPressedEventArgs e) {
		switch (e.id) {
			case "imageSlideShow_buttons_next_Button": //HOMEMENU2 - NEXT
				Debug.Log(e.id + " - HOMEMENU2 - BUTTON NEXT");
				GUI_Animation.SwitchMenus(GUI_OtherMenuController.instance.gui_HomeMenu2, GUI_OtherMenuController.instance.gui_HomeMenu1, GUI_Animation.ANIMANTION.SHOW_HIDE);

				break;
			case "imageSlideShow_buttons_previous_Button": //HOMEMENU2 - PREVIOUS
				Debug.Log(e.id + " - HOMEMENU2 - BUTTON PREVIOUS");
				GUI_Animation.SwitchMenus(GUI_OtherMenuController.instance.gui_HomeMenu2, GUI_OtherMenuController.instance.gui_HomeMenu1, GUI_Animation.ANIMANTION.SCALE);

				break;
			case "imageSlideShow_buttons_back_Button": //HOMEMENU2 - BACK
				Debug.Log(e.id + " - HOMEMENU2 - BUTTON BACK");
				GUI_Animation.SwitchMenus(GUI_OtherMenuController.instance.gui_HomeMenu2, GUI_OtherMenuController.instance.gui_HomeMenu1, GUI_Animation.ANIMANTION.FADE);

				break;
			default:
				break;
		}
	}

	void OnToggle_HomeMenu2(object sender, TogglePressedEventArgs e) {
		switch (e.id) {
			case "imageSlideShow_myToggle_Toggle": //HOMEMENU2 - MYTOGGLE
				Debug.Log(e.id + " - HOMEMENU2 - TOGGLE MYTOGGLE (" + e.value + ")");

				if (e.value) {

				}
				else {

				}

				break;
			default:
				break;
		}
	}

	void AssignEvents() {
		GUI_OtherMenuController.RemoveAllEvents();

		GUI_OtherMenuController.GUI_OtherMenuHomeMenu1_ButtonPressed += OnButton_HomeMenu1;
		GUI_OtherMenuController.GUI_OtherMenuHomeMenu2_ButtonPressed += OnButton_HomeMenu2;

		GUI_OtherMenuController.GUI_OtherMenuHomeMenu2_TogglePressed += OnToggle_HomeMenu2;
	}

	#region UNITY_CALLBACKS
	// Use this for initialization
	void Start () {
		AssignEvents();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#endregion
}
