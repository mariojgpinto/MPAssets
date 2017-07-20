using UnityEngine;
using System;
using System.IO;
using System.Collections;

using MPAssets;

[Prefab("GUI_MenuController", true)]
public class GUI_MenuController : Singleton<GUI_MenuController> {
	#region VARIABLES
	public GameObject gui_HomeMenu1;
	public GameObject gui_HomeMenu2;

	public static event EventHandler<ButtonPressedEventArgs> GUI_MenuHomeMenu1_ButtonPressed;

	public static event EventHandler<ButtonPressedEventArgs> GUI_MenuHomeMenu2_ButtonPressed;

	#endregion

	#region SETUP
	void FindGameObjects(){
		for(int i = 0 ; i < this.transform.childCount ; ++i){
			GameObject go = this.transform.GetChild(i).gameObject;

			switch(go.name){
				case "Panel_HomeMenu1":
					gui_HomeMenu1 = go;
					break;
				case "Panel_HomeMenu2":
					gui_HomeMenu2 = go;
					break;
				default: break;
			}
		}
	}

	void InitializeClasses(){
		gui_HomeMenu1.GetComponent<RectTransform>().localPosition = Vector3.zero;
		gui_HomeMenu2.GetComponent<RectTransform>().localPosition = Vector3.zero;

		GUI_MenuHomeMenu1.UpdateValues(gui_HomeMenu1);
		GUI_MenuHomeMenu2.UpdateValues(gui_HomeMenu2);
		gui_HomeMenu1.gameObject.SetActive(true);
		gui_HomeMenu2.gameObject.SetActive(false);
	}

	void InitializeListeners(){
		GUI_MenuHomeMenu1.imageSlideShow_buttons_next_Button.onClick.AddListener(() => Event_OnButton_HomeMenu1("imageSlideShow_buttons_next_Button"));
		GUI_MenuHomeMenu1.imageSlideShow_buttons_previous_Button.onClick.AddListener(() => Event_OnButton_HomeMenu1("imageSlideShow_buttons_previous_Button"));
		GUI_MenuHomeMenu1.imageSlideShow_buttons_back_Button.onClick.AddListener(() => Event_OnButton_HomeMenu1("imageSlideShow_buttons_back_Button"));

		GUI_MenuHomeMenu2.imageSlideShow_buttons_next_Button.onClick.AddListener(() => Event_OnButton_HomeMenu2("imageSlideShow_buttons_next_Button"));
		GUI_MenuHomeMenu2.imageSlideShow_buttons_previous_Button.onClick.AddListener(() => Event_OnButton_HomeMenu2("imageSlideShow_buttons_previous_Button"));
		GUI_MenuHomeMenu2.imageSlideShow_buttons_back_Button.onClick.AddListener(() => Event_OnButton_HomeMenu2("imageSlideShow_buttons_back_Button"));

	}

	protected static void OnButtonPressed(string id, string desc, EventHandler<ButtonPressedEventArgs> handler){
		ButtonPressedEventArgs args = new ButtonPressedEventArgs();
		args.id = id;
		args.description = desc;

		if(handler != null){
			handler(GUI_MenuController.instance, args);
		}
	}

	protected static void OnTogglePressed(string id, string desc, bool value, EventHandler<TogglePressedEventArgs> handler){
		TogglePressedEventArgs args = new TogglePressedEventArgs();
		args.id = id;
		args.description = desc;

		args.value = value;

		if(handler != null){
			handler(GUI_MenuController.instance, args);
		}
	}
	#endregion

	#region CALLBACKS
	void Event_OnButton_HomeMenu1(string id){
		switch(id){
			case "imageSlideShow_buttons_next_Button" : //HOMEMENU1 - NEXT
				OnButtonPressed(id, "HOMEMENU1 - BUTTON NEXT",GUI_MenuHomeMenu1_ButtonPressed);
				break;
			case "imageSlideShow_buttons_previous_Button" : //HOMEMENU1 - PREVIOUS
				OnButtonPressed(id, "HOMEMENU1 - BUTTON PREVIOUS",GUI_MenuHomeMenu1_ButtonPressed);
				break;
			case "imageSlideShow_buttons_back_Button" : //HOMEMENU1 - BACK
				OnButtonPressed(id, "HOMEMENU1 - BUTTON BACK",GUI_MenuHomeMenu1_ButtonPressed);
				break;
			default: break;
		}
	}

	void Event_OnButton_HomeMenu2(string id){
		switch(id){
			case "imageSlideShow_buttons_next_Button" : //HOMEMENU2 - NEXT
				OnButtonPressed(id, "HOMEMENU2 - BUTTON NEXT",GUI_MenuHomeMenu2_ButtonPressed);
				break;
			case "imageSlideShow_buttons_previous_Button" : //HOMEMENU2 - PREVIOUS
				OnButtonPressed(id, "HOMEMENU2 - BUTTON PREVIOUS",GUI_MenuHomeMenu2_ButtonPressed);
				break;
			case "imageSlideShow_buttons_back_Button" : //HOMEMENU2 - BACK
				OnButtonPressed(id, "HOMEMENU2 - BUTTON BACK",GUI_MenuHomeMenu2_ButtonPressed);
				break;
			default: break;
		}
	}

	#endregion

	#region UNITY_CALLBACKS
	protected override void Awake(){
		base.Awake();

		if (destroyed)
			return;

		FindGameObjects();

		InitializeClasses();
		InitializeListeners();
	}

//	void Update(){
//		
//		}

//	void OnGUI(){
//		
//		}
	#endregion

	#region COMMENTS
//	void OnButton_HomeMenu1(object sender, ButtonPressedEventArgs e){
//		switch(e.id){
//			case "imageSlideShow_buttons_next_Button" : //HOMEMENU1 - NEXT
//				Debug.Log(e.id + " - HOMEMENU1 - BUTTON NEXT");

//				break;
//			case "imageSlideShow_buttons_previous_Button" : //HOMEMENU1 - PREVIOUS
//				Debug.Log(e.id + " - HOMEMENU1 - BUTTON PREVIOUS");

//				break;
//			case "imageSlideShow_buttons_back_Button" : //HOMEMENU1 - BACK
//				Debug.Log(e.id + " - HOMEMENU1 - BUTTON BACK");

//				break;
//			default: break;
//		}
//	}

//	void OnButton_HomeMenu2(object sender, ButtonPressedEventArgs e){
//		switch(e.id){
//			case "imageSlideShow_buttons_next_Button" : //HOMEMENU2 - NEXT
//				Debug.Log(e.id + " - HOMEMENU2 - BUTTON NEXT");

//				break;
//			case "imageSlideShow_buttons_previous_Button" : //HOMEMENU2 - PREVIOUS
//				Debug.Log(e.id + " - HOMEMENU2 - BUTTON PREVIOUS");

//				break;
//			case "imageSlideShow_buttons_back_Button" : //HOMEMENU2 - BACK
//				Debug.Log(e.id + " - HOMEMENU2 - BUTTON BACK");

//				break;
//			default: break;
//		}
//	}

//	void AssignEvents(){
//		GUI_MenuController.GUI_MenuHomeMenu1_ButtonPressed += OnButton_HomeMenu1;
//		GUI_MenuController.GUI_MenuHomeMenu2_ButtonPressed += OnButton_HomeMenu2;

//	}
	#endregion
}
