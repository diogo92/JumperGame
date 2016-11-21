/*
 *	Misc helper functions
 *	Created by Diogo Ribeiro - 2016
 */ 

using UnityEngine;
using System.Collections;

public class SharedFunctions {

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < 90 || angle > 270) {       // if angle in the critic region...
			if (angle > 180)
				angle -= 360;  // convert all angles to -180..+180
			if (max > 180)
				max -= 360;
			if (min > 180)
				min -= 360;
		}    
		angle = Mathf.Clamp (angle, min, max);
		if (angle < 0)
			angle += 360;  // if angle negative, convert to 0..360
		return angle;
	}

	public static float DeviceDiagonalSizeInInches ()
	{
		float screenWidth = Screen.width / Screen.dpi;
		float screenHeight = Screen.height / Screen.dpi;
		float diagonalInches = Mathf.Sqrt (Mathf.Pow (screenWidth, 2) + Mathf.Pow (screenHeight, 2));
		return diagonalInches;
	}
}


