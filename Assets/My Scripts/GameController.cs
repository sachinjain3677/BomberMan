using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//rename to game Controller
public class GameController : MonoBehaviour {

	Vector3 bombSpawn;
	float tempTime;
	
	public GameObject bomb;
	public float bombWaitTime;
	public GameObject player;
	
	// Use this for initialization
	void Start(){
		tempTime = bombWaitTime;
	}
	
	void FixedUpdate(){
		bombSpawn = player.transform.position;
		bombSpawn += Vector3.up * 0.35f;
		
		if(Input.GetKeyDown(KeyCode.F) && Time.time > tempTime){
			//Debug.Log(Time.time);
			tempTime += bombWaitTime;
			GameObject spawnedBomb = (GameObject)Instantiate(bomb, bombSpawn, player.transform.rotation);
			
			Destroy(spawnedBomb,3);
		}

	}
}
