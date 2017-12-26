using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_pick_up : MonoBehaviour {

	

	void OnTriggerEnter(Collider collider){
		if(collider.tag=="power_up"){
			Destroy(collider.gameObject);
			//DO SOME ANIMATION STUFF
		}
	}
}
