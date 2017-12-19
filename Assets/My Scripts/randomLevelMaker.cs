using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomLevelMaker : MonoBehaviour {

	ObjectMatrix om;
	int rows;
	int columns;

	public GameObject woodenBox;
	public GameObject woodenBoxPowerUp;
	public float selectorThreshold;
	public GameObject imageTarget;
	public int noOfPowerUps;


	public void generateLevel() {
		om = GameObject.Find("GameController").GetComponent<ObjectMatrix>();
		rows = om.rows;
		columns = om.columns;
		// Debug.Log(rows);
		// Debug.Log(columns);

		for(int i=0; i<rows-1;i++){
			for(int j=0;j<columns-1;j++){
				//Debug.Log("i: "+i+", "+"j: "+j);
				if(i==0 && j==0 || i==1 && j==0 || i==0 && j==1){
					continue;
				}
				if(om.level[i,j] == null){
					float selector = Random.Range(0.0f, 1.0f);
					if(selector < selectorThreshold){
						GameObject spawnedWoodenBox = Instantiate(woodenBox, new Vector3(i, 0, j), Quaternion.identity);
						om.level[i,j] = spawnedWoodenBox.gameObject;
						spawnedWoodenBox.transform.parent = imageTarget.transform;
						//Debug.Log("Yes!!!");
					}else{
						//Debug.Log("No!!!");
					}
				}
			}
		}
	}
}
