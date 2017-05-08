using UnityEngine;
using System;
using System.IO;
using System.Collections;

[Prefab("GUI_Controller", true)]
public class GUI_Controller : Singleton<GUI_Controller> {
	#region VARIABLES
	public GameObject gui_Full;
	public GameObject gui_Left;
	public GameObject gui_Right;

	public static event EventHandler<ButtonPressedEventArgs> GUI_Full_ButtonPressed;

	#endregion

	#region SETUP
	void FindGameObjects(){
		for(int i = 0 ; i < this.transform.childCount ; ++i){
			GameObject go = this.transform.GetChild(i).gameObject;

			switch(go.name){
				case "Panel_Full":
					gui_Full = go;
					break;
				case "Panel_Left":
					gui_Left = go;
					break;
				case "Panel_Right":
					gui_Right = go;
					break;
				default: break;
			}
		}
	}

	void InitializeClasses(){
		gui_Full.GetComponent<RectTransform>().localPosition = Vector3.zero;
		gui_Left.GetComponent<RectTransform>().localPosition = Vector3.zero;
		gui_Right.GetComponent<RectTransform>().localPosition = Vector3.zero;

		GUI_Full.UpdateValues(gui_Full);
		GUI_Left.UpdateValues(gui_Left);
		GUI_Right.UpdateValues(gui_Right);
		gui_Full.gameObject.SetActive(true);
		gui_Left.gameObject.SetActive(false);
		gui_Right.gameObject.SetActive(false);
	}

	void InitializeListeners(){
		GUI_Full.toggleRight_Button.onClick.AddListener(() => Event_OnButton_Full("toggleRight_Button"));
		GUI_Full.toggleLeft_Button.onClick.AddListener(() => Event_OnButton_Full("toggleLeft_Button"));
		GUI_Full.toggleLeft2_Button.onClick.AddListener(() => Event_OnButton_Full("toggleLeft2_Button"));

	}

	protected static void OnButtonPressed(string id, string desc, EventHandler<ButtonPressedEventArgs> handler){
		ButtonPressedEventArgs args = new ButtonPressedEventArgs();
		args.id = id;
		args.description = desc;

		if(handler != null){
			handler(GUI_Controller.instance, args);
		}
	}

	protected static void OnTogglePressed(string id, string desc, bool value, EventHandler<TogglePressedEventArgs> handler){
		TogglePressedEventArgs args = new TogglePressedEventArgs();
		args.id = id;
		args.description = desc;

		args.value = value;

		if(handler != null){
			handler(GUI_Controller.instance, args);
		}
	}
	#endregion

	#region CALLBACKS
	void Event_OnButton_Full(string id){
		switch(id){
			case "toggleRight_Button" : //FULL - TOGGLERIGHT
				OnButtonPressed(id, "FULL - BUTTON TOGGLERIGHT",GUI_Full_ButtonPressed);
				break;
			case "toggleLeft_Button" : //FULL - TOGGLELEFT
				OnButtonPressed(id, "FULL - BUTTON TOGGLELEFT",GUI_Full_ButtonPressed);
				break;
			case "toggleLeft2_Button" : //FULL - TOGGLELEFT2
				OnButtonPressed(id, "FULL - BUTTON TOGGLELEFT2",GUI_Full_ButtonPressed);
				break;
			default: break;
		}
	}

	#endregion

	#region UNITY_CALLBACKS
	protected override void Awake(){
		base.Awake();

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
//	void OnButton_Full(object sender, ButtonPressedEventArgs e){
//		switch(e.id){
//			case "toggleRight_Button" : //FULL - TOGGLERIGHT
//				Debug.Log(e.id + " - FULL - BUTTON TOGGLERIGHT");

//				break;
//			case "toggleLeft_Button" : //FULL - TOGGLELEFT
//				Debug.Log(e.id + " - FULL - BUTTON TOGGLELEFT");

//				break;
//			case "toggleLeft2_Button" : //FULL - TOGGLELEFT2
//				Debug.Log(e.id + " - FULL - BUTTON TOGGLELEFT2");

//				break;
//			default: break;
//		}
//	}

//	void AssignEvents(){
//		GUI_Controller.GUI_Full_ButtonPressed += OnButton_Full;

//	}
	#endregion
}
