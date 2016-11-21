/*
 *	Player input handler
 *	Created by Diogo Ribeiro - 2016
 */

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	MovementManager movementManager;

	void Start(){
		movementManager = GetComponent<MovementManager> ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			movementManager.Jump ();
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.RightArrow)) {
			movementManager.Jump ();
		}
	}
}
