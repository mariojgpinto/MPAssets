using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MPAssets;

public class TestTimers : MonoBehaviour {
	#region VARIABLES
	Timer timer = new Timer();
	StopWatch watch = new StopWatch();
	#endregion

	#region UNITY_CALLBACKS
	// Use this for initialization
	void Start () {
		timer.timeout = 60;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (timer.Status() == Timer.STATUS.RUNNING) {
				timer.Pause();
				watch.Pause();
			}
			else {
				timer.Run();
				watch.Run();
			}
		}


		if (Input.GetKeyDown(KeyCode.Escape)) {
			timer.Stop();
			watch.Stop();
		}
	}

	private void OnGUI() {
		GUI.TextField(
			new Rect(0,0,Screen.width / 2f, 90),
			"Timer:\n" +
			"\n  Timout: " + timer.timeout + 
			"\n  Status: " + timer.Status().ToString() +
			"\n  Time: " + timer.CheckTime()
		);

		GUI.TextField(
			new Rect(Screen.width / 2f, 0, Screen.width / 2f, 90),
			"StopWatch:" +
			"\n  Status: " + watch.Status().ToString() + 
			"\n  Time: " + watch.CheckTime()
		);
	}
	#endregion
}
