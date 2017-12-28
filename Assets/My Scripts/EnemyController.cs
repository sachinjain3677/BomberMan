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
}