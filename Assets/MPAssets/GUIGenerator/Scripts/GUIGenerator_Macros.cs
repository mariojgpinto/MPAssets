
public static class GUIGenerator_Macros{
	#region GENERAL
	public static string replacement_name = "NNNNNN";
	public static string replacement_type = "TTTTTT";
	public static string replacement_variable = "VVVVVV";
	public static string replacement_variable2 = "V2V2V2V2V2V2";
	public static string replacement_variable3 = "V3V3V3V3V3V3";
	public static string replacement_variableParent = "PPPPPP";
	public static string replacement_function = "FFFFFF";
	#endregion

	#region ELEMENTS
	public static string elem_panel  = "Panel_";
	public static string elem_image  = "Image_";
	public static string elem_text 	 = "Text_";
	public static string elem_button = "Button_";
	public static string elem_toggle = "Toggle_";
	public static string elem_scroll = "Scroll_";
	public static string elem_input  = "InputField_";
	public static string elem_other  = "Other_";

	public static string sub_gameObject  = "_gameObject";
	public static string sub_panel  = "_Panel";
	public static string sub_image  = "_Image";
	public static string sub_text 	= "_Text";
	public static string sub_button = "_Button";
	public static string sub_toggle = "_Toggle";
	public static string sub_scroll = "_Scroll";
	public static string sub_input =  "_InputField";
	public static string sub_other  = "_Other";

	public static string type_gameObject  = "GameObject";
	public static string type_panel  = "UnityEngine.UI.Image";
	public static string type_image  = "UnityEngine.UI.Image";
	public static string type_text 	= "UnityEngine.UI.Text";
	public static string type_button = "UnityEngine.UI.Button";
	public static string type_toggle = "UnityEngine.UI.Toggle";
	public static string type_scroll = "GameObject";
	public static string type_input = "UnityEngine.UI.InputField";
	public static string type_other  = "GameObject";

	public static string typeFormated_gameObject  = "GameObject\t\t\t\t";
	public static string typeFormated_panel  = "UnityEngine.UI.Image\t\t";
	public static string typeFormated_image  = "UnityEngine.UI.Image\t\t";
	public static string typeFormated_text 	 = "UnityEngine.UI.Text\t\t";
	public static string typeFormated_button = "UnityEngine.UI.Button\t\t";
	public static string typeFormated_toggle = "UnityEngine.UI.Toggle\t\t";
	public static string typeFormated_scroll = "GameObject\t\t\t\t";
	public static string typeFormated_input  = "UnityEngine.UI.InputField\t";
	public static string typeFormated_other  = "GameObject\t\t\t\t";

	#endregion

	#region FILES_GENERATION
	public static string text_classPrefix = "GUI_";

	public static string file_init = "GUIGenerator_";
	public static string file_end = ".cs";

	public static string text_includes = "" +
			"using UnityEngine;\n" +
			"using System;\n" +
			"using System.IO;\n" + 
			"using System.Collections;\n\n";

	public static string text_classDeclaration = "public class " + replacement_name + " : MonoBehaviour {\n";

	public static string text_variableInstance = "\tpublic static " + replacement_type + " instance;\n\n";
	public static string text_variableDeclaration = "\tpublic static " + replacement_type + replacement_name + ";\n";

	public static string text_function_UpdateValues_Header = "\tpublic static void UpdateValues(GameObject panel){\n";
	public static string text_function_UpdateValues_GetGameObjectParent = "\t\t" + replacement_variable + " = " + replacement_variableParent + ".gameObject;\n";
	public static string text_function_UpdateValues_GetGameObject = "\t\t" + replacement_variable + " = " + replacement_variableParent + ".transform.Find(\"" + replacement_name + "\").gameObject;\n";
	public static string text_function_UpdateValues_GetComponent = "\t\t" + replacement_variable + " = " + replacement_variableParent + ".GetComponent<"+ replacement_type +">();\n";
	public static string text_function_UpdateValues_ReplacementParent = "panel";



	public static string text_regionMacro_Variables = "VARIABLES";
	public static string text_regionMacro_Setup = "SETUP";
	public static string text_regionMacro_Callbacks = "CALLBACKS";
	public static string text_regionMacro_UnityCallbacks = "UNITY_CALLBACKS";
	public static string text_regionMacro_Comments = "COMMENTS";


	public static string text_regionBegin = "\t#region " + replacement_name + "\n";
	public static string text_regionEnd = "\t#endregion\n";

	public static string text_function_End = "\t}\n";
	public static string text_comment_End = "//\t}\n";

	public static string text_classEnd = "}\n";

	#endregion

	#region FILE_CLASSES
	public static string file_classes = "Classes";

	public static string text_classEventButtonName = "ButtonPressedEventArgs";
	public static string text_classEventButton = "" +
		"public class " + text_classEventButtonName + " : EventArgs {\n" +
		"\tpublic string id { get; set; }\n" +
		"\tpublic string description { get; set; }\n" +
		"}\n\n";

	public static string text_classEventToggleName = "TogglePressedEventArgs";
	public static string text_classEventToggle = "" +
		"public class " + text_classEventToggleName + " : EventArgs {\n" +
		"\tpublic string id { get; set; }\n" +
		"\tpublic string description { get; set; }\n\n" +
		"\tpublic bool value { get; set; }\n" +
		"}\n\n";
	#endregion

	#region FILE_CONTROLLER
	public static string file_controller = "Controller";

	public static string text_gameObjectVariable = "\tpublic GameObject " + replacement_name + ";\n";

	public static string text_buttonEventHandlerVariableName = replacement_variable + "_ButtonPressed";
	public static string text_buttonEventHandlerVariable = "\tpublic static event EventHandler<" + replacement_type + "> " + text_buttonEventHandlerVariableName + ";\n";

	public static string text_toggleEventHandlerVariableName = replacement_variable + "_TogglePressed";
	public static string text_toggleEventHandlerVariable = "\tpublic static event EventHandler<" + replacement_type + "> " + text_toggleEventHandlerVariableName + ";\n";

	public static string text_function_FindObjects_Header = "\tvoid FindGameObjects(){\n";
	public static string text_function_FindObjects_Method_Find = "\t\t" + replacement_variable + " = GameObject.Find(\"" + replacement_name + "\");\n";

	public static string text_function_InitializeValues_Header = "\tvoid InitializeClasses(){\n";
	public static string text_function_InitializeValues_Method_Comment = "\t// " + replacement_variable + " ---------------------------------------------\n";
	public static string text_function_InitializeValues_Method_ResetPosition = "\t\t" + replacement_variable + ".GetComponent<RectTransform>().localPosition = Vector3.zero;\n";
	public static string text_function_InitializeValues_Method_UpdateValues = "\t\t" + replacement_name + ".UpdateValues(" + replacement_variable + ");\n";
	public static string text_function_InitializeValues_Method_SetActive = "\t\t" + replacement_name + ".gameObject.SetActive(" + replacement_variable + ");\n";

	public static string text_function_InitializeListeners_Header = "\tvoid InitializeListeners(){\n";
	public static string text_function_InitializeListeners_AddClickListener = "\t\t" + replacement_variable + "." + replacement_variable2 + ".onClick.AddListener(() => " + replacement_variable3 + ");\n";
	public static string text_function_InitializeListeners_AddToggleListener = "\t\t" + replacement_variable + "." + replacement_variable2 + ".onValueChanged.AddListener((bool value) => " + replacement_variable3 + ");\n";

	public static string text_function_OnButtonPressedName = "OnButtonPressed";
	public static string text_function_OnButtonPressed = "" + 
		"\tprotected static void " + text_function_OnButtonPressedName + "(string id, string desc, EventHandler<" + text_classEventButtonName + "> handler){\n" + 
		"\t\t" + text_classEventButtonName + " args = new " + text_classEventButtonName + "();\n" + 
		"\t\targs.id = id;\n" + 		
		"\t\targs.description = desc;\n\n" + 		
		"\t\tif(handler != null){\n" + 
		"\t\t\thandler(" + replacement_variable + ".instance, args);\n" + 
		"\t\t}\n" + 
		"\t}\n";

	public static string text_function_OnTogglePressedName = "OnTogglePressed";
	public static string text_function_OnTogglePressed = "" + 
		"\tprotected static void " + text_function_OnTogglePressedName + "(string id, string desc, bool value, EventHandler<" + text_classEventToggleName + "> handler){\n" + 
			"\t\t" + text_classEventToggleName + " args = new " + text_classEventToggleName + "();\n" + 
			"\t\targs.id = id;\n" + 	
			"\t\targs.description = desc;\n\n" + 		
			"\t\targs.value = value;\n\n" + 
			"\t\tif(handler != null){\n" + 
			"\t\t\thandler(" + replacement_variable + ".instance, args);\n" + 
			"\t\t}\n" + 
			"\t}\n";

	public static string text_function_OnButtonCallback_Prefix = "Event_OnButton_" + replacement_name + "(" + replacement_variable + ")";
	public static string text_function_OnButtonCallback_Header = "\tvoid Event_OnButton_" + replacement_name + "(string id){\n";
	public static string text_function_OnButtonCallback_Call = text_function_OnButtonPressedName + "(id, \"" + replacement_name + "\"," + replacement_variable + ");";
	public static string text_function_OnButtonCallback_SwitchInit = "\t\tswitch(id){\n";
	public static string text_function_OnButtonCallback_Case = 	
		"\t\t\tcase " + replacement_variable + " : //" + replacement_variableParent + " - " + replacement_name + 
		"\n\t\t\t\t" + replacement_function + 
		"\n\t\t\t\tbreak;\n";
	public static string text_function_OnButtonCallback_SwitchEnd = "\t\t\tdefault: break;\n\t\t}\n";

	public static string text_function_OnToggleCallback_Prefix = "Event_OnToggle_" + replacement_name + "("  + replacement_variable + ", value)";
	public static string text_function_OnToggleCallback_Header = "\tvoid Event_OnToggle_" + replacement_name + "(string id, bool value){\n";
	public static string text_function_OnToggleCallback_Call = text_function_OnTogglePressedName + "(id, \"" + replacement_name + "\", value," + replacement_variable + ");";
	public static string text_function_OnToggleCallback_SwitchInit = "\t\tswitch(id){\n";
	public static string text_function_OnToggleCallback_Case = 	
		"\t\t\tcase " + replacement_variable + " : //" + replacement_variableParent + " - " + replacement_name + 
		"\n\t\t\t\t" + replacement_function + 
		"\n\t\t\t\tbreak;\n";
	public static string text_function_OnToggleCallback_SwitchEnd = "\t\t\tdefault: break;\n\t\t}\n";






	public static string text_comment_OnButtonCallback_Header = "//\tvoid OnButton_" + replacement_name + "(object sender, ButtonPressedEventArgs e){\n";
	public static string text_comment_OnButtonCallback_Call = text_function_OnButtonPressedName + "(id, \"" + replacement_name + "\"," + replacement_variable + ");";
	public static string text_comment_OnButtonCallback_SwitchInit = "//\t\tswitch(e.id){\n";
	public static string text_comment_OnButtonCallback_Case = 	
		"//\t\t\tcase " + replacement_variable + " : //" + replacement_variableParent + " - " + replacement_name + "\n" + 
			"//\t\t\t\tDebug.Log(e.id + \" - " + replacement_variableParent +  " - BUTTON " + replacement_name + "\");" + "\n\n" + 
			"//\t\t\t\tbreak;\n";
	public static string text_comment_OnButtonCallback_SwitchEnd = "//\t\t\tdefault: break;\n//\t\t}\n";


	public static string text_comment_OnToggleCallback_Header = "//\tvoid OnToggle_" + replacement_name + "(object sender, TogglePressedEventArgs e){\n";
	public static string text_comment_OnToggleCallback_SwitchInit = "//\t\tswitch(e.id){\n";
	public static string text_comment_OnToggleCallback_Case = 	
		"//\t\t\tcase " + replacement_variable + " : //" + replacement_variableParent + " - " + replacement_name + 
		"\n//\t\t\t\tDebug.Log(e.id + \" - " + replacement_variableParent +  " - TOGGLE " + replacement_name + " (\" + e.value + \")\");" + 
		"\n//\t\t\t\t" + 
		"\n//\t\t\t\tif(e.value){" + 
		"\n//\t\t\t\t\t" + 
		"\n//\t\t\t\t} else{ "+ 
		"\n//\t\t\t\t\t" + 
		"\n//\t\t\t\t}" +
		"\n\n//\t\t\t\tbreak;\n";
	public static string text_comment_OnToggleCallback_SwitchEnd = "//\t\t\tdefault: break;\n//\t\t}\n";

	public static string text_comment_AssignEvents_Header = "//\tvoid AssignEvents(){\n";
	public static string text_comment_OnButtonCallback_Assign = "//\t\t" + replacement_variable + "." + replacement_variable2 + " += OnButton_" + replacement_name + ";\n";
	public static string text_comment_OnToggleCallback_Assign = "//\t\t" + replacement_variable + "." + replacement_variable2 + " += OnToggle_" + replacement_name + ";\n";
	public static string text_comment_AssignEvents_End = "//\t}\n";



	public static string text_function_ControllerStart_Full = 
			"\tvoid Awake(){\n" +
			"\t\tif(instance == null){\n" + 
			"\t\t\tinstance = this;\n" + 
			"\t\t}\n" + 
			"\t\telse{\n" + 
			"\t\t\tif (instance != this) {\n" + 
			"\t\t\t\tDestroy(gameObject);\n"  + 
			"\t\t\t\treturn;\n" + 
			"\t\t\t}\n" + 
			"\t\t}\n\n" + 
			"\t\tFindGameObjects();\n\n" + 
			"\t\tInitializeClasses();\n" + 
			"\t\tInitializeListeners();\n" + 
			"\t}\n";

	public static string text_function_Update_Commented = "//\tvoid Update(){\n//\t\t\n//\t\t}\n";

	public static string text_function_OnGUI_Commented = "//\tvoid OnGUI(){\n//\t\t\n//\t\t}\n";

	#endregion

	#region FILE_ANIMATIONS
	public static string file_animation = "Animation";

	public static string text_variableAnimationInstance = "\tpublic static " + replacement_variable + " instance;\n";
	//public static string text_variableAnimationTime = "\tpublic static float animationTime = .25f;\n";

	public static string text_regionMacro_AnimationShowHide = "SHOW_HIDE";
	public static string text_function_ShowMenu = 	"\tpublic static void ShowMenu(GameObject panel)\n" +
													"\t{\n" + 
													"\t\tpanel.SetActive(true);\n" + 
													"\t\tpanel.GetComponent<RectTransform>().localPosition = Vector3.zero;\n" + 
													"\t}\n";

	public static string text_function_HideMenu = 	"\tpublic static void HideMenu(GameObject panel)\n" +
													"\t{\n" + 
													"\t\tpanel.SetActive(false);\n" + 
													"\t\tpanel.GetComponent<RectTransform>().localPosition = new Vector3(Screen.width,0,0);\n" + 
													"\t}\n";

	public static string text_regionMacro_AnimationSlide = "SLIDE";
	public static string text_functionSideBase = "\tpublic static IEnumerator SlidePanel_Routine(GameObject panel, Vector3 initialPosition, Vector3 finalPosition, float animationTime = .25f, float delayTime = 0f, bool removeElement = false)\n" +
													"\t{\n" +
													"\t\tfloat progress = 0;\n\n" +
													"\t\tif(delayTime>0)\n" + 
													"\t\t\tyield return new WaitForSeconds(delayTime);\n\n" +
													"\t\tif(!removeElement){\n\t\t\tpanel.SetActive(true);\n\t\t}\n\n" + 
													"\t\tRectTransform panelPos = panel.GetComponent<RectTransform>();\n\n" +
													"\t\tVector3 deltaPosition = finalPosition - initialPosition;\n" +
													"\t\tVector3 endPosition = finalPosition;\n\n" +		
													"\t\twhile(progress < 1)\n" +
													"\t\t{\n" +
													"\t\t\tpanelPos.localPosition = initialPosition + deltaPosition * progress;\n\n" +			
													"\t\t\tprogress += Time.deltaTime/animationTime;\n" +
													"\t\t\tyield return true;\n" +
													"\t\t}\n\n" +		
													"\t\tpanelPos.localPosition = endPosition;\n\n" +
													"\t\tif(removeElement){\n\t\t\tpanel.SetActive(false);\n\t\t}\n\n" + 
													"\t\tyield break;\n" +
													"\t}\n";

	public static string text_functionSlidePanel	 = "\tpublic static void SlidePanel(GameObject panel, Vector3 initialPosition, Vector3 finalPosition, float animationTime = .25f, float delayTime = 0f, bool removeElement = false)\n" +
														"\t{\n" +
														"\t\tinstance.SlidePanel_Instance(panel, initialPosition, finalPosition, animationTime, delayTime, removeElement);\n" +
														"\t}\n";

	public static string text_functionSideBringRight = "\tpublic static void BringBackFromRight(GameObject panel, float animationTime = .25f, float delayTime = 0f)\n" +
														"\t{\n" +
														"\t\tinstance.SlidePanel_Instance(panel, new Vector3(Screen.width,0,0), Vector3.zero, animationTime, delayTime);\n" +
														"\t}\n";

	public static string text_functionSideBringLeft = "\tpublic static void BringBackFromLeft(GameObject panel, float animationTime = .25f, float delayTime = 0f)\n" +
														"\t{\n" +
														"\t\tinstance.SlidePanel_Instance(panel, new Vector3(-Screen.width,0,0), Vector3.zero, animationTime, delayTime);\n" +
														"\t}\n";

	public static string text_functionSideRemoveRight = "\tpublic static void RemoveToRight(GameObject panel, float animationTime = .25f, float delayTime = 0f)\n" +
														"\t{\n" +
														"\t\tinstance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(Screen.width,0,0), animationTime, delayTime, true);\n" +
														"\t}\n";

	public static string text_functionSideRemoveLeft = "\tpublic static void RemoveToLeft(GameObject panel, float animationTime = .25f, float delayTime = 0f)\n" +
														"\t{\n" +
														"\t\tinstance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(-Screen.width,0,0), animationTime, delayTime, true);\n" +
														"\t}\n";

	public static string text_functionSideBringTop = "\tpublic static void BringBackFromTop(GameObject panel, float animationTime = .25f, float delayTime = 0f)\n" +
														"\t{\n" +
														"\t\tinstance.SlidePanel_Instance(panel, new Vector3(0,Screen.height,0), Vector3.zero, animationTime, delayTime);\n" +
														"\t}\n";
	
	public static string text_functionSideBringBottom = "\tpublic static void BringBackFromBottom(GameObject panel, float animationTime = .25f, float delayTime = 0f)\n" +
														"\t{\n" +
														"\t\tinstance.SlidePanel_Instance(panel, new Vector3(0,-Screen.height,0), Vector3.zero, animationTime, delayTime);\n" +
														"\t}\n";
	
	public static string text_functionSideRemoveTop = "\tpublic static void RemoveToTop(GameObject panel, float animationTime = .25f, float delayTime = 0f)\n" +
														"\t{\n" +
														"\t\tinstance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(0,Screen.height,0), animationTime, delayTime, true);\n" +
														"\t}\n";
	
	public static string text_functionSideRemoveBottom = "\tpublic static void RemoveToBottom(GameObject panel, float animationTime = .25f, float delayTime = 0f)\n" +
														"\t{\n" +
														"\t\tinstance.SlidePanel_Instance(panel, Vector3.zero, new Vector3(0,-Screen.height,0), animationTime, delayTime, true);\n" +
														"\t}\n";

	public static string text_functionSideBaseInstance = "\tvoid SlidePanel_Instance(GameObject panel, Vector3 initialPosition, Vector3 finalPosition, float animationTime = .25f, float delayTime = 0f, bool removeElement = false)\n" +
														"\t{\n" +
														"\t\tStartCoroutine(SlidePanel_Routine(panel, initialPosition, finalPosition, animationTime, delayTime, removeElement));\n" +
														"\t}\n";

	public static string text_functionAnimationAwake = "\tvoid Awake()\n" +
														"\t{\n" +
														"\t\t" + replacement_variable + ".instance = this;\n" +
														"\t}\n";


	#endregion

	#region DIRECTORIES
	public static string directory_mainScripts = "/Scripts";
	public static string directory_GUIScripts = "/GUIGenerator/";

	#endregion
}