using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_Over : MonoBehaviour {

	BombSpawnAndExplode bsae;

	public GameObject game_over_menu;
	// Use this for initialization
	void Start () {
		bsae = GameObject.Find ("GameController").GetComponent<BombSpawnAndExplode> ();
		game_over_menu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (bsae.isDead) {
			game_over_menu.SetActive (true);
		}
	}
}
