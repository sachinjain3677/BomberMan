using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down_button_control : MonoBehaviour {

	private Animator animator;
	public float buffer;
	public AudioSource music;
	PlayerController pc;

	RectTransform button;

	// Use this for initialization
	void Start () {
		
		button = GetComponent<RectTransform> ();
	}

	// Update is called once per frame
	void Update () {
		pc = GameObject.Find("PlayerGameObject").GetComponent<PlayerController>();
		animator = GameObject.Find("Basic_BanditPrefab Bighead").GetComponent<Animator> ();

		if (animator == null || pc == null) {
			music.Stop ();
			return;	}
		if (Input.mousePosition.y >= button.position.y - button.rect.width / 2  && Input.mousePosition.y <= button.position.y + button.rect.width / 2  && Input.mousePosition.x <= button.position.x + button.rect.height / 2 + buffer && Input.mousePosition.x >= button.position.x - button.rect.height / 2 - buffer) {
			if (Input.GetMouseButtonDown (0)) {
				pc.direction_z = -1;
				animator.SetBool ("Walk", true);
				music.Play ();
			}	


		}

		if (Input.GetMouseButtonUp (0)) {
			pc.direction_z = 0;
			animator.SetBool ("Walk", false);
			music.Stop ();
		}



	}
}
