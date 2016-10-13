using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIGenerator_Elem_Base {
	public static int idAc = 0;

	static int namelessCounter = 0;

	public enum TYPE{
		PANEL,
		IMAGE,
		TEXT,
		BUTTON,
		TOGGLE,
		INPUT_FIELD,
		OTHER
	};
	public TYPE type = TYPE.OTHER;

	public GameObject obj;

	public int id = -1;

	public string name = "";
	public string nameLower = "";
	public string nameOrig = "";
	public string nameForChild = "";
	public string variableName = "";
	public string variableName_obj = "";
	public string classType = "";
	public string classTypeFormated = "";

	public string className = "";
	public string classInstanceName = "";
	public string classButtonListenerFunction = "";
	public string classToggleListenerFunction = "";


	public GUIGenerator_Elem_Base parent = null;
	public List<GUIGenerator_Elem_Base> children = new List<GUIGenerator_Elem_Base>();

	public void GenerateInfo(GameObject mainObj){
		this.id = idAc++;
		this.obj = mainObj;
		this.nameOrig = this.obj.name;
		
		if(this.nameOrig.StartsWith(GUIGenerator_Macros.elem_panel)){
			this.name += this.nameOrig.Substring(GUIGenerator_Macros.elem_panel.Length);

			this.type = GUIGenerator_Elem_Base.TYPE.PANEL;
			this.classType = GUIGenerator_Macros.type_image;
			this.classTypeFormated = GUIGenerator_Macros.typeFormated_image;
		} else
		if(this.nameOrig.StartsWith(GUIGenerator_Macros.elem_image)){
			this.name += this.nameOrig.Substring(GUIGenerator_Macros.elem_image.Length);

			this.type = GUIGenerator_Elem_Base.TYPE.IMAGE;
			this.classType = GUIGenerator_Macros.type_image;
			this.classTypeFormated = GUIGenerator_Macros.typeFormated_image;
		} else
		if(this.nameOrig.StartsWith(GUIGenerator_Macros.elem_text)){
			this.name += this.nameOrig.Substring(GUIGenerator_Macros.elem_text.Length);

			this.type = GUIGenerator_Elem_Base.TYPE.TEXT;
			this.classType = GUIGenerator_Macros.type_text;
			this.classTypeFormated = GUIGenerator_Macros.typeFormated_text;
		} else
		if(this.nameOrig.StartsWith(GUIGenerator_Macros.elem_button)){
			this.name += this.nameOrig.Substring(GUIGenerator_Macros.elem_button.Length);

			this.type = GUIGenerator_Elem_Base.TYPE.BUTTON;
			this.classType = GUIGenerator_Macros.type_button;
			this.classTypeFormated = GUIGenerator_Macros.typeFormated_button;
		} else
		if(this.nameOrig.StartsWith(GUIGenerator_Macros.elem_toggle)){
			this.name += this.nameOrig.Substring(GUIGenerator_Macros.elem_toggle.Length);
			
			this.type = GUIGenerator_Elem_Base.TYPE.TOGGLE;
			this.classType = GUIGenerator_Macros.type_toggle;
			this.classTypeFormated = GUIGenerator_Macros.typeFormated_toggle;
		} else
		if(this.nameOrig.StartsWith(GUIGenerator_Macros.elem_input)){
			this.name += this.nameOrig.Substring(GUIGenerator_Macros.elem_input.Length);
			
			this.type = GUIGenerator_Elem_Base.TYPE.INPUT_FIELD;
			this.classType = GUIGenerator_Macros.type_input;
			this.classTypeFormated = GUIGenerator_Macros.typeFormated_input;
		} else{
		//if(this.nameOrig.StartsWith(GUIGenerator_Macros.elem_other)){
			this.name = this.nameOrig;

			this.type = GUIGenerator_Elem_Base.TYPE.OTHER;
			this.classType = GUIGenerator_Macros.type_other;
			this.classTypeFormated = GUIGenerator_Macros.typeFormated_other;
		}

		UpdateVariableNames();
	}

	public void UpdateVariableNames(){
		this.nameLower = this.name.Substring(0, 1).ToLower();
		this.nameLower += this.name.Substring(1,this.name.Length-1);

		string parentName = "";//((parent != null) ? parent.nameForChild : "");
		string name = "";

		if(parent != null){
			if(parent.parent != null){
				parentName = parent.nameForChild;
			}
			else{ //GUI Parent -> Class
				parentName = "";
			}
		}

//		if(this.type == TYPE.OTHER){
//			name = type.ToString().Substring(0,1).ToLower() + ++namelessCounter + "_" + this.nameLower;
//		}
//		else{
//			name = this.nameLower;
//		}
		name = this.nameLower;

		this.variableName = ""; // (parentName == "") ? name : parentName + "_" + name;

		if(parentName.Length == 0){
			this.variableName = name;
		}
		else{
			this.variableName = parentName + "_" + name;
		}
		this.nameForChild = this.variableName;

		switch(type){
			case TYPE.PANEL:
				this.variableName += GUIGenerator_Macros.sub_panel;
				break;
			case TYPE.IMAGE:
				this.variableName += GUIGenerator_Macros.sub_image;
				break;
			case TYPE.TEXT:
				this.variableName += GUIGenerator_Macros.sub_text;
				break;
			case TYPE.BUTTON:
				this.variableName += GUIGenerator_Macros.sub_button;
				break;
			case TYPE.TOGGLE:
				this.variableName += GUIGenerator_Macros.sub_toggle;
				break;
			case TYPE.INPUT_FIELD:
				this.variableName += GUIGenerator_Macros.sub_input;
				break;
			case TYPE.OTHER:
			default:
				this.variableName += GUIGenerator_Macros.sub_other + "_" + ++namelessCounter;
				break;
		}
		this.variableName_obj = this.variableName + GUIGenerator_Macros.sub_gameObject;
	}

	public void PrintElement(ref string str, int level){
		str += "------------------------------------------------------------------------------------------\n";
		for (int i = 0; i < level; ++i) str += ("\t");

		str += "ID: " + id + "\n";
		
		for (int i = 0; i < level; ++i) str += ("\t");

		str += "Name Orig: " + nameOrig + "\n";

		for (int i = 0; i < level; ++i) str += ("\t");

		str += "Name: " + name + "\n";

		for (int i = 0; i < level; ++i) str += ("\t");

		str += "NameLower: " + nameLower + "\n";
		
		for (int i = 0; i < level; ++i) str += ("\t");

		str += "Type: " + type.ToString() + "\n";
		
		for (int i = 0; i < level; ++i) str += ("\t");
		
		str += "Variable: " + variableName + "\n";

		for (int i = 0; i < level; ++i) str += ("\t");

		str += "Variable_obj: " + variableName_obj + "\n";
		
		for (int i = 0; i < level; ++i) str += ("\t");

		str += "className: " + className + "\n";
		
		for (int i = 0; i < level; ++i) str += ("\t");

		str += "classInstanceName: " + classInstanceName + "\n";
		
		for (int i = 0; i < level; ++i) str += ("\t");

		str += "classListenerFunction: " + classButtonListenerFunction + "\n";

		for (int i = 0; i < level; ++i) str += ("\t");
		
		str += "classToggleListenerFunction: " + classToggleListenerFunction + "\n";
		
		for (int i = 0; i < level; ++i) str += ("\t");
		
		try{str += "Parent: " + parent.variableName + "\n";
		}
		catch{}

		for( int i = 0 ; i < this.children.Count ; ++i){
			this.children[i].PrintElement(ref str, level +1);
		} 
	}

	public bool HasButtons(){
		return HasButtonsRecursive(this);
	}

	bool HasButtonsRecursive(GUIGenerator_Elem_Base elem){
		if(elem.type == TYPE.BUTTON)
			return true;

		if(elem.children.Count > 0){
			for(int i = 0 ; i < elem.children.Count ; ++i){
				if(HasButtonsRecursive(elem.children[i])){
					return true;
				}
			}
			return false;
		}
		else
			return false;
	}


	public bool HasToggle(){
		return HasToggleRecursive(this);
	}
	
	bool HasToggleRecursive(GUIGenerator_Elem_Base elem){
		if(elem.type == TYPE.TOGGLE)
			return true;
		
		if(elem.children.Count > 0){
			for(int i = 0 ; i < elem.children.Count ; ++i){
				if(HasToggleRecursive(elem.children[i])){
					return true;
				}
			}
			return false;
		}
		else
			return false;
	}
}
