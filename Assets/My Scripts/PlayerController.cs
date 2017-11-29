using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	float x;
	float z;
	Vector3 bombSpawn;
	float tempTime;
	
	public float speed;
	public GameObject bomb;
	public float bombWaitTime;
	
	void Start(){
		tempTime = bombWaitTime;
	}

	// Update is called once per frame
	void Update () {
		x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

		transform.Translate(x,0,0);
		transform.Translate(0,0,z);
		
	}


	void FixedUpdate(){
		bombSpawn = transform.position;
		bombSpawn += Vector3.up * 0.35f;
		
		if(Input.GetButton("Fire1") && Time.time > tempTime){
			//Debug.Log(Time.time);
			tempTime += bombWaitTime;
			Instantiate(bomb, bombSpawn, transform.rotation);
		}

	}
}
