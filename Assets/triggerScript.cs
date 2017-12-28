using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerScript : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
		if(collider.tag == "Player"){
			Destroy(collider.gameObject);
		}
	}
}
