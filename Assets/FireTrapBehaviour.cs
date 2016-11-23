using UnityEngine;
using System.Collections;

public class FireTrapBehaviour : MonoBehaviour {

	public GameObject FirePrefab;
	//GameObject InstantiatedParticleSystem;
	bool Firing = false;
	float currTime = 0f;
	float timeToFire = 0f;
	float timeToStop = 5f;
	void Start () {
		timeToFire = Random.Range (5f, 10f);
		FirePrefab.SetActive (false);
	/*	InstantiatedParticleSystem = Instantiate (FirePrefab) as GameObject;
		InstantiatedParticleSystem.transform.position = transform.position;
		InstantiatedParticleSystem.SetActive (false);*/
	}
	
	void Update () {
		currTime += Time.deltaTime;
		if (!Firing) {
			if (currTime >= timeToFire) {
				Firing = true;
				currTime = 0;
				FirePrefab.SetActive (true);
				//InstantiatedParticleSystem.SetActive (true);
			}
		}
		if (Firing) {
			if (currTime >= timeToStop) {
				Firing = false;
				currTime = 0;
				FirePrefab.SetActive (false);
				//InstantiatedParticleSystem.SetActive (false);
			}
		}
	}
}
