using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace MPAssets {
	public class MyVariable {
		public string type;
		public string name;
		public string defaultValue;

		public MyVariable(string _type, string _name) {
			this.type = _type;
			this.name = _name;
		}

		public MyVariable(string _type, string _name, string _defaultValue) {
			this.type = _type;
			this.name = _name;
			this.defaultValue = _defaultValue;
		}
	}

	public class MyJSON : MonoBehaviour {
		#region VARIABLE_GENERATION
		public static string GenerateVariable(string type, string name, string defaultValue = null) {
			string str = "";

			str += "\t[SerializeField]\n";
			str += "\t" + type + " " + name.ToUpper() + (defaultValue != null ? " = " + defaultValue : "") + ";\n";
			str += "\tpublic static " + type + " " + name + "{\n";
			str += "\t\tset { instance." + name.ToUpper() + " = value; }\n";
			str += "\t\tget { return instance." + name.ToUpper() + "; }\n";
			str += "\t}\n";

			return str;
		}
		#endregion

		#region JSON_SAVE_LOAD
		public static T LoadInfo<T>(string path) {
			if (System.IO.File.Exists(path)) {
				try {
					T data = JsonUtility.FromJson<T>(System.IO.File.ReadAllText(path));
					return data;
				}
				catch (System.Exception E) {
					Debug.Log(E.ToString());

					return default(T);
				}
			}
			else {
				Debug.Log("File does not exist: " + path);
			}

			return default(T);
		}

		public static void SaveInfo<T>(T data, string path) {
			//VALIDATE PATH?
			string str = MyJSON.FormatJson(JsonUtility.ToJson(data));

			System.IO.File.WriteAllText(path, str);
		}
		#endregion

		#region JSON_FORMAT
		private const string INDENT_STRING = "\t";
		public static string FormatJson(string str) {
			var indent = 0;
			var quoted = false;
			var sb = new System.Text.StringBuilder();
			for (var i = 0; i < str.Length; i++) {
				var ch = str[i];
				switch (ch) {
					case '{':
					case '[':
						sb.Append(ch);
						if (!quoted) {
							sb.AppendLine();
							System.Linq.Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
						}
						break;
					case '}':
					case ']':
						if (!quoted) {
							sb.AppendLine();
							System.Linq.Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
						}
						sb.Append(ch);
						break;
					case '"':
						sb.Append(ch);
						bool escaped = false;
						var index = i;
						while (index > 0 && str[--index] == '\\')
							escaped = !escaped;
						if (!escaped)
							quoted = !quoted;
						break;
					case ',':
						sb.Append(ch);
						if (!quoted) {
							sb.AppendLine();
							System.Linq.Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
						}
						break;
					case ':':
						sb.Append(ch);
						if (!quoted)
							sb.Append(" ");
						break;
					default:
						sb.Append(ch);
						break;
				}
			}
			return sb.ToString();
		}
		#endregion
	}

	static class Extensions {
		public static void ForEach<T>(this IEnumerable<T> ie, System.Action<T> action) {
			foreach (var i in ie) {
				action(i);
			}
		}
	}
}