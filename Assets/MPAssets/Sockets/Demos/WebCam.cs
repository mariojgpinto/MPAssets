using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WebCam : MonoBehaviour {
	public RawImage img;

	public WebCamTexture cam;

	// Use this for initialization
	void Awake () {
		img = GameObject.Find("RawImageCam").GetComponent<RawImage>();

		WebCamDevice[] devices = WebCamTexture.devices;

		int ac = 0;
		foreach (WebCamDevice device in devices){
			Debug.Log(ac++ + " - " + device.name);
		}
#if UNITY_EDITOR
		cam = new WebCamTexture(devices[0].name, 640, 480, 30);
#else
		cam = new WebCamTexture();
#endif

		img.texture = cam;

		cam.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
