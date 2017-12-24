using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	Vector3 approxPosition;
	Vector3 movementDirection;
	Rigidbody rb;
	ObjectMatrix om;
	
	public int movementSpeed;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	void Update () {
		approxPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

		if(approxPosition == transform.position){
			Debug.Log("Start of while loop");
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

			movement(movementDirection, approxPosition, ref om);
		}

		Debug.Log("While loop ended");
	}

	void movement(Vector3 direction, Vector3 approxPosition, ref ObjectMatrix om){
		Debug.Log("Inside movement function");
		om = GameObject.Find("GameController").GetComponent<ObjectMatrix>();
		if(space_available(direction)){
			transform.eulerAngles = new Vector3(0,(direction.x + direction.z) * 90.0f,0);
			if(transform.position != approxPosition + direction){
				Debug.Log("Enemy Moved");
				transform.Translate(transform.forward);
			}
			return;
		}else return;
	}	

	bool space_available(Vector3 direction){
		Debug.Log("Inside space_available function");
		Vector3 temp_position = transform.position + direction;

		if(temp_position.x < 0 || temp_position.z < 0 || temp_position.x > om.rows-2 || temp_position.z > om.columns-2){
			return false;
		}

		if(om.level[(int)temp_position.x, (int)temp_position.z] == null){
			return true;
		}else return false;
	}	
}