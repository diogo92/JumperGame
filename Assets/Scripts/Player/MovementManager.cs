/*
 *	Player movement handler
 *	Created by Diogo Ribeiro - 2016
 */

using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour {

	//Wall Anchor defines which wall the player is anchored to
	public enum WallAnchor{
		LeftSide,
		RightSide
	}
	WallAnchor PlayerAnchor;
	Vector3 NewPosition;
	Transform CurrentWallHit;
	Vector3 WallCollisionPoint;
	Vector3 NewRotation;
	//Components
	Animator anim;
	Rigidbody rb;
	PhysicsVariablesDefinitions Variables;
	CameraFollow cameraFollow;

	//Check if the player is currently mid jump
	bool isJumping = false;

	//Check if player hit a wall
	bool hitWallLeft=false;
	bool hitWallRight=false;


	void Start () {
		cameraFollow = Camera.main.GetComponent<CameraFollow> ();
		Variables = GetComponent<PhysicsVariablesDefinitions> ();
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		transform.eulerAngles = Variables.RightOrientationEulerAngles;
		//Randomize starting anchor side
		int randomSide = Random.Range (0, 2);
		if (randomSide == 0) {
			PlayerAnchor = WallAnchor.RightSide;
			transform.eulerAngles = Variables.RightOrientationEulerAngles;
		} else {
			PlayerAnchor = WallAnchor.LeftSide;
			transform.eulerAngles = Variables.LeftOrientationEulerAngles;
		}
		NewRotation = transform.eulerAngles;
		Jump ();
	}
	//Function called by the input manager
	public void Jump(){
		if (!isJumping) {
			anim.SetTrigger ("Jump");
			rb.velocity = Variables.JumpUpVector;
			isJumping = true;
			if (PlayerAnchor == WallAnchor.LeftSide) {
				StartCoroutine("JumpToRight");
			}
			else{
				StartCoroutine("JumpToLeft");
			}
		}
	}

	IEnumerator JumpToLeft(){
		transform.eulerAngles = Variables.LeftOrientationEulerAngles;
		transform.position += Variables.JumpLeftVector * Time.deltaTime;
		while(!hitWallLeft){
			transform.position += Variables.JumpLeftVector * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		isJumping = false;
		NewPosition = transform.position;
		NewPosition.z = CurrentWallHit.position.z + (1 - Variables.WallOffset);
		transform.position = WallCollisionPoint;

		PlayerAnchor = WallAnchor.LeftSide;
	}

	IEnumerator JumpToRight(){
		transform.eulerAngles = Variables.RightOrientationEulerAngles;
		transform.position += Variables.JumpRightVector * Time.deltaTime; 
		while(!hitWallRight){
			transform.position += Variables.JumpRightVector * Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		isJumping = false;
		NewPosition = transform.position;
		NewPosition.z = CurrentWallHit.position.z - Variables.WallOffset;
		transform.position = WallCollisionPoint;
		PlayerAnchor = WallAnchor.RightSide;
	}


	//Collision Handler
	//Sets player and camera positions
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "AnchorLeft") {
			cameraFollow.MoveToZCoordinate(collision.gameObject.transform.parent.position.z + collision.gameObject.GetComponentInParent<WallCoordinates> ().ZSpawnerCoordinate);
			hitWallLeft = true;
			CurrentWallHit = collision.gameObject.transform;
			WallCollisionPoint = collision.contacts[(collision.contacts.Length-1)/2].point;
			cameraFollow.TargetY = WallCollisionPoint.y + 1f;
			NewRotation = transform.eulerAngles;
			NewRotation.x = collision.gameObject.GetComponentInParent<WallCoordinates> ().XPlayerEulerAngle;
			transform.eulerAngles = NewRotation;
		} else
			hitWallLeft = false;
		if (collision.gameObject.tag == "AnchorRight") {
			cameraFollow.MoveToZCoordinate(collision.gameObject.transform.parent.position.z + collision.gameObject.GetComponentInParent<WallCoordinates> ().ZSpawnerCoordinate);
			hitWallRight = true;
			CurrentWallHit = collision.gameObject.transform;
			WallCollisionPoint = collision.contacts[(collision.contacts.Length-1)/2].point;
			cameraFollow.TargetY = WallCollisionPoint.y + 1f;
			NewRotation = transform.eulerAngles;
			NewRotation.x = -collision.gameObject.GetComponentInParent<WallCoordinates> ().XPlayerEulerAngle;
			transform.eulerAngles = NewRotation;
		} else
			hitWallRight = false;
	}
}
