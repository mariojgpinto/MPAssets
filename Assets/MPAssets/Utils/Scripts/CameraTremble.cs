using UnityEngine;
using System.Collections;

public class CameraTremble : MonoBehaviour {
	Vector3 originPosition;
	Quaternion originRotation;

	public float shakeDecay = 0.002f;
	public float shakeIntensity = .3f;

	float current_decay = 0.002f;
	float current_intensity = .3f;

	bool shake = false;

	public void Shake(){
		originPosition = transform.position;
		originRotation = transform.rotation;

		current_decay = shakeDecay;
		current_intensity = shakeIntensity;
		shake = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(shake){
			transform.position = originPosition + Random.insideUnitSphere * current_intensity;
			transform.rotation =  new Quaternion(
				originRotation.x + Random.Range(-current_intensity,current_intensity)*.2f,
				originRotation.y + Random.Range(-current_intensity,current_intensity)*.2f,
				originRotation.z + Random.Range(-current_intensity,current_intensity)*.2f,
				originRotation.w + Random.Range(-current_intensity,current_intensity)*.2f);
			current_intensity -= current_decay;

			if(current_intensity <= 0){
				shake = false;

				transform.position = originPosition;
				transform.rotation = transform.rotation;
			}
		}
	}

	void OnGUI () {
		if (Input.GetKeyDown(KeyCode.P)){
			Shake();
		}
	} 
}