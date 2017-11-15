// Latest version available at:
// https://bitbucket.org/pschraut/unitycopylightingsettings

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;

static class CopyLightingSettings {
	static SerializedObject s_sourceLightmapSettings;
	static SerializedObject s_sourceRenderSettings;

	[MenuItem("Window/Lighting/Copy Settings", priority = 200)]
	static void CopySettings() {
		UnityEngine.Object lightmapSettings;
		if (!TryGetSettings<LightmapEditorSettings>("GetLightmapSettings", out lightmapSettings))
			return;

		UnityEngine.Object renderSettings;
		if (!TryGetSettings<RenderSettings>("GetRenderSettings", out renderSettings))
			return;

		s_sourceLightmapSettings = new SerializedObject(lightmapSettings);
		s_sourceRenderSettings = new SerializedObject(renderSettings);
	}

	[MenuItem("Window/Lighting/Paste Settings", priority = 201)]
	static void PasteSettings() {
		UnityEngine.Object lightmapSettings;
		if (!TryGetSettings<LightmapEditorSettings>("GetLightmapSettings", out lightmapSettings))
			return;

		UnityEngine.Object renderSettings;
		if (!TryGetSettings<RenderSettings>("GetRenderSettings", out renderSettings))
			return;

		CopyInternal(s_sourceLightmapSettings, new SerializedObject(lightmapSettings));
		CopyInternal(s_sourceRenderSettings, new SerializedObject(renderSettings));

		UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
	}

	[MenuItem("Window/Lighting/Paste Settings", validate = true)]
	static bool PasteValidate() {
		return s_sourceLightmapSettings != null && s_sourceRenderSettings != null;
	}

	static void CopyInternal(SerializedObject source, SerializedObject dest) {
		var prop = source.GetIterator();
		while (prop.Next(true)) {
			var copyProperty = true;
			foreach (var propertyName in new[] { "m_Sun", "m_FileID", "m_PathID", "m_ObjectHideFlags" }) {
				if (string.Equals(prop.name, propertyName, System.StringComparison.Ordinal)) {
					copyProperty = false;
					break;
				}
			}

			if (copyProperty)
				dest.CopyFromSerializedProperty(prop);
		}

		dest.ApplyModifiedProperties();
	}

	static bool TryGetSettings<T>(string methodName, out UnityEngine.Object settings) {
		settings = null;

		var method = typeof(T).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
		if (method == null) {
			Debug.LogErrorFormat("CopyLightingSettings: Could not find {0}.{1}", typeof(T).Name, methodName);
			return false;
		}

		var value = method.Invoke(null, null) as UnityEngine.Object;
		if (value == null) {
			Debug.LogErrorFormat("CopyLightingSettings: Could get data from {0}.{1}", typeof(T).Name, methodName);
			return false;
		}

		settings = value;
		return true;
	}
}
#endif