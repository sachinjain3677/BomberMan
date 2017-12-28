using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlayer : MonoBehaviour {

	public bool move;
	public bool stop;
	public float movementSpeed;
	public Vector3 target;
	public Vector3 moving_direction;

	// Use this for initialization
	void Start () {
		target = transform.position + new Vector3(1,1,1);
		move = false;
		stop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!stop){
			if(transform.position == target){
				move = false;
			}

			if(move){
				transform.LookAt(transform.position + moving_direction);
				transform.position += transform.forward * movementSpeed * Time.deltaTime;
				if(Vector3.Distance(transform.position, target) < 0.1f){
					transform.position = target;
					move = false;
				}
			}
		}
}
}
