using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {

	int l=5;
	int k=6;

	// Use this for initialization
	void Start () {
		
		// func(ref i);
		// Debug.Log("i: "+i);
		// Debug.Log(Vector3.forward.z);
		l = (l<k)?k:l;
		Debug.Log("Temp_l: "+l);

	}
	
	// Update is called once per frame
	void func (ref int a) {
		a=6;
	}
}
