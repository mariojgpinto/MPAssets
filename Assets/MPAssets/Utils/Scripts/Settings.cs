using UnityEngine;


namespace MPAssets {
	public class Settings : MonoBehaviour {
		#region VARIABLES
		//--------------------------------------------------
		// CONTROL VARIABLES
		//--------------------------------------------------
		public static Settings instance;

		public static bool activeDebug = true;

		static int currentHeight = 10;
		static int height = 30;

		static string windowTitle = "Settings";

		static int topMargin = 10;
		static int leftMargin = 10;
		static int labelLenght = 190;
		static int labelToggleLenght = 300;
		static int sliderLenght = 550;


		//--------------------------------------------------
		// MY VARIABLES
		//--------------------------------------------------
		public static bool myBoolVariable;

		public static int myIntVariable;
		public static float myFloatVariable;
		public static string myStringSingleLineVariable = "";
		public static string myStringMultiLineVariable = "";

		public static int listIdx = 0;
		public static string[] listValues = new string[] { "Value1", "Value2", "Value3" };
		#endregion

		#region MY_SETTINGS
		void MyDebugSettings(int windowID = 0) {
			//RESET POSITION
			currentHeight = topMargin;

			//ADD FIELDS
			AddTitle("A LABEL");

			AddToggle(ref myBoolVariable, "A Bool Toggle");

			AddFloatSlider(ref myFloatVariable, "A Float Slider", 0, 100);

			AddIntSlider(ref myIntVariable, "An Int Slider", 0, 100);

			AddTextField(ref myStringSingleLineVariable, "String Single Line");

			AddTextArea(ref myStringMultiLineVariable, "String Multi Line");

			AddList(ref listIdx, listValues, "List Select");

			//BACKGROUND BOX
			//GUI.Box(new Rect(0, 0, leftMargin + labelLenght + sliderLenght + leftMargin, currentHeight + height), "");
		}

		#endregion

		#region GUI_LAYOUT
		//Adds a Label to the debug interface
		void AddTitle(string name) {
			GUI.Label(new Rect(leftMargin, currentHeight, labelLenght, height), name);
			currentHeight += height;
		}

		//TOGGLE BOOL VALUE
		void AddToggle(ref bool value, string name) {
			value = GUI.Toggle(new Rect(leftMargin, currentHeight, labelToggleLenght, height), value, name);
			currentHeight += height;
		}

		//SLIDER INT VALUE
		void AddIntSlider(ref int value, string name, float min, float max) {
			value = Mathf.RoundToInt(GUI.HorizontalSlider(new Rect(labelLenght + leftMargin, currentHeight, sliderLenght, height), value, min, max));
			GUI.Label(new Rect(leftMargin, currentHeight, labelLenght, height), name + " (" + value + ")");
			currentHeight += height;
		}

		//SLIDER FLOAT VALUE
		void AddFloatSlider(ref float value, string name, float min, float max) {
			value = GUI.HorizontalSlider(new Rect(labelLenght + leftMargin, currentHeight, sliderLenght, height), value, min, max);
			GUI.Label(new Rect(leftMargin, currentHeight, labelLenght, height), name + " (" + value.ToString(".000") + ")");
			currentHeight += height;
		}

		//STRING VALUE SINGLE LINE
		void AddTextField(ref string value, string name) {
			GUI.Label(new Rect(leftMargin, currentHeight, labelLenght, height), name);
			value = GUI.TextField(new Rect(labelLenght + leftMargin, currentHeight, labelToggleLenght, height), value);
			currentHeight += height;
		}

		//STRING VALUE MULTI LINE
		void AddTextArea(ref string value, string name) {
			GUI.Label(new Rect(leftMargin, currentHeight, labelLenght, height), name);
			value = GUI.TextArea(new Rect(labelLenght + leftMargin, currentHeight, labelToggleLenght, height), value);
			currentHeight += height;
		}

		//TOGGLE STRING VALUE MULTI LINE
		void AddList(ref int index, string[] values, string name) {
			index = GUI.Toolbar(new Rect(labelLenght + leftMargin, currentHeight, labelLenght, height), index, values);
			GUI.Label(new Rect(leftMargin, currentHeight, labelLenght, height), name + "(" + index + ")");
			currentHeight += height;
		}

		#endregion

		#region UNITY_CALLBACKS
		void Awake() {
			//Check if instance already exists
			if (instance == null) {
				//if not, set instance to this
				instance = this;
			}

			//If instance already exists and it's not this:
			else {
				if (instance != this) {
					//Then destroy this. This enforces our singleton pattern.
					Destroy(gameObject);
					return;
				}
			}

			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
		}

		void OnGUI() {
			if (activeDebug) {
				//MyDebugSettings();
				GUI.ModalWindow(0, new Rect(
					(Screen.width - (leftMargin + labelLenght + sliderLenght + leftMargin)) / 2f,
					(Screen.height - (currentHeight + height)) / 2f,
					leftMargin + labelLenght + sliderLenght + leftMargin
					, currentHeight + height), MyDebugSettings, windowTitle);
			}
		}
		#endregion
	}
}