using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_button_control : MonoBehaviour {

	BombSpawnAndExplode bsae;
	//public AudioSource music;
	//public AudioClip clip;
	RectTransform button;

	// Use this for initialization
	void Start () {
		bsae = GameObject.Find ("GameController").GetComponent<BombSpawnAndExplode> ();
		bsae.fire_button_pressed = false;
		button = GetComponent<RectTransform> ();
		//clip = music.clip;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.y >= button.position.y - button.rect.height / 2 && Input.mousePosition.y <= button.position.y + button.rect.height / 2 && Input.mousePosition.x <= button.position.x + button.rect.width / 2 && Input.mousePosition.x >= button.position.x - button.rect.width / 2) {
			if (Input.GetMouseButtonDown (0)) {
				bsae.fire_button_pressed = true;
				//music.Play ();
			}


		}

		if (Input.GetMouseButtonUp (0)) {
			bsae.fire_button_pressed = false;
			//music.Stop ();
		}
	}
}
