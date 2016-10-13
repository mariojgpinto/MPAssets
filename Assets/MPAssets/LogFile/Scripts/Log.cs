using UnityEngine;

public class Log : MonoBehaviour {
	#region VARIABLES
	//SINGLETON
	public static Log instance;

	//LOG SETTINGS 	
	public enum LOG_ORDER {
		NEW_TO_OLD,
		OLD_TO_NEW
	};
	public LOG_ORDER logOrder = LOG_ORDER.NEW_TO_OLD;
	LOG_ORDER _logOrder = LOG_ORDER.NEW_TO_OLD;


	//UNITY CONSOLE
	public bool useConsole = true;

	//UNITY UI
	public bool useUI;
	public UnityEngine.UI.Text textLogUI;

	//LOG FILE
	public bool useFile;
	public string log_file = "log.txt";

	//GOOGLE ANALYTICS
	public bool useGoogleAnalytics;


	#endregion

	#region LOG_METHODS
	static string TimeStamp() {
		return "[" + System.DateTime.Now.ToString() + "]";
	}

	static void UpdateLog(string str) {
#if LOG_FILE
		instance.WriteInLog(str);
#endif
#if LOG_ANALYTICS

#endif
#if LOG_UNITY
		AddToDebug(str);
#endif
#if LOG_UI
		instance.WriteToUI(str);
#endif
	}
	#endregion

	#region PUBLIC_METHODS
	//ADD JUST TO CONSOLE LOG
	public static void AddToDebug(string str) {
		Debug.Log(str);
	}

	public static void AddToLog(string _str) {
		string str = TimeStamp() + " " + _str;
		str += "\n";

		UpdateLog(str);
	}

	public static void AddToLog(string _class, string _function, string _message, string _exception = null, string[] args = null) {
		string str = TimeStamp() + "\n";
		str += "\t" + _class;
		str += "\t" + _function;
		str += "\t" + _message;

		if (_exception != null)
			str += "\t" + _exception;

		if (args != null) {
			str += "\tArgs:";
			for (int i = 0; i < args.Length; ++i) {
				str += "\t\t- " + args[i];
			}
		}

		str += "\n";

		UpdateLog(str);
	}

	public static void AddToLog(string[] _str) {
		string str = TimeStamp() + "\n";

		for (int i = 0; i < _str.Length; ++i) {
			str += "\t" + _str[i];
		}

		str += "\n";

		UpdateLog(str);
	}
	#endregion

	#region CONSOLE
	//void AddToConsole(string str) {
	//	Debug.Log(str);
	//}
	#endregion

	#region UI
	void WriteToUI(string str) {
		if (_logOrder == LOG_ORDER.NEW_TO_OLD)
			textLogUI.text = str + "\n" + textLogUI.text;
		else
			textLogUI.text += "\n" + str;
	}
	#endregion

	#region FILE
	void WriteToFile(string str) {
		System.IO.File.AppendAllText(Application.persistentDataPath + "/" + instance.log_file, str);
	}
	#endregion

	#region GOOGLE_ANALYTICS

	#endregion
	
	#region UNITY_CALLBACKS
	void Awake() {
		instance = this;
	}

	void Start() {
		_logOrder = logOrder;
	}
	#endregion
}
