using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MPAssets;

public class CustomServer : MonoBehaviour {
	#region VARIABLES
	public UnityEngine.UI.Text textUI;
	float waitTime = 2f;

	SocketServer server = null;
	#endregion

	#region COM_SEND
	void SendMessage(string txt, int id = -1) {
		if (id == -1)
			server.BroadcastMessage(txt);
		else
			server.SendMessage(id, txt);
	}

	IEnumerator SendMessageDelayed(string txt, float delay, int id = -1) {
		yield return new WaitForSeconds(delay);

		if (id == -1)
			server.BroadcastMessage(txt);
		else
			server.SendMessage(id, txt);
	}
	#endregion

	#region COM_RECEIVE
	void ProcessMessage(COMData_text msg, int from) {
		string txt = msg.GetText();

		//ANALYSE MESSAGE
		textUI.text = txt;

		Log.AddToLog(txt);
		int count = 0;
		try {
			count = System.Convert.ToInt32(txt);
		}
		catch (System.Exception) {
			Debug.Log("ERROR ON MESSAGE - " + txt);
		}


		StartCoroutine(SendMessageDelayed("" + (count + 1), waitTime, from));
	}
	#endregion
	
	#region UNITY_CALLBACKS
	// Use this for initialization
	void Start () {
		Log.AddToLog("Init Server");
		server = new SocketServer();
		server.StartServer("192.168.1.7");
	}
	
	// Update is called once per frame
	void Update () {
		if (server.infoReceived.Count > 0) {
			KeyValuePair<int, COMData> data = server.infoReceived.Dequeue();

			if (data.Value.type == COMData.TYPE.TEXT) {
				COMData_text text = (COMData_text)data.Value;

				ProcessMessage(text, data.Key);
			}
		}
	}

	void OnApplicationQuit() {
		//Debug.Log("ApplicationQuit");
		if (server != null) {
			server.Close();
		}
		//Debug.Log("ApplicationQuit");
	}
	#endregion
}
