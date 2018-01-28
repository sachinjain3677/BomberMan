using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTriggerScript : MonoBehaviour {
	public AudioSource music,music2;
	public AudioClip clip,clip2;
	void Start () {
		clip = music.clip;
		clip2 = music2.clip;
		music.Play ();
		music2.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider collider){
		GetComponent<BoxCollider>().isTrigger = false;
	}
}
