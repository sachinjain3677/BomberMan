using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_control_direction_predictor : MonoBehaviour {

	float z;
	float x;
	float rotation_y;

	PlayerController pc;

	void Start(){
		
	}

	// Update is called once per frame
	void Update () {
		pc = GameObject.Find ("PlayerGameObject").GetComponent<PlayerController> ();
		//uncomment if change controls according to ar camera position
		z = transform.position.z;
		x = transform.position.x;
		if (2 * z < x) {
			if (2 * (14 - z) > x) {
				pc.ar_camera_side = 0;
			} else if (2 * (14 - z) < x) {
				pc.ar_camera_side = 3;
			}
		} else if (2 * z > x) {
			if (2 * (14 - z) > x) {
				pc.ar_camera_side = 1;
			} else if (2 * (14 - z) < x) {
				pc.ar_camera_side = 2;
			}
		}	


		//uncomment if change controls according to rotation of ar camera
	//	rotation_y = transform.rotation.y;
	//	if (rotation_y < 0) {
	//		rotation_y = 360.0f - Mathf.Abs (rotation_y) % 360;
	//	}
	
	//	if (rotation_y > 360) {
	//		rotation_y = rotation_y % 360;
	//	}

	//	if (rotation_y > 120.0f) {
	//		if (rotation_y < 240.0f) {
	//			pc.ar_camera_side = 2;
	//		} else if (rotation_y < 300.0f) {
	//			pc.ar_camera_side = 3;
	//		} else {
	//			pc.ar_camera_side = 0;
	//		}
	//	} else { 
	//		if (rotation_y > 60.0f) {
	//			pc.ar_camera_side = 1;
	//		} else {
	//			pc.ar_camera_side = 0;
	//		}
	//	}
	}
}
