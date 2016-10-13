using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System;


public class Test : MonoBehaviour {
	#region VARIABLES
	public InputField key_String;
	public InputField value_String;

	public InputField key_Int;
	public InputField value_Int;

	public InputField key_Float;
	public InputField value_Float;

	public InputField key_List;
	public InputField value_List;

	public InputField key_Image;
	public RawImage   value_Image;

	public InputField key_Path;
	public InputField key_FileName;
	public Text 	  text_FilePathInput;
	public Text 	  text_FilePathOutput;


	public Text 	text_Output;
	public RawImage image_Output;




	string myKeyString;
	string myKeyInt;
	string myKeyFloat;
	string myKeyList;
	string myKeyImage;

	string myValueString;
	int myValueInt;
	float myValueFloat;
	List<int> myValueList = new List<int>();
	Texture2D myValueImage;

	string filePath;

	#endregion

	#region BUTTONS_CALLBACKS
	public void OnButtonPressed(int id){
		switch(id){
		case 0 : //CHOOSE FILE
			string path = EditorUtility.OpenFilePanel("Choose Image", "C:/Dev/Data/Images/", "png,jpg");//, new string[]{".jpg",".png",".bmp"});

			Debug.Log("Path:" + path);

			value_Image.color = Color.white;
			myValueImage = LoadImage(path);
			value_Image.texture = myValueImage;

			float ratio_image = (float)value_Image.texture.width / (float)value_Image.texture.height;
			float ratio_container = value_Image.rectTransform.rect.width / value_Image.rectTransform.rect.height;
			
			if(ratio_image < ratio_container){
				float newWidth = value_Image.rectTransform.rect.height * (float)value_Image.texture.width / (float)value_Image.texture.height;
				value_Image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,newWidth);
			} else{
				float newHeight = value_Image.rectTransform.rect.width * (float)value_Image.texture.height / (float)value_Image.texture.width;				
				value_Image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,newHeight);
			}

			break;

		case 1 : //SAVE INFO
			GetInfo();
			GetValues();

			SaveInfo();

			break;
		case 2 : //LOAD INFO
			LoadInfo();	
			break;
		case 3 : //CHANGE PATH
			filePath = Path.Combine(key_Path.text, key_FileName.text);
			
			text_FilePathInput.text = filePath;
			text_FilePathOutput.text = filePath;
			break;
		default: break;
		}
	}
	#endregion

	#region AUX
	public Texture2D LoadImage(string filePath) {
		
		Texture2D tex = null;
		byte[] fileData;
		
		if (File.Exists(filePath))     {
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}

	void GetInfo(){
		myKeyString = key_String.text;
		myKeyInt = key_Int.text;
		myKeyFloat = key_Float.text;
		myKeyList = key_List.text;
		myKeyImage = key_Image.text;
	}

	void GetValues(){
		myValueString = value_String.text;
		myValueInt = Convert.ToInt32(value_Int.text);
		myValueFloat = (float)Convert.ToDouble(value_Float.text);
		string str = value_List.text;	 

		string[] parts = str.Split(new String[]{","}, StringSplitOptions.RemoveEmptyEntries);

		myValueList.Clear();
		for(int i = 0 ; i < parts.Length ; ++i){
			myValueList.Add(Convert.ToInt32(parts[i]));
		}
	}
	#endregion

	#region SAVE_LOAD
	// Use this for initialization
	public void SaveInfo () {
		StaticSerializer.AddToDictionary(myKeyString	, myValueString);
		StaticSerializer.AddToDictionary(myKeyInt		, myValueInt);
		StaticSerializer.AddToDictionary(myKeyFloat	, myValueFloat);
		StaticSerializer.AddToDictionary(myKeyList	, myValueList);
		StaticSerializer.AddToDictionary(myKeyImage	, myValueImage);
		
		StaticSerializer.SaveDictionary(filePath);	
	}
	
	public void LoadInfo(){
		StaticSerializer.LoadDictionary(filePath);

		string str = "";

		str += "Key String(" + myKeyString + ") - Value(" + (string)StaticSerializer.GetFromDictionary(myKeyString) + ")\n";
		str += "Key Int(" + myKeyInt + ") - Value(" + (int)StaticSerializer.GetFromDictionary(myKeyInt) + ")\n";
		str += "Key Float(" + myKeyFloat + ") - Value(" + (float)StaticSerializer.GetFromDictionary(myKeyFloat) + ")\n";

		List<int> myList = (List<int>)StaticSerializer.GetFromDictionary(myKeyList);
		string l = "";
		for(int i = 0 ; i < myList.Count ; ++i){
			l += myList[i] + ",";
		}
		l = l.Substring(0,l.Length-1);

		str += "Key List(" + myKeyList + ") - Value(" + l + ")\n";
		str += "Key Image(" + myKeyImage + ")\n";

		text_Output.text = str;

		image_Output.color = Color.white;
		image_Output.texture = (Texture2D)StaticSerializer.GetFromDictionary(myKeyImage);

		if(image_Output.texture != null && image_Output.texture.width > 0 && image_Output.texture.height > 0){
			float ratio_image = (float)image_Output.texture.width / (float)image_Output.texture.height;
			float ratio_container = image_Output.rectTransform.rect.width / image_Output.rectTransform.rect.height;
			
			if(ratio_image < ratio_container){
				float newWidth = image_Output.rectTransform.rect.height * (float)image_Output.texture.width / (float)image_Output.texture.height;
				image_Output.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,newWidth);
			} else{
				float newHeight = image_Output.rectTransform.rect.width * (float)image_Output.texture.height / (float)image_Output.texture.width;				
				image_Output.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,newHeight);
			}
		}
	}
	#endregion

	#region UNITY_CALLBACKS
	void Start(){
		key_String.text 	= "KeyString";
		key_Int.text 		= "KeyInt";
		key_Float.text 		= "KeyFloat";
		key_List.text	 	= "KeyList";
		key_Image.text 		= "KeyImage";

		key_Path.text = "C:/Dev/Data/";
		key_FileName.text = "SavedData.dat";

		value_String.text = "Value String 1";
		value_Int.text = "5";
		value_Float.text = "5.6";
		value_List.text = "1,2,3,4,5,6,7,8,9";

		filePath = Path.Combine(key_Path.text, key_FileName.text);

		text_FilePathInput.text = filePath;
		text_FilePathOutput.text = filePath;

		GetInfo();
	}

	#endregion
}
