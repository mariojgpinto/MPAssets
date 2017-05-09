using UnityEngine;
using System;
using System.Collections;

namespace MPAssets {
	[Serializable]
	public class PhoneInfo {
		public string folder;
		public int width;
		public int height;
	}

	public class ScreenshotGenerator : MonoBehaviour {
		public enum IMAGE_TYPE {
			PNG,
			JPG
		};
		public IMAGE_TYPE imageType = IMAGE_TYPE.PNG;


		//[SerializeField]
		public PhoneInfo[] phones = new PhoneInfo[]{
		new PhoneInfo{
			folder = "Android/Phone",
			width = 1280,
			height = 720
		},
		new PhoneInfo{
			folder = "Android/Tablet7",
			width = 1280,
			height = 720
		},
		new PhoneInfo{
			folder = "Android/Tablet10",
			width = 1920,
			height = 1080
		},
		new PhoneInfo{
			folder = "iOS/iPad",
			width = 2048,
			height = 1536
		},
		new PhoneInfo{
			folder = "iOS/iPhone_3_5",
			width = 960,
			height = 640
		},
		new PhoneInfo{
			folder = "iOS/iPhone_4",
			width = 1136,
			height = 640
		},
		new PhoneInfo{
			folder = "iOS/iPhone_4_7",
			width = 1334,
			height = 750
		},
		new PhoneInfo{
			folder = "iOS/iPhone_5_5",
			width = 2208,
			height = 1242
		}
	};

		public string photoPath = "C:/Dev/Data/ScreenShots/";
		public string photoName = "";




		private bool takePhoto = false;
		private int takePhotoIdx = 0;

		public void GenerateScreenShots() {
			Debug.Log("GenerateScreenShots - " + phones.Length);
			if (takePhoto == false) {
				if (!photoPath.EndsWith("/")) {
					photoPath += "/";
				}

				if (!System.IO.Directory.Exists(photoPath)) {
					System.IO.Directory.CreateDirectory(photoPath);
				}

				for (int i = 0; i < phones.Length; ++i) {
					//				Debug.Log(photoPath+phones[i].folder);
					if (!System.IO.Directory.Exists(photoPath + phones[i].folder)) {
						System.IO.Directory.CreateDirectory(photoPath + phones[i].folder);
					}
				}

				takePhotoIdx = 0;
				takePhoto = true;

				StartCoroutine(TakePhoto());
			}
		}

		//	void Update() {
		//
		//	}

		IEnumerator TakePhoto() {
			if (takePhoto) {
				int resWidth = phones[takePhotoIdx].width;
				int resHeight = phones[takePhotoIdx].height;

				Screen.SetResolution(resWidth, resHeight, false);
				yield return new WaitForSeconds(1f);
				string filename = ScreenShotName(takePhotoIdx, resWidth, resHeight);


				RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);

				GetComponent<Camera>().targetTexture = rt;
				Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

				//ResetAspect() ResetProjectionMatrix() Render()
				GetComponent<Camera>().ResetAspect();
				GetComponent<Camera>().ResetProjectionMatrix();
				GetComponent<Camera>().Render();
				RenderTexture.active = rt;

				screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
				GetComponent<Camera>().targetTexture = null;
				RenderTexture.active = null; // JC: added to avoid errors
				Destroy(rt);
				byte[] bytes = screenShot.EncodeToPNG();
				System.IO.File.WriteAllBytes(filename, bytes);
				//			Application.CaptureScreenshot(filename);

				Debug.Log(string.Format("Took screenshot to: {0}", filename));

				takePhotoIdx++;
				if (takePhotoIdx == phones.Length) {
					Debug.Log("Finish taking photos");
					takePhoto = false;
					takePhotoIdx = 0;
				}
				else {
					StartCoroutine(TakePhoto());
				}
			}
		}

		public string ScreenShotName(int id, int width, int height) {
			return string.Format("{0}{1}screen_{2}x{3}_{4}.png",
								 photoPath,
								 phones[id].folder + (phones[id].folder.EndsWith("/") ? "" : "/"),
								 width, height,
								 System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
		}

		//void Update(){
		//	if(Input.GetMouseButton(0)){
		//		GenerateScreenShots();
		//	}
		//}
		// Use this for initialization
		//	void Start () {

		//		phones = new PhoneInfo[]{
		//			new PhoneInfo{
		//				folder = "Android/Phone"
		//			},
		//			new PhoneInfo{
		//				folder = "Android/Tablet7"
		//			},
		//			new PhoneInfo{
		//				folder = "Android/Tablet10"
		//			},
		//			new PhoneInfo{
		//				folder = "iOS/iPad"
		//			},
		//			new PhoneInfo{
		//				folder = "iOS/iPhone_3_5"
		//			},
		//			new PhoneInfo{
		//				folder = "iOS/iPhone_4"
		//			},
		//			new PhoneInfo{
		//				folder = "iOS/iPhone_4_7"
		//			},
		//			new PhoneInfo{
		//				folder = "iOS/iPhone_5_5"
		//			}
		//		};
		//	}

		// Update is called once per frame
		//	void Update () {
		//	
		//	}

		//	public override void OnInspectorGUI()
		//	{
		//		DrawDefaultInspector();
		//		
		////		ObjectBuilderScript myScript = (ObjectBuilderScript)target;
		//		if(GUILayout.Button("Build Object"))
		//		{
		////			myScript.BuildObject();
		//			Debug.Log("Button Pressed");
		//		}
		//	}
	}
}