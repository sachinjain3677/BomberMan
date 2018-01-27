using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_button_control : MonoBehaviour {

	BombSpawnAndExplode bsae;

	// Use this for initialization
	void Start () {
		bsae = GameObject.Find ("GameController").GetComponent<BombSpawnAndExplode> ();
		bsae.fire_button_pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			bsae.fire_button_pressed = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			bsae.fire_button_pressed = false;
		}
	}
}
