﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class right_button_control : MonoBehaviour {

	public Animator animator;

	PlayerController pc;

	RectTransform button;

	// Use this for initialization
	void Start () {
		pc = GameObject.Find("PlayerGameObject").GetComponent<PlayerController>();
		button = GetComponent<RectTransform> ();
		animator = GameObject.Find("Basic_BanditPrefab Bighead").GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.y >= button.position.y - button.rect.height / 2 && Input.mousePosition.y <= button.position.y + button.rect.height / 2 && Input.mousePosition.x <= button.position.x + button.rect.width / 2 && Input.mousePosition.x >= button.position.x - button.rect.width / 2) {

			if (Input.GetMouseButtonDown (0)) {
				pc.direction_x = 1;
				animator.SetBool ("Walk", true);
			}	

			if (Input.GetMouseButtonUp (0)) {
				pc.direction_x = 0;
				animator.SetBool ("Walk", false);
			}
		}


	}
}
