using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject levelHolder;
	public GameObject[,] level = new GameObject[10, 10];

	// Use this for initialization
	void Start () {
		var objects = levelHolder.GetComponentsInChildren<Transform>();
	
		foreach(var child in objects){
			if(child.gameObject.name!="Level"){
				level[(int)child.position.x, (int)child.position.z] = child.gameObject;
			}
		}

		foreach(var child in level){
			Debug.Log(child);
		}


		foreach(var child in level){
			if(child!=null){
				Debug.Log("["+child.gameObject.transform.position.x+","+child.gameObject.transform.position.y+","+child.gameObject.transform.position.z+"]");
			}
		}

	}
	
	// Update is called once per frame
	void test () {
			
	}
}
