using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTriggerScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider collider){
		GetComponent<BoxCollider>().isTrigger = false;
	}
}
