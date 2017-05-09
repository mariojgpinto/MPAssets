using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MPAssets;

public class CustomClient : MonoBehaviour {
	#region VARIABLES
	public UnityEngine.UI.Text textUI;
	float waitTime = 2f;
	SocketClient client;

	public static bool first = true;
	// Use this for initialization

	int number = 0;
	#endregion

	#region COM_CALLBACKS
	private void Client_OnConnect(object sender, SocketArgs e) {
		//Debug.Log("Client_OnConnect" + e != null ? ": " + e.message : "");
		Debug.Log("Client_OnConnect");
	}

	private void Client_OnClose(object sender, SocketArgs e) {
		//Debug.Log("Client_OnClose" + e != null ? ": " + e.message: "");
	}

	private void Client_OnReceiveFailed(object sender, SocketArgs e) {
		//Debug.Log("Client_OnReceiveFailed" + e != null ? ": " + e.message : "");
	}

	private void Client_OnSendFailed(object sender, SocketArgs e) {
		//Debug.Log("Client_OnSendFailed" + e != null ? ": " + e.message : "");
	}

	private void Client_OnSendSucess(object sender, SocketArgs e) {
		//Debug.Log("Client_OnSendSucess" + e != null ? ": " + e.message : "");
	}

	private void Client_OnReceive(object sender, SocketArgs e) {
		//Debug.Log("Client_OnReceive" + e != null ? ": " + e.message : "");
		Debug.Log("Client_OnReceive: ");

		COMData data = client.infoReceived.Dequeue();

		if (data.type == COMData.TYPE.TEXT) {
			COMData_text text = (COMData_text)data;

			ProcessMessage(text);
			//Log.AddToLog("Message Received: " + text.GetText());

			//string txt = text.GetText();
			//Log.AddToLog(txt);
			////textUI.text = txt;


			//number = System.Convert.ToInt32(txt);

			////client.SendInfo_text("" + (number + 1));
			//StartCoroutine(SendMessageDelayed("" + (number + 1), waitTime));
		}
	}

	private void Client_OnConnectionLost(object sender, SocketArgs e) {
		//Debug.Log("Client_OnConnectionLost" + e != null ? ": " + e.message : "");
	}
	#endregion

	#region COM_SEND
	void SendMessage_Text(string txt) {
		client.SendInfo_text(txt);
	}

	IEnumerator SendMessageDelayed(string txt, float delay) {
		yield return new WaitForSeconds(delay);
		
		client.SendInfo_text(txt);
	}
	#endregion

	#region COM_RECEIVE
	void ProcessMessage(COMData_text msg) {
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

		StartCoroutine(SendMessageDelayed("" + (count + 1), waitTime));
	}
	#endregion

	#region UNITY_CALLBACKS
	void Start () {
		Log.AddToLog("Init Client");
		client = new SocketClient("192.168.1.7");//

		//client.OnConnect += Client_OnConnect;
		//client.OnConnectionLost += Client_OnConnectionLost;
		//client.OnReceive += Client_OnReceive;
		//client.OnReceiveFailed += Client_OnReceiveFailed;
		//client.OnSendSucess += Client_OnSendSucess;
		//client.OnSendFailed += Client_OnSendFailed;
		//client.OnClose += Client_OnClose;

		client.TryToConnect();

	}

	// Update is called once per frame
	void Update() {
		if (first) {
			if (client.isConnected) {
				StartCoroutine(SendMessageDelayed("" + number, waitTime));

				first = false;
			}
		}
		else {
			if (client.infoReceived.Count > 0) {
				COMData data = client.infoReceived.Dequeue();

				if (data.type == COMData.TYPE.TEXT) {
					COMData_text text = (COMData_text)data;

					ProcessMessage(text);
				}
			}
		}
	}
	#endregion

	void OnApplicationQuit() {
		Debug.Log("OnApplicationQuit");
		client.Close();
		Debug.Log("OnApplicationQuit agora a serio");
	}
}
