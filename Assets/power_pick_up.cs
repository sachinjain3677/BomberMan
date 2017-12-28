using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_pick_up : MonoBehaviour {

	BombSpawnAndExplode bsae;

	void Start(){
		bsae = GameObject.Find("GameController").GetComponent<BombSpawnAndExplode>();
	}


	void OnTriggerEnter(Collider collider){

		if(collider.tag=="power_up"){
			Destroy(collider.gameObject);
			bsae.explosionSpread++;
			//DO SOME ANIMATION STUFF
		}
	}
}
