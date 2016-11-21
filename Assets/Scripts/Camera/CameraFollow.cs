using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float TargetY;
	public float TargetZ;
	public Transform PlayerTransform;
	Vector3 NewPosition;
	public float damping = 1;
	public Vector3 offset;
	[AddComponentMenu("Camera-Control/Smooth Follow")]

	void Start(){
		NewPosition = Vector3.zero;
	}
	void LateUpdate() {
		float currentAngle = transform.eulerAngles.y;
		//float desiredAngle = PlayerTransform.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle(currentAngle, 0, damping);

		Quaternion rotation = Quaternion.Euler(0, angle, 0);
		NewPosition.x = PlayerTransform.transform.position.x;
		//NewPosition.y = PlayerTransform.transform.position.y;
		NewPosition.y = Mathf.MoveTowards(NewPosition.y, TargetY,Time.deltaTime*2);
		NewPosition.z = Mathf.MoveTowards(NewPosition.z, TargetZ,Time.deltaTime*2);
		transform.position = NewPosition + (rotation * offset);
		transform.LookAt(PlayerTransform.transform);
		float yAngle = SharedFunctions.ClampAngle(transform.eulerAngles.y,248.975f,-69.82f+360f);
		transform.eulerAngles = new Vector3 (55, yAngle, transform.eulerAngles.z);
	}
		

	public void MoveToZCoordinate(float newZ){
		//StartCoroutine(MoveCameraTo(newZ));
		TargetZ=newZ;
	}

	IEnumerator MoveCameraTo(float newZ){
		Vector3 CurrVector;
		Vector3 targetVector = NewPosition;
		targetVector.z = newZ;
		while(Mathf.Abs(NewPosition.z - newZ) >0.05f){
			CurrVector = NewPosition;
			CurrVector = Vector3.Lerp (NewPosition, targetVector, Time.deltaTime*2);
			NewPosition = CurrVector;
			yield return new WaitForEndOfFrame ();
		}
	}


}
