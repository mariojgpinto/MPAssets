using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Log))]
public class LogEditor : Editor {
	private Log _log = null;

	void OnEnable() {
		_log = (Log)target;
	}

	public override void OnInspectorGUI() {
		_log.logOrder = (Log.LOG_ORDER)EditorGUILayout.EnumPopup("Log Order: ", _log.logOrder);

		GUILayout.Space(20);

		if (_log.useConsole = EditorGUILayout.Toggle("Use Unity Console", _log.useConsole)) {
			//GUILayout.BeginHorizontal();

			//GUILayout.EndHorizontal();
		}

		if (_log.useUI = EditorGUILayout.Toggle("Use Unity UI", _log.useUI)) {
			GUILayout.BeginHorizontal();

			GUILayout.Space(20);
			GUILayout.Label("UI.Text Variable", GUILayout.Width(120));
			_log.textLogUI = (UnityEngine.UI.Text)EditorGUILayout.ObjectField((Object)_log.textLogUI, typeof(UnityEngine.UI.Text), true);

			GUILayout.EndHorizontal();
		}

		if (_log.useFile= EditorGUILayout.Toggle("Use Log File", _log.useFile)) {
			GUILayout.BeginHorizontal();

			GUILayout.Space(20);
			GUILayout.Label("Log File: ", GUILayout.Width(120));
			_log.log_file = EditorGUILayout.TextField(_log.log_file);

			GUILayout.EndHorizontal();
		}

		if (_log.useGoogleAnalytics = EditorGUILayout.Toggle("Use Google Analytics", _log.useGoogleAnalytics)) {
			GUILayout.BeginHorizontal();

			GUILayout.Space(20);
			GUILayout.Label("Not Available Yet.");

			GUILayout.EndHorizontal();
		}
		
	}
}
