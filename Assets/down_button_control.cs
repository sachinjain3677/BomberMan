using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down_button_control : MonoBehaviour {

	public Animator animator;

	PlayerController pc;

	// Use this for initialization
	void Start () {
		pc = GameObject.Find("PlayerGameObject").GetComponent<PlayerController>();

		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			pc.direction_z = -1;
			animator.SetBool ("Walk", true);
		}	

		if (Input.GetMouseButtonUp (0)) {
			pc.direction_z = 1;
			animator.SetBool ("Walk", false);
		}
	}
}
