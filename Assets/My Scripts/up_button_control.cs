﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class up_button_control : MonoBehaviour {

	PlayerController pc;

	public Animator animator;

	RectTransform button;
	// Use this for initialization
	void Start () {
		pc = GameObject.Find("PlayerGameObject").GetComponent<PlayerController>();
		button = GetComponent<RectTransform> ();
		animator = GameObject.Find("Basic_BanditPrefab Bighead").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.y >= button.position.y - button.rect.width / 2 && Input.mousePosition.y <= button.position.y + button.rect.width / 2 && Input.mousePosition.x <= button.position.x + button.rect.height / 2 && Input.mousePosition.x >= button.position.x - button.rect.height / 2) {
			if (Input.GetMouseButtonDown (0)) {
				pc.direction_z = 1;
				animator.SetBool ("Walk", true);
			}	

			if (Input.GetMouseButtonUp (0)) {
				pc.direction_z = 0;
				animator.SetBool ("Walk", false);
			}
		}
	}
}
