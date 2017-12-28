using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {

	int l=5;
	int k=6;

	// Use this for initialization
	void Start () {
		transform.LookAt(new Vector3(transform.position.x,transform.position.y,transform.position.z+1));	
	}
	
	
}
