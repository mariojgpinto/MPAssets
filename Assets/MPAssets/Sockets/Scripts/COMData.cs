using UnityEngine;
using System.Collections;

public class COMData {
	public enum TYPE{
		TEXT,
		IMAGE,
		AUDIO
	}
	
	public TYPE type;
	
	public const string macroInit = "[INIT]";
	public const string macroEnd = "[END]";
	
	public const string macroSeparator = "|";
}

public class COMData_text : COMData {
	public byte[] data;
	
	public COMData_text(){
		type = TYPE.TEXT;
	}
	
	public COMData_text(byte[] txt){
		type = TYPE.TEXT;
		
		data = txt;
	}

	public COMData_text(string txt){
		type = TYPE.TEXT;
		
		data = System.Text.Encoding.UTF8.GetBytes(txt);
	}
	
	public string GetText(){
		if(data.Length > 0){
			return System.Text.Encoding.UTF8.GetString(data,0,data.Length);
		}
		return null;
	}
}

public class COMData_image : COMData {
	public byte[] data;
	
	public int imageWidth;
	public int imageHeight;
	public int imageFormat;
	
	public int size;
	
	public COMData_image(){
		type = TYPE.IMAGE;
	}
	
	public COMData_image(byte[] image, int width, int height){
		type = TYPE.IMAGE;
		
		data = image;
		imageWidth = width;
		imageHeight = height;
		
		size = data.Length;
	}
}

public class COMData_audio : COMData {
	public byte[] data;
	
	public int size;
	
	public COMData_audio(){
		type = TYPE.AUDIO;
	}
	
	public COMData_audio(byte[] image){
		type = TYPE.AUDIO;
		
		data = image;
		
		size = data.Length;
	}
}
