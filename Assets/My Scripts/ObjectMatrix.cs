using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMatrix : MonoBehaviour {

	public int rows;// x direction
	public int columns;// z direction
	public GameObject levelHolder;
	public GameObject[,] level;


	public void getObjectMatrix () {
		//Matrix is filled at the start so explosions instantiated later won't be counted

		level = new GameObject[rows, columns];
		var objects = levelHolder.GetComponentsInChildren<Transform>();
	
		foreach(var child in objects){
			if(child.gameObject.name!="Level" && child.gameObject.name!="SteelBlockMatrix"){
				level[(int)child.position.x, (int)child.position.z] = child.gameObject;
			}
		}
		//Debug.Log(level[0,0]);

		// foreach(var child in level){
		// 	Debug.Log(child);
		// }
	}
}
