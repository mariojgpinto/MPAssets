using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MPAssets;

public class TestCamera : MonoBehaviour {
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
		CameraController.OnCameraStart += CameraController_onCameraStart;
		CameraController.OnCameraStopped += CameraController_OnCameraStopped;
		CameraController.OnCameraErrorIndex += CameraController_OnCameraErrorIndex;
		CameraController.OnCameraErrorDevice += CameraController_OnCameraErrorDevice;
	}

	private void CameraController_OnCameraErrorDevice(object sender, CameraControllerEventArgs e) {
		Log.Error("Error Playing Camera (" + e.cameraIndex + "): " + e.cameraName);
	}

	private void CameraController_OnCameraErrorIndex(object sender, CameraControllerEventArgs e) {
		Log.Error("Error on camera index: " + e.cameraIndex);
	}

	private void CameraController_OnCameraStopped(object sender, CameraControllerEventArgs e) {
		Log.Info("Camera Stopped (" + e.cameraIndex + ")");
	}

	private void CameraController_onCameraStart(object sender, CameraControllerEventArgs e) {
		Log.Info(
			"Camere Started",
			"Camera(" + e.cameraIndex+ "): " + e.cameraName,
			"Size: " + e.cameraWidth + " x " + e.cameraHeight,
			"FPS: " + CameraController.instance.activeCameraTexture.requestedFPS,
			"videoRotationAngle: " + CameraController.instance.activeCameraTexture.videoRotationAngle,
			"videoVerticallyMirrored: " + CameraController.instance.activeCameraTexture.videoVerticallyMirrored);

		image.texture = CameraController.instance.image;

		rotationVector.z = -CameraController.instance.activeCameraTexture.videoRotationAngle;
		image.rectTransform.localEulerAngles = rotationVector;

		//Set AspectRatioFitter's ratio
		float videoRatio =
			(float)CameraController.instance.activeCameraTexture.width / (float)CameraController.instance.activeCameraTexture.height;
		imageFitter.aspectRatio = videoRatio;

		// Unflip if vertically flipped
		image.uvRect =
			CameraController.instance.activeCameraTexture.videoVerticallyMirrored ? fixedRect : defaultRect;

		// Mirror front-facing camera's image horizontally to look more natural
		imageParent.localScale =
			CameraController.instance.activeCameraDevice.isFrontFacing ? fixedScale : defaultScale;
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			CameraController.instance.SelectNextCamera(1920, 1080);
		}

		if (Input.GetKeyDown(KeyCode.F5)) {
			CameraController.instance.SetupDevices();
		}
	}
	#endregion
}
