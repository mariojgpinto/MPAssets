using System;
using System.Collections.Generic;
using UnityEngine;

namespace MPAssets {
	public class WebCamControllerEventArgs : EventArgs {
		public string webCamName;
		public int webCamIndex;
		public int webCamWidth;
		public int webCamHeight;
	}

	public class WebCamController : Singleton<WebCamController> {
		#region EVENTS
		public static event EventHandler<WebCamControllerEventArgs> OnWebCamStart;
		public static event EventHandler<WebCamControllerEventArgs> OnWebCamStopped;
		public static event EventHandler<WebCamControllerEventArgs> OnWebCamErrorIndex;
		public static event EventHandler<WebCamControllerEventArgs> OnWebCamErrorName;
		public static event EventHandler<WebCamControllerEventArgs> OnWebCamErrorDevice;
		#endregion

		#region VARIABLES
		private int _currentWidth;
		public int currentWidth {
			get { return _currentWidth; }
			private set { _currentWidth = value;}
		}

		private int _currentHeight;
		public int currentHeight {
			get { return _currentHeight; }
			private set { _currentHeight = value; }
		}

		private int _currentIndex = 0;
		public int currentIndex {
			get { return _currentIndex; }
			private set { _currentIndex = value; }
		}

		private string _currenName = "";
		public string currentName{
			get { return _currenName; }
			private set { _currenName = value; }
		}

		private Texture _image = null;
		public Texture image {
			get { return _image; }
			private set { _image = value; }
		}

		private WebCamDevice _activeWebCamDevice;
		public WebCamDevice activeWebCamDevice {
			get { return _activeWebCamDevice; }
			private set { _activeWebCamDevice = value; }
		}
		private WebCamTexture _activeWebCamTexture = null;
		public WebCamTexture activeWebCamTexture {
			get { return _activeWebCamTexture; }
			private set { _activeWebCamTexture = value; }
		}

		// Device WebCams
		List<WebCamDevice> devices = new List<WebCamDevice>();
		List<WebCamTexture> textures = new List<WebCamTexture>();
		#endregion

		#region API
		/// <summary>
		/// Checks the current webcam devices connected to the device and setups them.
		/// </summary>
		public void SetupDevices() {
			bool wasPaying = IsWebCamPlaying();
			Debug.Log(currentWidth + " x " + currentHeight);
			StopCurrentWebCam(false);

			devices.Clear();
			textures.Clear();

			for (int i = 0; i < WebCamTexture.devices.Length; ++i) {
				devices.Add(WebCamTexture.devices[i]);
				textures.Add(new WebCamTexture(WebCamTexture.devices[i].name));
				textures[i].filterMode = FilterMode.Trilinear;
			}

			if (wasPaying) {
				Debug.Log(currentWidth + " x " + currentHeight);
				StartSelectedWebCam(currentIndex, currentWidth, currentHeight);
			}
		}

		/// <summary>
		/// Setup the device WebCam to use and starts it.
		/// </summary>
		/// <param name="name">Name of the device.</param>
		/// <param name="customWidth">Requested width for the current WebCam.</param>
		/// <param name="customHeight">Requested height for the current WebCam.</param>
		/// <param name="fps">Requested FPS for the current WebCam.</param>
		public void StartSelectedWebCam(string name, int customWidth = -1, int customHeight = -1, int fps = -1) {
			int index = -1;
			for (int i = 0; i < devices.Count; ++i) {
				if (devices[i].name == name) {
					index = i;
					break;
				}
			}

			if (index >= 0)
				StartSelectedWebCam(index, customWidth, customHeight, fps);
			else {
				if(OnWebCamErrorName != null) {
					OnWebCamErrorName(this, new WebCamControllerEventArgs() { webCamName = name });
				}
			}
		}
		/// <summary>
		/// Set the device WebCam to use and start it
		/// </summary>
		/// <param name="index">Index of the device.</param>
		/// <param name="customWidth">Requested width for the current WebCam.</param>
		/// <param name="customHeight">Requested height for the current WebCam.</param>
		/// <param name="fps">Requested FPS for the current WebCam.</param>
		public void StartSelectedWebCam(int index, int customWidth = -1, int customHeight = -1, int fps = -1) {
			if (index >= devices.Count) {
				if (OnWebCamErrorIndex != null) {
					OnWebCamErrorIndex(this, new WebCamControllerEventArgs() { webCamIndex = index });
				}
			}

			StopCurrentWebCam();

			if (customWidth > 0 && customHeight > 0) {
				int _fps = fps > 0 ? fps : 30;
				textures[index] = new WebCamTexture(WebCamTexture.devices[index].name, customWidth, customHeight, _fps);
			}

			activeWebCamTexture = textures[index];
			activeWebCamDevice = WebCamTexture.devices[index];
			
			activeWebCamTexture.Play();

			if (!activeWebCamTexture.isPlaying) {
				if (OnWebCamErrorDevice != null) {
					OnWebCamErrorDevice(this, new WebCamControllerEventArgs() {
						webCamName = activeWebCamTexture.name,
						webCamIndex = index,
						webCamWidth = activeWebCamTexture.width,
						webCamHeight = activeWebCamTexture.height
					});
				}
			}
			else {
				image = activeWebCamTexture;

				if (OnWebCamStart != null) {
					OnWebCamStart(this, new WebCamControllerEventArgs() {
						webCamName = activeWebCamTexture.name,
						webCamIndex = index,
						webCamWidth = activeWebCamTexture.width,
						webCamHeight = activeWebCamTexture.height
					});
				}
			}

			currentIndex = index;
			currentName = activeWebCamTexture.name;
			currentWidth = activeWebCamTexture.width;
			currentHeight = activeWebCamTexture.height;
		}

		/// <summary>
		/// Selects the next device in the devices list
		/// </summary>
		/// <param name="customWidth">Requested width for the current WebCam.</param>
		/// <param name="customHeight">Requested height for the current WebCam.</param>
		/// <param name="fps">Requested FPS for the current WebCam.</param>
		public void SelectNextWebCam(int customWidth = -1, int customHeight = -1, int fps = -1) {
			int nextIndex = (currentIndex + 1) % devices.Count;

			StartSelectedWebCam(nextIndex, customWidth, customHeight, fps);
		}

		/// <summary>
		/// Stops the playback of the current WebCam.
		/// </summary>
		/// <param name="setupDevices">If true, calls <see cref="SetupDevices"/>.</param>
		public void StopCurrentWebCam(bool setupDevices = true) {
			if (activeWebCamTexture != null) {
				activeWebCamTexture.Stop();
				if (OnWebCamStopped != null) {
					OnWebCamStopped(this, new WebCamControllerEventArgs() { webCamIndex = currentIndex });
				}
			}
			if(setupDevices)
				SetupDevices();
		}

		/// <summary>
		/// Checks if the current WebCam is playing.
		/// </summary>
		/// <returns>True if the WebCam is playing, False otherwise.</returns>
		public bool IsWebCamPlaying() {
			if (activeWebCamTexture == null)
				return false;

			return activeWebCamTexture.isPlaying;
		}

		/// <summary>
		/// Returns the number of devices.
		/// </summary>
		/// <returns>Number of devices</returns>
		public int GetNumberOfDevices() {
			return devices.Count;
		}
		#endregion

		#region UNITY_CALLBACKS
		void Start() {
			SetupDevices();
			currentIndex = -1;
		}
		#endregion
	}
}