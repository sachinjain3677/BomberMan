using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomLevelMaker : MonoBehaviour {

	ObjectMatrix om;
	int rows;
	int columns;
	int enemiesSpawned;
	int[] enemy_position_x;
	int[] enemy_position_z;
	//EnemyController[] enemy_movement_script_list;
	int power_ups_spawned;
	int woodenBox_spawn_count;


	public GameObject woodenBox;
	public float selectorThreshold;
	public GameObject level;
	public GameObject enemy;
	public int max_no_of_enemies;
	public int max_no_of_power_ups;
	public float distancePerEnemy_x;//actual distance in units is obtained by multiplying this by skipFactor
	public float distancePerEnemy_z;//actual distance in units is obtained by multiplying this by skipFactor
	public int skipFactor;
	public int enemies_per_section;


	void Start(){
		power_ups_spawned = 0;
		woodenBox_spawn_count = 0;
	}


	public void generateLevel() {
		//enemy_movement_script_list = new EnemyController[max_no_of_enemies];
		enemiesSpawned = 0;//no of enemies spawned so far
		om = GameObject.Find("GameController").GetComponent<ObjectMatrix>();
		rows = om.rows;//rows on the game field
		columns = om.columns;//columns on the game field
		int enemySections_x = rows/((int)distancePerEnemy_x*skipFactor);//no of sections in x direction in which game field is divided
		int enemySections_z = columns/((int)distancePerEnemy_z*skipFactor);//no of sections in z direction in which game field is divided
		//NOTE: enemies_per_section NUMBER OF ENEMIES ARE SPAWNED IN ONE SECTION, SAME FOR ALL SECTIONS 
		

		max_no_of_enemies = (max_no_of_enemies < enemySections_z + enemySections_z)? enemySections_z + enemySections_z:max_no_of_enemies; 

		enemy_position_x = new int[max_no_of_enemies];//array containing x-coordinates of enemies spawn positions
		enemy_position_z = new int[max_no_of_enemies];//array containing z-coordinates of enemies spawn positions
		Debug.Log("enemySections_x: "+enemySections_x);
		Debug.Log("enemySections_z: "+enemySections_z);

		for(int i=0; i<max_no_of_enemies; i++){
			enemy_position_x[i] = (int)Random.Range(0.0f, distancePerEnemy_x);//random numbers are used as x-coordinates
			enemy_position_z[i] = (int)Random.Range(0.0f, distancePerEnemy_z);//random numbers are used as z-coordinates
		}//this has generated two random numbers for each enemy

		for(int i=0; i<enemySections_x; i++){
			for(int j=0; j<enemySections_z; j++){
				Debug.Log("i: "+i);
				Debug.Log("j: "+j);
				if(i==0 && j==0){
					continue;
				}

				if(enemiesSpawned == max_no_of_enemies){
					break;
				}

				int x_coordinate = skipFactor*((int)distancePerEnemy_x*(i) + enemy_position_x[i+j]);
				int z_coordinate = skipFactor*((int)distancePerEnemy_z*(j) + enemy_position_z[i+j]);
				Debug.Log("x_coordinate: "+x_coordinate);
				Debug.Log("z_coordinate: "+z_coordinate);
				if(x_coordinate <= rows-2 && z_coordinate <= columns-2 && om.level[x_coordinate, z_coordinate] == null){
					GameObject enemySpawned = Instantiate(enemy, new Vector3(x_coordinate, 0, z_coordinate), Quaternion.identity);
					om.level[x_coordinate, z_coordinate] = enemySpawned.gameObject;
					//enemy_movement_script_list[enemiesSpawned] = enemySpawned.GetComponent<EnemyController>();
					enemiesSpawned++;
				}
			}
			
			if(enemiesSpawned == max_no_of_enemies){
					break;
			}	
		}		


		for(int i=0; i<rows-1; i++){
			for(int j=0; j<columns-1; j++){
				
				if(i==0 && j==0 || i==1 && j==0 || i==0 && j==1){
					continue;
				}//used to clear a certain area near the player
				
				if(om.level[i,j] == null){
					float selector = Random.Range(0.0f, 1.0f);
					if(selector < selectorThreshold){
						GameObject spawnedWoodenBox = Instantiate(woodenBox, new Vector3(i, 0, j), Quaternion.identity);
						om.level[i,j] = spawnedWoodenBox.gameObject;
						woodenBox_spawn_count++;
						if(woodenBox_spawn_count == 1){
							spawnedWoodenBox.transform.gameObject.tag = "woodenBox_power_up";//set tag of wooden box with power inside it as woodenBox_power_up
							power_ups_spawned++;
						}
						spawnedWoodenBox.transform.parent = level.transform;
					}
				}
			}
		}


		// for(int i=0;i<enemiesSpawned;i++){
		// 	enemy_movement_script_list[i].enabled = true;
		// }
	}
}
