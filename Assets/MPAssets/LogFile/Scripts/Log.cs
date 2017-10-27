using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace MPAssets {
	public class LogEntry {
		DateTime timestamp;
		string message;
	}

	[Prefab("Log", true)]
	public class Log : Singleton<Log> {
		#region VARIABLES
		//LOG PRIORITIES 	
		public enum LOG_PRIORITY {
			ALL		= 0,
			TEST	= ALL + 1,
			DEBUG	= TEST + 1,
			INFO	= DEBUG + 1, 
			WARNING	= INFO + 1,
			ERROR	= WARNING + 1,
			FATAL	= ERROR + 1,
			NONE	
		};
		
		//TAGS
		public string customTagsRaw;
		public List<string> customTags = new List<string>();

		//LOG SETTINGS 	
		public enum LOG_ORDER {
			NEW_TO_OLD,
			OLD_TO_NEW
		};
		public LOG_ORDER logOrder = LOG_ORDER.NEW_TO_OLD;
		LOG_ORDER _logOrder = LOG_ORDER.NEW_TO_OLD;


		string str_log = "";
		//UNITY CONSOLE
		public bool useConsole = true;
		public LOG_PRIORITY logPriorityConsole = LOG_PRIORITY.ALL;

		//UNITY UI
		public bool useUI;
		public LOG_PRIORITY logPriorityUI = LOG_PRIORITY.INFO;
		public UnityEngine.UI.Text textLogUI;
		bool updateUI = false;

		//LOG FILE
		public bool useFile;
		public LOG_PRIORITY logPriorityFile = LOG_PRIORITY.INFO;
		public string log_file = "log.txt";

		//LOG HTTP SERVER
		public bool useServer;
		public LOG_PRIORITY logPriorityServer = LOG_PRIORITY.ERROR;

		public string request_url;
		public string pending_file = "server_pending_file.txt";
		string pending_file_path;
		string pending_file_directory;
		int pending_file_timout = 10;
		string log_file_directory;
		

		//GOOGLE ANALYTICS
		public bool useGoogleAnalytics;
		#endregion

		#region LOG_METHODS
		static bool ContainsTag(string tag) {
			foreach(string t in instance.customTags) {
				if (tag.ToUpper() == t.ToUpper())
					return true;
			}
			return false;
		}

		static string TimeStamp() {
			return "[" + System.DateTime.UtcNow.ToString() + "]";
		}

		static string TransformToString(params string[] content) {
			string str = "";
			if (content.Length == 1) {
				str = TimeStamp() + " " + content[0];
			}
			else {
				str = TimeStamp() + "\n";
				for (int i = 0; i < content.Length; ++i) {
					str += "\t" + content[i];
				}
			}
			return str;
		}

		static void UpdateLog(string str, LOG_PRIORITY priority = LOG_PRIORITY.DEBUG) {
			if (instance.useConsole) {
				if((int)instance.logPriorityConsole <= (int)priority)
				_Debug(str);
			}

			if (instance.useUI) {
				instance.WriteToUI(str);
			}

			if (instance.useFile) {
				instance.WriteToFile(str);
			}

			if (instance.useServer) {
				instance.SendServerLog(str);
			}

			if (instance.useGoogleAnalytics) {
				//AddDebug(str);
			}
		}
		#endregion

		#region PUBLIC_METHODS
		[System.Diagnostics.Conditional("DEBUG")]
		public static void Test(string tag = "", params string[] content) {
			if (tag != "" && !ContainsTag(tag))
				return;

			Debug(content);
		}

		//ADD JUST TO CONSOLE LOG
		[System.Diagnostics.Conditional("DEBUG")]
		public static void Debug(params string[] content) {
			UnityEngine.Debug.Log(TransformToString(content));
		}
		//ADD JUST TO CONSOLE LOG
		static void _Debug(string content) {
			UnityEngine.Debug.Log(content);
		}

		public static void Info(params string[] content) {
			string str = TransformToString(content);

			_Debug(str);
			UpdateLog(str, LOG_PRIORITY.INFO);
		}

		public static void Warning(params string[] content) {
			string str = TransformToString(content);

			_Debug(str);
			UpdateLog(str, LOG_PRIORITY.WARNING);
		}

		public static void Error(params string[] content) {
			string str = TransformToString(content);

			_Debug(str);
			UpdateLog(str, LOG_PRIORITY.ERROR);
		}

		public static void Fatal(params string[] content) {
			string str = TransformToString(content);

			_Debug(str);
			UpdateLog(str, LOG_PRIORITY.FATAL);
		}













		[System.Obsolete("AddToDebug is deprecated, please use Debug instead.")]
		public static void AddToDebug(string str) {
			UnityEngine.Debug.Log(str);
		}

		[System.Obsolete("AddToLog is deprecated, please use Debug, Info, Warning, Error or Fatal instead.")]
		public static void AddToLog(params string[] _str) {
			string str = "";
			if (_str.Length == 1) {
				str = TimeStamp() + " " + _str[0];
				//str += "\n";
			}
			else {
				str = TimeStamp() + "\n";

				for (int i = 0; i < _str.Length; ++i) {
					str += "\t" + _str[i];
				}
			}
			UpdateLog(str);
		}

		[System.Obsolete("AddToLog is deprecated, please use Debug, Info, Warning, Error or Fatal instead.")]
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
		#endregion

		#region CONSOLE
		//void AddToConsole(string str) {
		//	Debug.Log(str);
		//}
		#endregion

		#region UI
		void WriteToUI(string str) {
			if (_logOrder == LOG_ORDER.NEW_TO_OLD) {
				//textLogUI.text = str + "\n" + textLogUI.text;
				str_log = str + "\n" + str_log;
			}
			else {
				//textLogUI.text += "\n" + str;
				str_log += "\n" + str;
			}

			updateUI = true;
		}
		#endregion

		#region FILE
		void WriteToFile(string str) {
			System.IO.File.AppendAllText(Application.persistentDataPath + "/" + instance.log_file, str + "\n");
		}
		#endregion

		#region SERVER
		void SendServerLog(string str) {
			string filePath = log_file_directory + GetUniqueLogID() + ".txt";
			File.WriteAllText(filePath, str);
		}

		IEnumerator ServerRoutine() {
			int ac = 0;
			float lastTime = -pending_file_timout * 2;
			bool pendingFileFlag = false;
			while (true) {
				string[] files;
				if (Time.realtimeSinceStartup - lastTime > pending_file_timout) {
					files = Directory.GetFiles(pending_file_directory);
					lastTime = Time.realtimeSinceStartup;
					pendingFileFlag = true;
				}
				else {
					files = Directory.GetFiles(log_file_directory);
					pendingFileFlag = false;
				}

				if(files.Length == 0) {
					if (ac < 5) ac++;
					yield return new WaitForSeconds(1 * ac);
				}
				else {
					foreach(string file in files) {
						string str = File.ReadAllText(file);

						bool requestErrorOccurred = false;
						
						var request = new UnityWebRequest(instance.request_url, "POST");
						//request.timeout = 2;
						string jsonString = "{\"logMessage\":\"" + str.Replace("\n", "\\n") + "\"}";
						byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonString);
						request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
						request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
						request.SetRequestHeader("Content-Type", "application/json");

						yield return request.Send();

						if (request.isError) {
							requestErrorOccurred = true;
						}
						else {
							if (request.responseCode == 200) {
								File.Delete(file);
							}
							else {
								requestErrorOccurred = true;
							}
						}

						if (requestErrorOccurred) {
							//Debug.Log("Error at: " + file);
							if (!pendingFileFlag) {
								File.AppendAllText(pending_file_path, str);
								File.Delete(file);
							}
						}
					}
				}
			}
		}

		static long idCounter = 0;
		public static string GetUniqueLogID() {
			return "log_" + DateTime.UtcNow.ToFileTimeUtc() + "_" + idCounter++ + "_" + UnityEngine.Random.Range(0, int.MaxValue);
		}
		#endregion

		#region GOOGLE_ANALYTICS

		#endregion

		#region UNITY_CALLBACKS
		protected override void Awake() {
			base.Awake();

			if (textLogUI != null)
				textLogUI.text = "";

			Log.instance.customTags = new List<string>(
				customTagsRaw.Split(
					new string[] { ";" }, 
					System.StringSplitOptions.RemoveEmptyEntries));

			string str = "Custom Tags [" + customTagsRaw + "]\n";
			foreach(string t in Log.instance.customTags) {
				str += " - " + t + "\n";
			}
			UnityEngine.Debug.Log(str);
		}

		void Start() {
			_logOrder = logOrder;
			str_log = "";

			if (useServer) {
				log_file_directory = Application.persistentDataPath + "/ServerLog/";
				if (!Directory.Exists(log_file_directory))
					Directory.CreateDirectory(log_file_directory);

				pending_file_directory = Application.persistentDataPath + "/ServerLog/Pending/";
				if (!Directory.Exists(pending_file_directory))
					Directory.CreateDirectory(pending_file_directory);

				pending_file_path = pending_file_directory + instance.pending_file;

				UnityEngine.Debug.Log("Pending File Path: " + pending_file_path);

				StartCoroutine(ServerRoutine());
			}
		}

		void FixedUpdate() {
			if (updateUI) {
				textLogUI.text = str_log;
			}
		}
		#endregion
	}
}