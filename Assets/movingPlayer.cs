using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlayer : MonoBehaviour {

	public bool move;

	public float movementSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(move){
			transform.position += transform.forward * movementSpeed * Time.deltaTime;
		}
	}
}
