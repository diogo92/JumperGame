using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HazardPool : MonoBehaviour {

	int LastSection;
	int CurrentSection;
	public GameObject[] EarthFallingHazardPrefabs; //Earth section falling hazards
	List<GameObject> EarthFallingHazardPool;


	void Start(){
		ChangeSection (1);
	}

	//Switch section of the game to pool new prefabs and delete older
	public void ChangeSection(int section){
		LastSection = CurrentSection;
		CurrentSection = section;
		switch (CurrentSection) {
		case 1:
			PoolEarthObjects ();
			break;
		}
	}

	public GameObject GetFallingHazard(){
		switch (CurrentSection) {
		case 1:
			return GetEarthFallingHazard ();
			break;
		}
		return null;
	}


	public void PoolEarthObjects(){
		EarthFallingHazardPool = new List<GameObject> ();
		for (int i = 0; i < EarthFallingHazardPrefabs.Length; i++) {
			for (int x = 0; x < 2; x++) {
				GameObject obj = Instantiate (EarthFallingHazardPrefabs [i]) as GameObject;
				obj.SetActive (false);
				EarthFallingHazardPool.Add (obj);
			}
		}
	}

	GameObject GetEarthFallingHazard(){
		int index = Random.Range (0, EarthFallingHazardPool.Count);
		bool allEnabled = true;
		for (int i = 0; i < EarthFallingHazardPool.Count; i++) {
			if (!EarthFallingHazardPool [i].activeInHierarchy) {
				allEnabled = false;
				break;
			}
		}
		if (allEnabled)
			return null;
		while (EarthFallingHazardPool [index].activeInHierarchy) {
			index = Random.Range (0, EarthFallingHazardPool.Count);
		}
		return EarthFallingHazardPool [index];
	}
}
