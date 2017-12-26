using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	Vector3 approxPosition;
	Vector3 targetPosition;
	Vector3 movementDirection;
	Rigidbody rb;
	ObjectMatrix om;
	bool move;

	public int movementSpeed;

	void Start () {
		om = GameObject.Find("GameController").GetComponent<ObjectMatrix>();
		move = false;
		rb = GetComponent<Rigidbody> ();
		targetPosition = transform.position;
	}
	
	void Update () {
		approxPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

		if(move && transform.position!=targetPosition){
			Debug.Log("Enemy moved");
			transform.eulerAngles = new Vector3(0,(movementDirection.x + movementDirection.z) * 90.0f,0);
			rb.velocity = transform.forward * movementSpeed;
			if(Mathf.Abs(transform.position.x - targetPosition.x) + Mathf.Abs(transform.position.z - targetPosition.z) < 0.5f){
				transform.position = targetPosition;
				move = false;
			}
		}else if(transform.position == targetPosition){
			Debug.Log("Start of if loop");
			float direction_predictor = Random.Range(0.0f, 1.0f);
			
			if(direction_predictor < 0.25f){
				movementDirection = new Vector3(1.0f, 0, 0);
			}else if(direction_predictor < 0.5f){
				movementDirection = new Vector3(-1.0f, 0, 0);
			}else if(direction_predictor < 0.75f){
				movementDirection = new Vector3(0, 0, 1.0f);
			}else{
				movementDirection = new Vector3(0, 0, -1.0f);
			}

			if(space_available(movementDirection)){
				targetPosition = approxPosition + movementDirection;
				move = true;
				Debug.Log("move is true");
			}else{
				move = false;
				Debug.Log("move is false");
			}
				
			//movement(movementDirection, approxPosition, ref om);
			
		}

		// Debug.Log("While loop ended");
	}

	// void movement(Vector3 direction, Vector3 approxPosition, ref ObjectMatrix om){
	// 	Debug.Log("Inside movement function");
	// 	om = GameObject.Find("GameController").GetComponent<ObjectMatrix>();
	// 	if(space_available(direction)){
	// 		transform.eulerAngles = new Vector3(0,(direction.x + direction.z) * 90.0f,0);
	// 		while(transform.position != approxPosition + direction){
	// 			rb.velocity = direction * movementSpeed;
	// 		}
	// 		return;
	// 	}else return;
	// }	

	bool space_available(Vector3 direction){
		Debug.Log("Inside space_available function");
		Vector3 temp_position = approxPosition + direction;

		if(temp_position.x < 0 || temp_position.z < 0 || temp_position.x > om.rows-2 || temp_position.z > om.columns-2){
			return false;
		}

		if(om.level[(int)temp_position.x, (int)temp_position.z] == null){
			return true;
		}else return false;
	}	
}