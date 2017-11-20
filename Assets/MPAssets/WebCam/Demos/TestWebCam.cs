using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MPAssets;

public class TestWebCam : MonoBehaviour {
	#region VARIABLES
	public RawImage image;
	public RectTransform imageParent;
	public AspectRatioFitter imageFitter;

	// Image rotation
	Vector3 rotationVector = new Vector3(0f, 0f, 0f);

	// Image uvRect
	Rect defaultRect = new Rect(0f, 0f, 1f, 1f);
	Rect fixedRect = new Rect(0f, 1f, 1f, -1f);

	// Image Parent's scale
	Vector3 defaultScale = new Vector3(1f, 1f, 1f);
	Vector3 fixedScale = new Vector3(-1f, 1f, 1f);

	#endregion

	#region UNITY_CALLBACKS
	// Use this for initialization
	void Start () {
		Log.Info("Test Camera");
		WebCamController.OnWebCamStart += CameraController_onCameraStart;
		WebCamController.OnWebCamStopped += CameraController_OnCameraStopped;
		WebCamController.OnWebCamErrorIndex += CameraController_OnCameraErrorIndex;
		WebCamController.OnWebCamErrorDevice += CameraController_OnCameraErrorDevice;
	}

	private void CameraController_OnCameraErrorDevice(object sender, WebCamControllerEventArgs e) {
		Log.Error("Error Playing Camera (" + e.webCamIndex + "): " + e.webCamName);
	}

	private void CameraController_OnCameraErrorIndex(object sender, WebCamControllerEventArgs e) {
		Log.Error("Error on camera index: " + e.webCamIndex);
	}

	private void CameraController_OnCameraStopped(object sender, WebCamControllerEventArgs e) {
		Log.Info("Camera Stopped (" + e.webCamIndex + ")");
	}

	private void CameraController_onCameraStart(object sender, WebCamControllerEventArgs e) {
		Log.Info(
			"Camere Started",
			"Camera(" + e.webCamIndex+ "): " + e.webCamName,
			"Size: " + e.webCamWidth + " x " + e.webCamHeight,
			"FPS: " + WebCamController.instance.activeWebCamTexture.requestedFPS,
			"videoRotationAngle: " + WebCamController.instance.activeWebCamTexture.videoRotationAngle,
			"videoVerticallyMirrored: " + WebCamController.instance.activeWebCamTexture.videoVerticallyMirrored);

		image.texture = WebCamController.instance.image;

		rotationVector.z = -WebCamController.instance.activeWebCamTexture.videoRotationAngle;
		image.rectTransform.localEulerAngles = rotationVector;

		//Set AspectRatioFitter's ratio
		float videoRatio =
			(float)WebCamController.instance.activeWebCamTexture.width / (float)WebCamController.instance.activeWebCamTexture.height;
		imageFitter.aspectRatio = videoRatio;

		// Unflip if vertically flipped
		image.uvRect =
			WebCamController.instance.activeWebCamTexture.videoVerticallyMirrored ? fixedRect : defaultRect;

		// Mirror front-facing camera's image horizontally to look more natural
		imageParent.localScale =
			WebCamController.instance.activeWebCamDevice.isFrontFacing ? fixedScale : defaultScale;
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			WebCamController.instance.SelectNextWebCam(1920, 1080);
		}

		if (Input.GetKeyDown(KeyCode.F5)) {
			WebCamController.instance.SetupDevices();
		}
	}
	#endregion
}
