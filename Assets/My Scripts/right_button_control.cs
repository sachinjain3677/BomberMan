using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class right_button_control : MonoBehaviour {

	public Animator animator;
	public float buffer;

	PlayerController pc;
	public AudioSource music;
	public AudioClip clip;
	RectTransform button;

	// Use this for initialization
	void Start () {
		pc = GameObject.Find("PlayerGameObject").GetComponent<PlayerController>();
		button = GetComponent<RectTransform> ();
		animator = GameObject.Find("Basic_BanditPrefab Bighead").GetComponent<Animator> ();
		clip = music.clip;
	}

	// Update is called once per frame
	void Update () {
		if (animator == null) {
			return;	}
		if (Input.mousePosition.y >= button.position.y - button.rect.height / 2 && Input.mousePosition.y <= button.position.y + button.rect.height / 2 && Input.mousePosition.x <= button.position.x + button.rect.width / 2 + buffer && Input.mousePosition.x >= button.position.x - button.rect.width / 2 - buffer) {

			if (Input.GetMouseButtonDown (0)) {
				pc.direction_x = 1;
				animator.SetBool ("Walk", true);
				music.Play ();
			}	


		}

		if (Input.GetMouseButtonUp (0)) {
			pc.direction_x = 0;
			animator.SetBool ("Walk", false);
			music.Stop ();
		}


	}
}
