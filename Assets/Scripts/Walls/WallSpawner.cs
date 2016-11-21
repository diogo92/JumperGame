using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WallSpawner : MonoBehaviour {

	WallPooling pool;
	public Transform PlayerTransform;
	//Roughly the size of each wall prefab
	Vector3 SpawnOffset;
	Vector3 newPosition;
	//To count active walls
	List<GameObject> ActiveWalls;
	List<int> wallIndexesToRemove;
	GameObject Divisor;
	public bool SectionBuilt = false;
	void Start () {
		wallIndexesToRemove = new List<int> ();
		newPosition = new Vector3 (transform.position.x, 0, 0);
		SpawnOffset = new Vector3 (0, 7, 0);
		pool = GetComponent<WallPooling> ();
		ActiveWalls = new List<GameObject> ();
		BuildAWall ();
	}

	void Update(){
		//Make games great again
		BuildAWall ();
		//RemoveWalls ();
	}

	//The pool will give us a wall, and it will pay for it
	void BuildAWall(){
		if (!SectionBuilt) {
			GameObject WallToBuild = pool.GetWall ();
			if (WallToBuild == null) {
				SectionBuilt = true;
				Divisor = pool.GetDivisor ();
				Divisor.transform.position = transform.position;
				Divisor.SetActive (true);
				return;
			}
			WallToBuild.transform.position = transform.position;
			//WallToBuild.transform.rotation = transform.rotation;
			WallToBuild.SetActive (true);
			ActiveWalls.Add (WallToBuild);
			newPosition.z += WallToBuild.GetComponent<WallCoordinates> ().ZSpawnerCoordinate;
			newPosition.y = transform.position.y + WallToBuild.GetComponent<WallCoordinates> ().YSpawnerCoordinate;
			transform.position = newPosition;
		}
	}
		
	/*void RemoveWalls(){
		for (int i = 0; i < ActiveWalls.Count; i++) {
			if (ActiveWalls [i].transform.position.y < PlayerTransform.position.y - 20f) {
				ActiveWalls [i].SetActive (false);
				wallIndexesToRemove.Add (i);
			}
		}
		for (int i = 0; i < wallIndexesToRemove.Count; i++) {
			ActiveWalls.RemoveAt (wallIndexesToRemove[i]);
		}
		wallIndexesToRemove.Clear ();
	}*/
}
