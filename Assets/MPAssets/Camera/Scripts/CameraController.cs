using System;
using System.Collections.Generic;
using UnityEngine;

namespace MPAssets {
	public class CameraControllerEventArgs : EventArgs {
		public string cameraName;
		public int cameraIndex;
		public int cameraWidth;
		public int cameraHeight;
	}

	public class CameraController : Singleton<CameraController> {
		#region EVENTS
		public static event EventHandler<CameraControllerEventArgs> OnCameraStart;
		public static event EventHandler<CameraControllerEventArgs> OnCameraStopped;
		public static event EventHandler<CameraControllerEventArgs> OnCameraErrorIndex;
		public static event EventHandler<CameraControllerEventArgs> OnCameraErrorName;
		public static event EventHandler<CameraControllerEventArgs> OnCameraErrorDevice;
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

		private WebCamDevice _activeCameraDevice;
		public WebCamDevice activeCameraDevice {
			get { return _activeCameraDevice; }
			private set { _activeCameraDevice = value; }
		}
		private WebCamTexture _activeCameraTexture = null;
		public WebCamTexture activeCameraTexture {
			get { return _activeCameraTexture; }
			private set { _activeCameraTexture = value; }
		}

		// Device cameras
		List<WebCamDevice> devices = new List<WebCamDevice>();
		List<WebCamTexture> textures = new List<WebCamTexture>();
		#endregion

		#region API
		/// <summary>
		/// Checks the current camera devices connected to the device and setups them.
		/// </summary>
		public void SetupDevices() {
			bool wasPaying = IsCameraPlaying();
			Debug.Log(currentWidth + " x " + currentHeight);
			StopCurrentCamera(false);

			devices.Clear();
			textures.Clear();

			for (int i = 0; i < WebCamTexture.devices.Length; ++i) {
				devices.Add(WebCamTexture.devices[i]);
				textures.Add(new WebCamTexture(WebCamTexture.devices[i].name));
				textures[i].filterMode = FilterMode.Trilinear;
			}

			if (wasPaying) {
				Debug.Log(currentWidth + " x " + currentHeight);
				StartSelectedCamera(currentIndex, currentWidth, currentHeight);
			}
		}

		/// <summary>
		/// Setup the device camera to use and starts it.
		/// </summary>
		/// <param name="name">Name of the device.</param>
		/// <param name="customWidth">Requested width for the current camera.</param>
		/// <param name="customHeight">Requested height for the current camera.</param>
		/// <param name="fps">Requested FPS for the current camera.</param>
		public void StartSelectedCamera(string name, int customWidth = -1, int customHeight = -1, int fps = -1) {
			int index = -1;
			for (int i = 0; i < devices.Count; ++i) {
				if (devices[i].name == name) {
					index = i;
					break;
				}
			}

			if (index >= 0)
				StartSelectedCamera(index, customWidth, customHeight, fps);
			else {
				if(OnCameraErrorName != null) {
					OnCameraErrorName(this, new CameraControllerEventArgs() { cameraName = name });
				}
			}
		}
		/// <summary>
		/// Set the device camera to use and start it
		/// </summary>
		/// <param name="index">Index of the device.</param>
		/// <param name="customWidth">Requested width for the current camera.</param>
		/// <param name="customHeight">Requested height for the current camera.</param>
		/// <param name="fps">Requested FPS for the current camera.</param>
		public void StartSelectedCamera(int index, int customWidth = -1, int customHeight = -1, int fps = -1) {
			if (index >= devices.Count) {
				if (OnCameraErrorIndex != null) {
					OnCameraErrorIndex(this, new CameraControllerEventArgs() { cameraIndex = index });
				}
			}

			StopCurrentCamera();

			if (customWidth > 0 && customHeight > 0) {
				int _fps = fps > 0 ? fps : 30;
				textures[index] = new WebCamTexture(WebCamTexture.devices[index].name, customWidth, customHeight, _fps);
			}

			activeCameraTexture = textures[index];
			activeCameraDevice = WebCamTexture.devices[index];
			
			activeCameraTexture.Play();

			if (!activeCameraTexture.isPlaying) {
				if (OnCameraErrorDevice != null) {
					OnCameraErrorDevice(this, new CameraControllerEventArgs() {
						cameraName = activeCameraTexture.name,
						cameraIndex = index,
						cameraWidth = activeCameraTexture.width,
						cameraHeight = activeCameraTexture.height
					});
				}
			}
			else {
				image = activeCameraTexture;

				if (OnCameraStart != null) {
					OnCameraStart(this, new CameraControllerEventArgs() {
						cameraName = activeCameraTexture.name,
						cameraIndex = index,
						cameraWidth = activeCameraTexture.width,
						cameraHeight = activeCameraTexture.height
					});
				}
			}

			currentIndex = index;
			currentName = activeCameraTexture.name;
			currentWidth = activeCameraTexture.width;
			currentHeight = activeCameraTexture.height;
		}

		/// <summary>
		/// Selects the next device in the devices list
		/// </summary>
		/// <param name="customWidth">Requested width for the current camera.</param>
		/// <param name="customHeight">Requested height for the current camera.</param>
		/// <param name="fps">Requested FPS for the current camera.</param>
		public void SelectNextCamera(int customWidth = -1, int customHeight = -1, int fps = -1) {
			int nextIndex = (currentIndex + 1) % devices.Count;

			StartSelectedCamera(nextIndex, customWidth, customHeight, fps);
		}

		/// <summary>
		/// Stops the playback of the current camera.
		/// </summary>
		/// <param name="setupDevices">If true, calls <see cref="SetupDevices"/>.</param>
		public void StopCurrentCamera(bool setupDevices = true) {
			if (activeCameraTexture != null) {
				activeCameraTexture.Stop();
				if (OnCameraStopped != null) {
					OnCameraStopped(this, new CameraControllerEventArgs() { cameraIndex = currentIndex });
				}
			}
			if(setupDevices)
				SetupDevices();
		}

		/// <summary>
		/// Checks if the current camera is playing.
		/// </summary>
		/// <returns>True if the camera is playing, False otherwise.</returns>
		public bool IsCameraPlaying() {
			if (activeCameraTexture == null)
				return false;

			return activeCameraTexture.isPlaying;
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