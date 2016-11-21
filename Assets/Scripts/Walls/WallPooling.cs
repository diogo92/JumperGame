/*
 * 	Walls Pooling
 * 	Created by Diogo Ribeiro - 2016
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallPooling : MonoBehaviour {


	public int SectionSize;//Number of walls each section will have
	WorldSectionManager SectionManager;
	//Array of the different prefabs
	//Earth Section
	public GameObject[] EarthWallPrefabs;
	List<GameObject> EarthWallPool;
	//Sky Section
	public GameObject[] SkyWallPrefabs;
	List<GameObject> SkyWallPool;

	//Section divisors
	public GameObject[] SectionDivisorPrefabs;
	List<GameObject> SectionDivisors;
	void Awake () {
		SectionManager = FindObjectOfType <WorldSectionManager>();
		EarthWallPool = new List<GameObject> ();
		SkyWallPool = new List<GameObject> ();
		SectionDivisors = new List<GameObject> ();
		ChangeSection (WorldSectionManager.WorldSection.EARTH);
		for (int i = 0; i < SectionDivisorPrefabs.Length; i++) {
			GameObject obj = Instantiate (SectionDivisorPrefabs [i]) as GameObject;
			obj.SetActive (false);
			SectionDivisors.Add (obj);
		}
	}


	void PoolEarthWalls(){
		int indToSpawn = 0;
		for (int x = 0; x < SectionSize; x++) {
			indToSpawn = Random.Range (0, EarthWallPrefabs.Length);
			GameObject obj = Instantiate (EarthWallPrefabs [indToSpawn]) as GameObject;
			obj.SetActive (false);
			EarthWallPool.Add (obj);
		}
	}

	void PoolSkyWalls(){
		int indToSpawn = 0;
		for (int x = 0; x < SectionSize; x++) {
			indToSpawn = Random.Range (0, SkyWallPrefabs.Length);
			GameObject obj = Instantiate (SkyWallPrefabs [indToSpawn]) as GameObject;
			obj.SetActive (false);
			SkyWallPool.Add (obj);
		}
	}


	GameObject GetEarthWall(){
		int index = Random.Range (0, EarthWallPool.Count);
		bool allEnabled = true;
		for (int i = 0; i < EarthWallPool.Count; i++) {
			if (!EarthWallPool [i].activeInHierarchy) {
				allEnabled = false;
				break;
			}
		}
		if (allEnabled)
			return null;
		while (EarthWallPool [index].activeInHierarchy) {
			index = Random.Range (0, EarthWallPool.Count);
		}
		return EarthWallPool [index];
	}

	GameObject GetSkyWall(){
		int index = Random.Range (0, SkyWallPool.Count);
		bool allEnabled = true;
		for (int i = 0; i < SkyWallPool.Count; i++) {
			if (!SkyWallPool [i].activeInHierarchy) {
				allEnabled = false;
				break;
			}
		}
		if (allEnabled)
			return null;
		while (SkyWallPool [index].activeInHierarchy) {
			index = Random.Range (0, SkyWallPool.Count);
		}
		return SkyWallPool [index];
	}

	IEnumerator DestroyOldWalls(List<GameObject> OldWallPool){
		float maxY = 0f;
		for (int i = 0;i<OldWallPool.Count;i++) {
			if (OldWallPool [i].transform.position.y > maxY) {
				maxY = OldWallPool [i].transform.position.y;
			}
		}
		while (Camera.main.transform.position.y < maxY + 15) {
			yield return new WaitForEndOfFrame ();
		}
		for (int i = 0;i<OldWallPool.Count;i++) {
			Destroy (OldWallPool [i]);
		}
		SectionManager.Changing = false;
		OldWallPool.Clear ();
	}

	/*
	 * public functions
	 * 
	 */

	public void ChangeSection(WorldSectionManager.WorldSection NewSection){
		switch (NewSection) {
		case WorldSectionManager.WorldSection.EARTH:
			PoolEarthWalls ();
			StartCoroutine (DestroyOldWalls (SkyWallPool));
			break;
		case WorldSectionManager.WorldSection.SKY:
			PoolSkyWalls ();
			StartCoroutine (DestroyOldWalls (EarthWallPool));
			break;
		}
	}



	public GameObject GetWall(){
		switch (SectionManager.CurrentSection) {
		case WorldSectionManager.WorldSection.EARTH:
			return GetEarthWall ();
		case WorldSectionManager.WorldSection.SKY:
			return GetSkyWall ();
		}
		return GetEarthWall ();
	}

	public GameObject GetDivisor(){
		switch (SectionManager.CurrentSection) {
		case WorldSectionManager.WorldSection.EARTH:
			return SectionDivisors[0];
		case WorldSectionManager.WorldSection.SKY:
			return SectionDivisors[1];
		}
		return SectionDivisors[0];
	}
}
