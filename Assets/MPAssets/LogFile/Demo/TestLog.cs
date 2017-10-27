using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MPAssets;
using System.Diagnostics;

public class TestLog : MonoBehaviour {
	#region VARIABLES

	#endregion

	public void AddAnotherLog() {
		Log.Debug("Another Log _ new");
	}

	public void AddLog5() {
		Log.Debug("Log Level 5_ new");
	}

	public void AddLog4() {
		Log.Debug("Log Level 4_ new");
	}

	public void AddLog3() {
		Log.Debug("Log Level 3_ new");
	}

	public void AddLog2() {
		Log.Debug("Log Level 2_ new");
	}

	public void AddLog1() {
		Log.Debug("Log Level 1_ new");
	}
	
	[Conditional("DEBUG")]
	void Log1 () {
		Log.Debug("Log1");
	}

	IEnumerator Myroutine() {
		yield return new WaitForSeconds(0.5f);
		Log.Debug("Incoming Log: ");
		yield return new WaitForSeconds(0.5f);
		Log1();

		Log.Test("LOGIN", "Test For ", "Login");
		Log.Test("server", "Test For ", "server");
		Log.Test("other", "Test For ", "other");
	}

	#region UNITY_CALLBACKS
	// Use this for initialization
	void Start () {
		//StartCoroutine(Myroutine());
		//Log.Debug("Log Start");

		Invoke("AddAnotherLog", 1);

		Invoke("AddLog5", 2);
		Invoke("AddLog4", 3);
		Invoke("AddLog3", 4);
		Invoke("AddLog2", 5);
		Invoke("AddLog1", 6);
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}
	#endregion
}
