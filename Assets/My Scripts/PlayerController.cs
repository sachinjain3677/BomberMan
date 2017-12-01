using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	float x;
	float z;
	
	public float speed;
	
	
	// Update is called once per frame
	void Update () {
		x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		movement(x,z);
	}

	void movement(float x, float z){
		
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
}
