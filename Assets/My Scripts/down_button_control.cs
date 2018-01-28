using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down_button_control : MonoBehaviour {

	public Animator animator;
	public float buffer;
	public AudioSource music;
	public AudioClip clip;
	PlayerController pc;

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
		if (Input.mousePosition.y >= button.position.y - button.rect.width / 2 - buffer && Input.mousePosition.y <= button.position.y + button.rect.width / 2 + buffer && Input.mousePosition.x <= button.position.x + button.rect.height / 2 && Input.mousePosition.x >= button.position.x - button.rect.height / 2) {
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
