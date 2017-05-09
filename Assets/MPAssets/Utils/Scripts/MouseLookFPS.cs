using UnityEngine;
using System.Collections;


namespace MPAssets {
	/// MouseLook rotates the transform based on the mouse delta.
	/// Minimum and Maximum values can be used to constrain the possible rotation

	/// To make an FPS style character:
	/// - Create a capsule.
	/// - Add the MouseLook script to the capsule.
	///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
	/// - Add FPSInputController script to the capsule
	///   -> A CharacterMotor and a CharacterController component will be automatically added.

	/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
	/// - Add a MouseLook script to the camera.
	///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
	///   
	public class MouseLookFPS : MonoBehaviour {
		public enum InputMethod { Mouse, Touch }
		public InputMethod method = InputMethod.Mouse;

		public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
		public RotationAxes axes = RotationAxes.MouseXAndY;

		public bool invertedControls = false;

		public float sensitivityX = 3F;
		public float sensitivityY = 3F;

		public float minimumX = -360F;
		public float maximumX = 360F;

		public float minimumY = -60F;
		public float maximumY = 60F;

		float rotationY = 0F;





		void Update() {
			//method = (InputMethod)Settings_FirstPersonView.modeIdx;

			if (method == InputMethod.Mouse) {
				if (axes == RotationAxes.MouseXAndY) {
					float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX * (invertedControls ? -1 : 1);

					rotationY += Input.GetAxis("Mouse Y") * sensitivityY * (invertedControls ? -1 : 1);
					rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

					transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
				}
				else if (axes == RotationAxes.MouseX) {
					transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX * (invertedControls ? -1 : 1), 0);
				}
				else {
					rotationY += Input.GetAxis("Mouse Y") * sensitivityY * (invertedControls ? -1 : 1);
					rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

					transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
				}
			}
			else {
				if (Input.touchCount > 0) {
					if (Input.GetTouch(0).phase == TouchPhase.Moved) {
						Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

						float rotationX = transform.localEulerAngles.y + touchDeltaPosition.x * sensitivityX * (invertedControls ? -1 : 1);

						rotationY += touchDeltaPosition.y * sensitivityY * (invertedControls ? -1 : 1);
						rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

						transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
					}
				}
			}
		}

		//void Start() {
		//	//if(!networkView.isMine)
		//	//enabled = false;

		//	// Make the rigid body not change rotation
		//	//if (rigidbody)
		//	//rigidbody.freezeRotation = true;
		//}

		//void OnGUI() {

		//}
	}
}