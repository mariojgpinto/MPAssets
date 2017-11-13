using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MPAssets;

namespace MPAssets {
	[Prefab("InputManager", true)]
	public class InputManager : Singleton<InputManager> {
		#region SWIPE
		public enum SWIPES {
			UP,
			RIGHT,
			DOWN,
			LEFT,
			NONE
		}
		public static SWIPES swipe = SWIPES.NONE;//public
		#endregion

		#region VARIABLES
		public static float clickTime = .5f;
		public static float doubleClickTime = .3f;
		public static float longClickTime = 1f;
		public static Vector3 position {
			get { return Input.mousePosition; }
		}
		public static Vector3 lastPosition;
		public static Vector3 deltaPosition {
			get { return position - lastPosition; }
		}
		public static float lastClickTime;
		public static bool hasTouch = false;

		public static float idleClear = 1f;

		static Vector3 startPosition;
		float startTime;
		bool flagStill = true;
		float _doubleTapTimeD;
		float diffThreshold = 5;
		float diffThresholdSwipe = 50;
		#endregion

		#region GETS
		static bool _click = false;
		/// <summary>
		/// Checks if there has been a mouse click and consumes it.
		/// </summary>
		public static bool click {
			get {
				if (_click) {
					_click = false;
					return true;
				}
				return false;
			}
		}

		static bool _longClick;
		public static bool longClick {
			get {
				if (_longClick) {
					_longClick = false;
					return true;
				}
				return false;
			}
		}

		public static bool drag;
		//public st
		public static bool dragHorizontal;
		public static bool dragVertical;
		//public static bool drag {
		//	get {
		//		return _drag;
		//	}
		//}

		static bool _doubleClick = false;
		public static bool doubleClick {
			get {
				if (_doubleClick) {
					_doubleClick = false;
					return true;
				}
				return false;
			}
		}
		#endregion

		#region UPDATE
		//bool saveLast = true;
		void UpdateLastReading() {
			lastClickTime = Time.realtimeSinceStartup;
			//if(saveLast)
			lastPosition = Input.mousePosition;
			//saveLast = !saveLast;
		}
		#endregion

		#region UNITY_CALLBACKS
		// Use this for initialization
		void Start() {
			UpdateLastReading();
		}

		// Update is called once per frame
		void Update() {
			if (Input.GetMouseButtonDown(0)) {
				startPosition = Input.mousePosition;
				startTime = Time.realtimeSinceStartup;
				flagStill = true;
				hasTouch = true;

				if (Time.time < _doubleTapTimeD + doubleClickTime) {
					_doubleClick = true;
				}
				_doubleTapTimeD = Time.time;

				UpdateLastReading();
			}
			else
			if (Input.GetMouseButtonUp(0)) {
				if (flagStill) {
					float diffTime = Time.realtimeSinceStartup - startTime;

					if (diffTime < clickTime) {
						flagStill = false;
						_longClick = true;

						_click = true;
					}
				}

				_longClick = false;
				drag = false;
				swipe = SWIPES.NONE;
				dragHorizontal = false;
				dragVertical = false;
				UpdateLastReading();
			}
			else
			if (Input.GetMouseButton(0)) {
				float diff = Vector2.Distance(startPosition, Input.mousePosition);

				if (diff > diffThresholdSwipe) {
					if (swipe == SWIPES.NONE) {
						Vector2 delta = Input.mousePosition - startPosition;
						if (Mathf.Abs(delta.y) < Mathf.Abs(delta.x)) {
							swipe = delta.x > 0 ? SWIPES.RIGHT : SWIPES.LEFT;
						}
						else {
							swipe = delta.y > 0 ? SWIPES.UP : SWIPES.DOWN;
						}
					}
				}
				else {

				}

				if (diff > diffThreshold) {
					//MOVE
					flagStill = false;
					drag = true;

					if (!dragHorizontal && !dragVertical) {
						Vector2 delta = Input.mousePosition - startPosition;
						if (Mathf.Abs(delta.y) < diffThreshold/* / 1.5f*/) {
							dragHorizontal = true;
						}
						else {
							dragVertical = true;
						}
					}
				}
				else {
					//STILL
					if (flagStill) {
						float diffTime = Time.realtimeSinceStartup - startTime;

						if (diffTime > longClickTime) {
							flagStill = false;
							_longClick = true;
						}
					}
				}

				UpdateLastReading();
			}
			else {
				hasTouch = false;
				//print("Time: " + (Time.realtimeSinceStartup - lastTouchTime));
				if ((Time.realtimeSinceStartup - lastClickTime) > idleClear) {
					//Debug.Log("INACTIVE TIMEOUT");
					_click = false;
					_doubleClick = false;
					_longClick = false;
				}
			}
		}

		//void OnGUI() {
		////	if (Controller.debug) {
		//		GUI.TextField(new Rect(Screen.width - 210, /*Screen.height - 180*/ 0, 210, 130), "Position: " + position + "\n"
		//																	+ "Time: " + (Time.realtimeSinceStartup - startTime) + "\n"
		//																	+ "Click: " + _click + "\n"
		//																	+ "DoubleClick: " + _doubleClick + "\n"
		//																	+ "LongClick: " + _longClick + "\n"
		//																	+ "Drag: " + drag + " - " + (drag ? "" + (position - startPosition) : "") + "\n"
		//																	+ "Drag Type: " + ((dragVertical) ? "Vertical" : (dragHorizontal) ? "Horizontal" : "") + "\n"
		//																	+ "Swipe: " + swipe.ToString() + "\n"
		//																	//"Inactive: " + _inactive + " - " + (Time.realtimeSinceStartup - lastTouchTime)
		//																	);
		////	}
		//}
		#endregion
	}
}