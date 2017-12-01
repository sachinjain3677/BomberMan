using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//rename to game Controller
public class GameController : MonoBehaviour {

	Vector3 bombSpawn;
	float lastSpawnTime;
	float nextSpawnTime;
	
	public GameObject bomb;
	public float bombWaitTime;
	public GameObject player;
	public GameObject explosion;
	public int explosionSpread;

	// Use this for initialization
	void Start(){
		lastSpawnTime=nextSpawnTime=0.0f;
	}
	
	void FixedUpdate(){
		bombSpawn = player.transform.position;
		bombSpawn += Vector3.up * 0.35f;
		
		if(Input.GetKeyDown(KeyCode.F) && Time.time > nextSpawnTime){
			//Debug.Log(Time.time);
			lastSpawnTime = Time.time;
			nextSpawnTime = lastSpawnTime + bombWaitTime;
			GameObject spawnedBomb = (GameObject)Instantiate(bomb, bombSpawn, player.transform.rotation);
			Destroy(spawnedBomb,3);
			explosionSpawn(bombSpawn, player.transform.rotation);	
			
		}

	}

	void explosionSpawn(Vector3 bombSpawnCenter, Quaternion rotation){
		Instantiate(explosion, bombSpawnCenter, rotation);
		
		for(int i=1; i<explosionSpread; i++){
			Instantiate(explosion, bombSpawnCenter + Vector3.forward *i, rotation);
			Instantiate(explosion, bombSpawnCenter + Vector3.back *i, rotation);
			Instantiate(explosion, bombSpawnCenter + Vector3.left*i, rotation);
			Instantiate(explosion, bombSpawnCenter + Vector3.right *i, rotation);
			
		}
	}
}
