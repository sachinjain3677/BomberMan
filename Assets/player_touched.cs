using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_touched : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			transform.gameObject.tag = "Untagged";
		}
	}
}
