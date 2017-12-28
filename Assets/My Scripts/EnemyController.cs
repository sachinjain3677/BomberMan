using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	bool front_obstruction;
	bool back_obstruction;
	bool left_obstruction;
	bool right_obstruction;
	bool move;
	movingPlayer mp;
	Vector3 moving_direction;
	int layerMask;
	
	public float check_distance;
	
	void Start(){
		mp = GetComponent<movingPlayer>();
		//update_obstruction_bools(ref front_obstruction, ref back_obstruction, ref left_obstruction, ref right_obstruction);
		mp.move = false;
		mp.moving_direction = new Vector3(0,0,0);
		layerMask = 1 << 8;
		layerMask = layerMask + 1 << 9;
		layerMask = ~layerMask;
	}

	void Update(){
		update_obstruction_bools(ref front_obstruction, ref back_obstruction, ref left_obstruction, ref right_obstruction);
			
		if(!mp.move || mp.stop){
			mp.moving_direction = pick_direction();
			mp.target = transform.position + mp.moving_direction;
			mp.move = true;
		}	
	}

	Vector3 pick_direction(){
		if(front_obstruction && back_obstruction && left_obstruction && right_obstruction){
			mp.stop = true;
			return new Vector3(0,0,0);
		}

		float random_number = Random.Range(0.0f, 1.0f);
		if(random_number < 0.25f){
			if(!front_obstruction){
				return new Vector3(0,0,1);
				mp.stop = false;
			}else{
				return pick_direction();
			}
		}else if(random_number < 0.5f){
			if(!back_obstruction){
				return new Vector3(0,0,-1);
				mp.stop = false;
			}else{
				return pick_direction();
			}
		}else if(random_number < 0.75f){
			if(!left_obstruction){
				return new Vector3(-1,0,0);
				mp.stop = false;
			}else{
				return pick_direction();
			}
		}else{
			if(!right_obstruction){
				return new Vector3(1,0,0);
				mp.stop = false;
			}else{
				return pick_direction();
			}
		}
	}

	void update_obstruction_bools(ref bool front_obstruction, ref bool back_obstruction, ref bool left_obstruction, ref bool right_obstruction){
		if(Physics.Raycast(transform.position, Vector3.forward, check_distance, layerMask)){
			front_obstruction = true;
		}else{
			front_obstruction = false;
		}

		if(Physics.Raycast(transform.position, Vector3.back, check_distance, layerMask)){
			back_obstruction = true;
		}else{
			back_obstruction = false;
		}

		if(Physics.Raycast(transform.position, Vector3.left, check_distance, layerMask)){
			left_obstruction = true;
		}else{
			left_obstruction = false;
		}

		if(Physics.Raycast(transform.position, Vector3.right, check_distance, layerMask)){
			right_obstruction = true;
		}else{
			right_obstruction = false;
		}
	}


















///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	// Rigidbody rb;
	// Vector3 initialPosition;
	// Vector3 targetPosition;
	// Vector3 movementDirection;
	// float direction_predictor;
	// ObjectMatrix om;

	// public int movementSpeed;

	// void Start(){
	// 	om = GameObject.Find("GameController").GetComponent<ObjectMatrix>();
	// 	rb = GetComponent<Rigidbody> ();
	// 	initialPosition = transform.position;
	// 	targetPosition = transform.position;
	// 	movementDirection = new Vector3(0,0,0);
	// }

	// void Update(){
	// 	if(transform.position == targetPosition){
	// 		while(movementDirection == new Vector3(0,0,0)){
	// 			movementDirection = predict_direction();
	// 		}
	// 		targetPosition = transform.position + movementDirection;//so that compiler can go inside the else statement
	// 		initialPosition = transform.position;
	// 	}else{
	// 		// if(space_available(movementDirection, initialPosition)){
				
	// 		// }
	// 		if(Vector3.Distance(transform.position, targetPosition) > 0.05f){
	// 			transform.LookAt(targetPosition);
	// 			transform.position += transform.forward * movementSpeed * Time.deltaTime;
	// 		}else{
	// 			transform.position = targetPosition;
	// 			initialPosition = targetPosition;
	// 		}
	// 		movementDirection = new Vector3(0,0,0);
			
	// 	}
	// }

	// Vector3 predict_direction(){
	// 	direction_predictor = Random.Range(0.0f ,1.0f);
	// 	if((int)transform.position.x < om.rows-2 && (int)transform.position.z < om.columns-2 && (int)transform.position.x > 0 && (int)transform.position.z > 0){
	// 		if(direction_predictor < 0.25f){
	// 			if(om.level[(int)transform.position.x + 1, (int)transform.position.z] == null && (int)transform.position.x < om.rows-2){
	// 				return new Vector3(1.0f, 0, 0);	
	// 			}else if(om.level[(int)transform.position.x - 1, (int)transform.position.z] == null && (int)transform.position.x > 0){
	// 				return new Vector3(-1.0f, 0, 0); 
	// 			}else if(om.level[(int)transform.position.x, (int)transform.position.z + 1] == null && (int)transform.position.z < om.columns-2){
	// 				return new Vector3(0, 0, 1.0f);
	// 			}else if(om.level[(int)transform.position.x, (int)transform.position.z - 1] == null && (int)transform.position.z > 0){
	// 				return new Vector3(0, 0, -1.0f);
	// 			}
				
	// 		}else if(direction_predictor < 0.5f){
	// 			if(om.level[(int)transform.position.x - 1, (int)transform.position.z] == null && (int)transform.position.x > 0){
	// 				return new Vector3(1.0f, 0, 0);	
	// 			}else if(om.level[(int)transform.position.x, (int)transform.position.z + 1] == null && (int)transform.position.z < om.columns-2){
	// 				return new Vector3(-1.0f, 0, 0); 
	// 			}else if(om.level[(int)transform.position.x, (int)transform.position.z - 1] == null && (int)transform.position.z > 0){
	// 				return new Vector3(0, 0, 1.0f);
	// 			}else if(om.level[(int)transform.position.x + 1, (int)transform.position.z] == null && (int)transform.position.x < om.rows-2){
	// 				return new Vector3(0, 0, -1.0f);
	// 			}
	// 		}else if(direction_predictor < 0.75f){
	// 			if(om.level[(int)transform.position.x, (int)transform.position.z + 1] == null && (int)transform.position.z < om.columns-2){
	// 				return new Vector3(1.0f, 0, 0);	
	// 			}else if(om.level[(int)transform.position.x, (int)transform.position.z - 1] == null && (int)transform.position.z > 0){
	// 				return new Vector3(-1.0f, 0, 0); 
	// 			}else if(om.level[(int)transform.position.x + 1, (int)transform.position.z] == null && (int)transform.position.x < om.rows-2){
	// 				return new Vector3(0, 0, 1.0f);
	// 			}else if(om.level[(int)transform.position.x - 1, (int)transform.position.z] == null && (int)transform.position.x > 0){
	// 				return new Vector3(0, 0, -1.0f);
	// 			}
	// 		}else{
	// 			if(om.level[(int)transform.position.x, (int)transform.position.z - 1] == null && (int)transform.position.z > 0){
	// 				return new Vector3(1.0f, 0, 0);	
	// 			}else if(om.level[(int)transform.position.x + 1, (int)transform.position.z] == null && (int)transform.position.x < om.rows-2){
	// 				return new Vector3(-1.0f, 0, 0); 
	// 			}else if(om.level[(int)transform.position.x - 1, (int)transform.position.z] == null && (int)transform.position.x > 0){
	// 				return new Vector3(0, 0, 1.0f);
	// 			}else if(om.level[(int)transform.position.x, (int)transform.position.z + 1] == null && (int)transform.position.z < om.columns-2){
	// 				return new Vector3(0, 0, -1.0f);
	// 			}
	// 		}
	// 	}

	// 	return new Vector3(0,0,0);
	// }

	// bool space_available(Vector3 direction, Vector3 initialPosition){
	// 	Debug.Log("Inside space_available function");
	// 	Vector3 temp_position = initialPosition + direction;

	// 	if(temp_position.x < 0 || temp_position.z < 0 || temp_position.x > om.rows-2 || temp_position.z > om.columns-2){
	// 		return false;
	// 	}

	// 	if(om.level[(int)temp_position.x, (int)temp_position.z] == null){
	// 		return true;
	// 	}else return false;
	// }	

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	// Vector3 approxPosition;
	// Vector3 targetPosition;
	// Vector3 movementDirection;
	// Rigidbody rb;
	// ObjectMatrix om;
	// bool move;

	// public int movementSpeed;

	// void Start () {
	// 	om = GameObject.Find("GameController").GetComponent<ObjectMatrix>();
	// 	move = false;
	// 	rb = GetComponent<Rigidbody> ();
	// 	targetPosition = transform.position;
	// }

	
	// void Update () {
	// 	approxPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

	// 	if(move && transform.position!=targetPosition){
	// 		//Debug.Log("Enemy moved");
	// 		rb.velocity = transform.forward * movementSpeed;
	// 		// if(Mathf.Abs(transform.position.x - targetPosition.x) + Mathf.Abs(transform.position.z - targetPosition.z) < 0.5f){
	// 		// 	transform.position = targetPosition;
	// 		// 	move = false;
	// 		// }
	// 	}else if(transform.position == targetPosition){
	// 		// Debug.Log("Start of if loop");
	// 		float direction_predictor = Random.Range(0.0f, 1.0f);
			
	// 		if(direction_predictor < 0.25f){
	// 			transform.eulerAngles = new Vector3(0,0,0);
	// 			movementDirection = new Vector3(1.0f, 0, 0);
	// 		}else if(direction_predictor < 0.5f){
	// 			transform.eulerAngles = new Vector3(0,180.0f,0);
	// 			movementDirection = new Vector3(-1.0f, 0, 0);
	// 		}else if(direction_predictor < 0.75f){
	// 			transform.eulerAngles = new Vector3(0,-90.0f,0);
	// 			movementDirection = new Vector3(0, 0, 1.0f);
	// 		}else{
	// 			transform.eulerAngles = new Vector3(0,90.0f,0);
	// 			movementDirection = new Vector3(0, 0, -1.0f);
	// 		}

	// 		if(space_available(movementDirection)){
	// 			targetPosition = approxPosition + movementDirection;
	// 			move = true;
	// 			Debug.Log("move is true");
	// 		}else{
	// 			move = false;
	// 			Debug.Log("move is false");
	// 		}
				
	// 		//movement(movementDirection, approxPosition, ref om);
			
	// 	}

	// 	// Debug.Log("While loop ended");
	// }

	// // void movement(Vector3 direction, Vector3 approxPosition, ref ObjectMatrix om){
	// // 	Debug.Log("Inside movement function");
	// // 	om = GameObject.Find("GameController").GetComponent<ObjectMatrix>();
	// // 	if(space_available(direction)){
	// // 		transform.eulerAngles = new Vector3(0,(direction.x + direction.z) * 90.0f,0);
	// // 		while(transform.position != approxPosition + direction){
	// // 			rb.velocity = direction * movementSpeed;
	// // 		}
	// // 		return;
	// // 	}else return;
	// // }	


}