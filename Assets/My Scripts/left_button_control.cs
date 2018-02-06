using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left_button_control : MonoBehaviour {

	private Animator animator;
	public float buffer;

	PlayerController pc;
	public AudioSource music;
	RectTransform button;

	// Use this for initialization
	void Start () {
		//pc = GameObject.Find("PlayerGameObject").GetComponent<PlayerController>();
		button = GetComponent<RectTransform> ();
		//animator = GameObject.Find("Basic_BanditPrefab Bighead").GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		pc = GameObject.Find("PlayerGameObject").GetComponent<PlayerController>();
		animator = GameObject.Find("Basic_BanditPrefab Bighead").GetComponent<Animator> ();


		if (animator == null  || pc == null) {
			music.Stop ();
			return;	}
	
		if (Input.mousePosition.y >= button.position.y - button.rect.height / 2 - buffer && Input.mousePosition.y <= button.position.y + button.rect.height / 2 + buffer && Input.mousePosition.x <= button.position.x + button.rect.width / 2 && Input.mousePosition.x >= button.position.x - button.rect.width / 2) {

			if (Input.GetMouseButtonDown (0)) {
				pc.direction_x = -1;
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
