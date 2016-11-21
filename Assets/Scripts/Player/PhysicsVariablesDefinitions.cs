/*
 * Physics variables definitions
 * Created by Diogo Ribeiro - 2016
 */ 

using UnityEngine;
using System.Collections;
/*
 * I store variables related to player physics in order to keep the code cleaner.
 * The class inherits MonoBehaviour so we are able to change these variables through
 * other game mechanics, like powerups.
 */ 
public class PhysicsVariablesDefinitions : MonoBehaviour {
	
	//Jump velocity 
	public float zVelocity = 2f;
	public float yVelocity = 2f;

	//Jump movement vectors
	public Vector3 JumpUpVector;
	public Vector3 JumpLeftVector;
	public Vector3 JumpRightVector;

	public Vector3 RightOrientationEulerAngles;
	public Vector3 LeftOrientationEulerAngles;

	//Offset to avoid colliders getting stuck
	public float WallOffset = 1f;
	void Awake () {
		RightOrientationEulerAngles = new Vector3 (0, 0, 0);
		LeftOrientationEulerAngles = new Vector3 (0, 180, 0);
		JumpUpVector = new Vector3 (0, yVelocity, 0);
		JumpLeftVector = new Vector3 (0, 0, -zVelocity);
		JumpRightVector = new Vector3 (0, 0, zVelocity);
	}
}
