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


		if(x>0){

			transform.eulerAngles = new Vector3(0,0,0);	
			transform.Translate(x,0,0);
		}else if(x<0){

			transform.eulerAngles = new Vector3(0,180,0);
			transform.Translate(-x,0,0);	
			
		}

		if(z>0){
			if(transform.rotation.y!=180 ){

				transform.eulerAngles = new Vector3(0,-90,0);
				transform.Translate(z,0,0);
			}
		}else if(z<0){
			if(transform.rotation.y!=180 ){

				transform.eulerAngles = new Vector3(0,90,0);
				transform.Translate(-z,0,0);
			}
		}
		
	}


	void FixedUpdate(){
		bombSpawn = transform.position;
		bombSpawn += Vector3.up * 0.35f;
		
		if(Input.GetKeyDown(KeyCode.F) && Time.time > tempTime){
			//Debug.Log(Time.time);
			tempTime += bombWaitTime;
			GameObject spawnedBomb = (GameObject)Instantiate(bomb, bombSpawn, transform.rotation);
			
			Destroy(spawnedBomb,3);
		}

	}
}
