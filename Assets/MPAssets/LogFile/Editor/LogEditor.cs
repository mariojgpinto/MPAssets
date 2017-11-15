using UnityEditor;
using UnityEngine;
using System.Collections;

using MPAssets;
using System.Collections.Generic;

[CustomEditor(typeof(Log))]
public class LogEditor : Editor {
	private Log _log = null;

	void OnEnable() {
		_log = (Log)target;

	}

	public override void OnInspectorGUI() {
		_log.logOrder = (Log.LOG_ORDER)EditorGUILayout.EnumPopup("Log Order: ", _log.logOrder);

		GUILayout.Space(20);

		ConsoleInfo();

		UIInfo();

		FileInfo();

		ServerInfo();

		GoogleAnalyticsInfo();

		EditorUtility.SetDirty(_log);
	}

	void ConsoleInfo() {
		GUILayout.BeginHorizontal();
		GUILayout.Label("Use Unity Console", GUILayout.Width(140));
		_log.useConsole = EditorGUILayout.Toggle(_log.useConsole);
		GUILayout.EndHorizontal();
		if (_log.useConsole) {
			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("Priority Level: ", GUILayout.Width(120));
			Log.instance.logPriorityConsole = (Log.LOG_PRIORITY)EditorGUILayout.EnumPopup(Log.instance.logPriorityConsole);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("Test Labels", GUILayout.Width(120));
			Log.instance.customTagsRaw = EditorGUILayout.TextField(Log.instance.customTagsRaw);
			GUILayout.EndHorizontal();
		}
	}

	void UIInfo() {
		GUILayout.BeginHorizontal();
		GUILayout.Label("Use Unity UI", GUILayout.Width(140));
		_log.useUI = EditorGUILayout.Toggle(_log.useUI);
		GUILayout.EndHorizontal();
		if (_log.useUI) {
			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("Priority Level: ", GUILayout.Width(120));
			Log.instance.logPriorityUI = (Log.LOG_PRIORITY)EditorGUILayout.EnumPopup(Log.instance.logPriorityUI);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("UI.Text Variable", GUILayout.Width(120));
			_log.textLogUI = (UnityEngine.UI.Text)EditorGUILayout.ObjectField((Object)_log.textLogUI, typeof(UnityEngine.UI.Text), true);
			GUILayout.EndHorizontal();
		}
	}

	void FileInfo() {
		GUILayout.BeginHorizontal();
		GUILayout.Label("Use Log File", GUILayout.Width(140));
		_log.useFile = EditorGUILayout.Toggle(_log.useFile);
		GUILayout.EndHorizontal();
		if (_log.useFile) {
			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("Priority Level: ", GUILayout.Width(120));
			Log.instance.logPriorityFile = (Log.LOG_PRIORITY)EditorGUILayout.EnumPopup(Log.instance.logPriorityFile);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUIContent content = new GUIContent("Persitent Data Path:", "Use PersitentDataPath as parent folder");
			EditorGUILayout.LabelField(content, GUILayout.Width(120));
			_log.usePersitentDataPath = EditorGUILayout.Toggle(_log.usePersitentDataPath);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("Log File: ", GUILayout.Width(120));
			_log.log_file = EditorGUILayout.TextField(_log.log_file);
			GUILayout.EndHorizontal();
		}
	}

	void ServerInfo() {
		GUILayout.BeginHorizontal();
		GUILayout.Label("Use Server Log", GUILayout.Width(140));
		_log.useServer = EditorGUILayout.Toggle(_log.useServer);
		GUILayout.EndHorizontal();
		if (_log.useServer) {
			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("Priority Level: ", GUILayout.Width(120));
			Log.instance.logPriorityServer = (Log.LOG_PRIORITY)EditorGUILayout.EnumPopup(Log.instance.logPriorityServer);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("Request Url: ", GUILayout.Width(120));
			_log.request_url = EditorGUILayout.TextField(_log.request_url);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("Pending File: ", GUILayout.Width(120));
			_log.pending_file = EditorGUILayout.TextField(_log.pending_file);
			GUILayout.EndHorizontal();
		}
	}

	void GoogleAnalyticsInfo() {
		GUILayout.BeginHorizontal();
		GUILayout.Label("Use Google Analytics", GUILayout.Width(140));
		_log.useGoogleAnalytics = EditorGUILayout.Toggle(_log.useGoogleAnalytics);
		GUILayout.EndHorizontal();
		if (_log.useGoogleAnalytics) {
			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			GUILayout.Label("Not Available Yet.");
			GUILayout.EndHorizontal();
		}
	}
}
