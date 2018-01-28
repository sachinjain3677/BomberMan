using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTriggerScript : MonoBehaviour {
	public AudioSource music;
	public AudioClip clip;
	void Start ()
	{
		clip = music.clip;
	}
	void OnTriggerEnter(Collider collider){
		if(collider.tag=="Player"){
			Destroy(collider.gameObject);
			music.Play ();
		}

		if(collider.tag=="Enemy"){
			Destroy(collider.gameObject);	
		}
	}

	
}
