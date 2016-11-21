using UnityEngine;
using System.Collections;

public class WorldSectionManager : MonoBehaviour {

	HazardPool HazPool;
	WallSpawner WSpawner;
	WallPooling WallPool;
	Vector3 LastWallSpawnerPos;
	public WorldSection CurrentSection = WorldSection.EARTH;
	public Transform PlayerTransform;

	public bool Changing = false;
	public enum WorldSection
	{
		EARTH,
		SKY
	}

	public GameObject SectionDividerPrefab;
	GameObject SectionDivider;
	void Start () {
		SectionDivider = Instantiate(SectionDividerPrefab)  as GameObject;
		SectionDivider.SetActive (false);
		WallPool = FindObjectOfType<WallPooling> ();
		WSpawner = FindObjectOfType<WallSpawner> ();
		HazPool = FindObjectOfType<HazardPool> ();
		LastWallSpawnerPos = WSpawner.gameObject.transform.position;
	}

	void Update () {
		if (!Changing && WallPool.GetDivisor().transform.position.y - PlayerTransform.position.y <=2) {
			Changing = true;
			switch (CurrentSection) { 
			case WorldSection.EARTH:
				CurrentSection = WorldSection.SKY;
				ChangeSection (WorldSection.SKY);
				WallPool.ChangeSection (WorldSection.SKY);
				break;
			case WorldSection.SKY:
				CurrentSection = WorldSection.EARTH;
				ChangeSection (WorldSection.EARTH);
				WallPool.ChangeSection (WorldSection.EARTH);
				break;
			}
		}
	}

	void ChangeSection(WorldSection section){
		LastWallSpawnerPos = WSpawner.gameObject.transform.position;
		WSpawner.SectionBuilt = false;
	}
}
