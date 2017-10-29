using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class CubeBehavior : MonoBehaviour {
	public GameObject cubePrefab;
	public int x, y;
	// Both bools start false, airPlane turns on at the start if the cube is in the correct position. It is an inactive plane.
	public bool airPlane = false;
	public bool activeAirPlane = false;

	// When the cube is clicked it'll refer to the function in my MainScript.
	void OnMouseDown () {
			MainScript.ProcessClick (gameObject);
	}
}