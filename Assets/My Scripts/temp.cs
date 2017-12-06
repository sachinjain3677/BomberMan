using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {

	int i=5;

	// Use this for initialization
	void Start () {
		// func(ref i);
		// Debug.Log("i: "+i);
		Debug.Log(Vector3.forward.z);
	}
	
	// Update is called once per frame
	void func (ref int a) {
		a=6;
	}
}
