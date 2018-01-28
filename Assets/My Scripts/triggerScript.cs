using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerScript : MonoBehaviour {
	public AudioSource music;
	public AudioClip clip;
	void start ()
	{
		clip = music.clip;
	}
	void OnTriggerEnter(Collider collider){
		if(collider.tag == "Player"){
			Destroy(collider.gameObject);
			music.Play ();
		}
	}
}
