using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombSpawnAndExplode : MonoBehaviour {

	Vector3 bombSpawn;
	float lastSpawnTime;
	float nextSpawnTime;
	ObjectMatrix om;
	randomLevelMaker rlm;
	int rows;
	int columns;

	public GameObject power_up;
	public GameObject bomb;
	public float bombWaitTime;
	public GameObject player;
	public GameObject fireCenter;
	public GameObject fireSidewaysNoSmoke;
	public int explosionSpread;//if zero then explosion only in one cube, i.e. center
	public float explosionWaitTime;
	public float explosionStay;
	public float blockStayInExplosion;
	public GameObject broken_wooden_crate;
	public float broken_wooden_crate_stay_time;
	public float enemy_stay_in_explosion_time;

	void Start(){
		lastSpawnTime=nextSpawnTime=0.0f;
		om = GameObject.Find("GameController").GetComponent<ObjectMatrix>();
		om.getObjectMatrix();
		rows = om.rows;
		columns = om.columns;
		rlm = GameObject.Find("GameController").GetComponent<randomLevelMaker>();
		rlm.generateLevel();
	}
	
	void FixedUpdate(){
		//position of bomb 
		bombSpawn = new Vector3(Mathf.Round(player.transform.position.x), Mathf.Round(player.transform.position.y), Mathf.Round(player.transform.position.z));
		
		if(Input.GetKeyDown(KeyCode.F) && Time.time > nextSpawnTime && om.level[(int)bombSpawn.x, (int)bombSpawn.z]==null){
			lastSpawnTime = Time.time;//time of last bomb spawn
			nextSpawnTime = lastSpawnTime + bombWaitTime;//predicted time after which bomb spawn is possible
			GameObject spawnedBomb = (GameObject)Instantiate(bomb, bombSpawn, Quaternion.identity);//spawned bomb object
			om.level[(int)spawnedBomb.transform.position.x, (int)spawnedBomb.transform.position.z] = spawnedBomb.gameObject;//filling level matrix with the instantiated bomb
			Destroy(om.level[(int)spawnedBomb.transform.position.x, (int)spawnedBomb.transform.position.z],3);//destroying spawned bomb object
			Destroy(spawnedBomb,3);
			StartCoroutine(explosionSpawn(bombSpawn, spawnedBomb));//coroutine to do all the explosion stuff of the spwaned bomb
		}

	}

	//does all the explosion stuff by taking bomb position as explosion center
	IEnumerator explosionSpawn(Vector3 bombSpawnCenter, GameObject spawnedBomb){
		yield return new WaitForSeconds(3.0f);//made to wait 3 sec so that bomb can explode
		bool forward = true;//true if explosion spawn allowed in +z
		bool back = true;//true if explosion spawn allowed in -z
		bool left = true;//true if explosion spawn allowed in -x
		bool right = true;//true if explosion spawn allowed in +x

		bool forwardLast = false;//true if current explosion is last explosion in +z
		bool backLast = false;//true if current explosion is last explosion in -z
		bool leftLast = false;//true if current explosion is last explosion in -x
		bool rightLast = false;//true if current explosion is last explosion in +x


		GameObject explosionCenter = (GameObject)Instantiate(fireCenter, bombSpawnCenter + Vector3.up * 0.11f, Quaternion.identity);
		//spawned explosion at the origin of the explosion or the position of the bomb

		Destroy(explosionCenter, explosionStay);//destroying spawned explosion game object
		yield return new WaitForSeconds(explosionWaitTime);//time gap between two consecutive explosions to illusion of moving outward

		for(int i=1; i<=explosionSpread; i++){
			//forward->positive Z
			//backward->negative Z
			//left->negative X
			//right->positive X


			if(i == explosionSpread){
				forwardLast = true;
				backLast = true;
				leftLast = true;
				rightLast = true;
			}//last explosion in all directions


			if(!(bombSpawnCenter.z + Vector3.forward.z *(i+1) <= columns-2)){
				forwardLast = true;
			}//last explosion in forward direction if encountered wall

			if(!(bombSpawnCenter.z + Vector3.back.z *(i+1) >= 0)){
				backLast = true;
			}//last explosion in backward direction if encountered wall

			if(!(bombSpawnCenter.x + Vector3.left.x *(i+1) >= 0)){
				leftLast = true;
			}//last explosion in left direction if encountered wall
			
			if(!(bombSpawnCenter.x + Vector3.right.x *(i+1) <= rows-2)){
				rightLast = true;
			}//last explosion in right direction if encountered wall


			//following four conditions all spawning explosions after checking availability of space
			if(forward && bombSpawnCenter.z + Vector3.forward.z *i <= columns-2)
				spawnExplosion(bombSpawnCenter + Vector3.forward *i + Vector3.up * 0.11f, Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f)),ref forward, forwardLast);

			if(back && bombSpawnCenter.z + Vector3.back.z *i >= 0 )
				spawnExplosion(bombSpawnCenter + Vector3.back *i + Vector3.up * 0.11f, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)),ref back, backLast);

			if(left && bombSpawnCenter.x + Vector3.left.x *i >= 0 )
				spawnExplosion(bombSpawnCenter + Vector3.left *i + Vector3.up * 0.11f, Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f)),ref left, leftLast);

			if(right && bombSpawnCenter.x + Vector3.right.x *i <= rows-2)	
				spawnExplosion(bombSpawnCenter + Vector3.right *i + Vector3.up * 0.11f, Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f)),ref right, rightLast);

			yield return new WaitForSeconds(explosionWaitTime);
		}
	}



	void spawnExplosion(Vector3 position, Quaternion rotation, ref bool spawnCheck, bool last){
		//spawnCheck is the permission for the explosion to be spawned in that direction

		// GameObject fireToBeSpawned;

		// if(!last){
		// 	fireToBeSpawned = fireSidewaysNoSmoke;	
		// }else{
		// 	fireToBeSpawned = fireSidewaysSmoke;	
		// }//game object to be spawned as explosions changes type if last in its direction


		if(om.level[(int)position.x, (int)position.z]==null){//if position in level matrix is empty
			GameObject spawnedExplosion = (GameObject)Instantiate(fireSidewaysNoSmoke, position, rotation);//spawn fire/explosion
			Destroy(spawnedExplosion, explosionStay);//destroy fire/explosion
		}else{
			//if position in level matrix not empty but could also be due to wooden box that needs to be destroyed if comes in explosion's range
			if(om.level[(int)position.x, (int)position.z].gameObject.tag=="woodenBox"){
				//GameObject spawnedExplosion = (GameObject)Instantiate(fireSidewaysSmoke, position, rotation);//spawn explosion spawned on end points of explosion/fire as explosion/fire chain terminates here
				//Destroy(spawnedExplosion, explosionStay);//destroy the explosion/fire
				GameObject spawnedExplosion = (GameObject)Instantiate(fireSidewaysNoSmoke, position, rotation);//spawn fire/explosion
				Destroy(spawnedExplosion, explosionStay);//destroy fire/explosion
				Destroy(om.level[(int)position.x, (int)position.z].gameObject, blockStayInExplosion);//destroying the wooden block
				om.level[(int)position.x, (int)position.z]=null;//emptying the place in level matrix as wooden box is destroyed
				GameObject spawned_broken_crate = (GameObject)Instantiate(broken_wooden_crate,position,Quaternion.identity);
				Destroy(spawned_broken_crate, broken_wooden_crate_stay_time);
				spawnCheck = false;//no more permission to spawn explosion in this direction
			

			}else if(om.level[(int)position.x, (int)position.z].gameObject.tag=="woodenBox_power_up"){

				GameObject spawnedExplosion = (GameObject)Instantiate(fireSidewaysNoSmoke, position, rotation);//spawn fire/explosion
				Destroy(spawnedExplosion, explosionStay);//destroy fire/explosion
				Destroy(om.level[(int)position.x, (int)position.z].gameObject, blockStayInExplosion);
				GameObject spawned_power_up = (GameObject)Instantiate(power_up, position, Quaternion.identity);
				om.level[(int)position.x, (int)position.z] = spawned_power_up.gameObject;
				GameObject spawned_broken_crate = (GameObject)Instantiate(broken_wooden_crate,position,Quaternion.identity);
				Destroy(spawned_broken_crate, broken_wooden_crate_stay_time);
				spawnCheck = false;//no more permission to spawn explosion in this direction


			}else if(om.level[(int)position.x, (int)position.z].gameObject.tag=="Enemy"){
			

				GameObject spawnedExplosion = (GameObject)Instantiate(fireSidewaysNoSmoke, position, rotation);//spawn fire/explosion
				Destroy(spawnedExplosion, explosionStay);//destroy fire/explosion
				Destroy(om.level[(int)position.x, (int)position.z].gameObject, enemy_stay_in_explosion_time);
				om.level[(int)position.x, (int)position.z]=null;//emptying the place in level matrix as wooden box is destroyed
				//no spawnCheck = false in this case as explosion will pass through enemy
				//DO STUFF
			
			}else if(om.level[(int)position.x, (int)position.z].gameObject.tag=="bomb"){
			

				GameObject spawnedExplosion = (GameObject)Instantiate(fireSidewaysNoSmoke, position, rotation);//spawn fire/explosion
				Destroy(spawnedExplosion, explosionStay);//destroy fire/explosion
				//no spawnCheck = false in this case as explosion will pass through bomb
			
			}else{
				spawnCheck = false;
			}

			
		}
	}
}
