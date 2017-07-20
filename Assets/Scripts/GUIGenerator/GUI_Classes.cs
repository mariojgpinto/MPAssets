using UnityEngine;
using System;
using System.IO;
using System.Collections;

using MPAssets;

public class ButtonPressedEventArgs : EventArgs {
	public string id { get; set; }
	public string description { get; set; }
}

public class TogglePressedEventArgs : EventArgs {
	public string id { get; set; }
	public string description { get; set; }

	public bool value { get; set; }
}

