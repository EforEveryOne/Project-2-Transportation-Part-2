using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainScript : MonoBehaviour {

//variables. I have cubePrefab listed in both scripts, is that not needed? It seems not needed, but it doesn't seem to cause any issues.
	public GameObject cubePrefab;
	Vector3 cubePosition;
	public static GameObject clickedCube;
	public static GameObject airplaneCube;
	int airplaneX, airplaneY;

// this is a built in function, everthing starts to happen after this.
	void Start () {

//starting coordinates for a plane
		airplaneX = 0;
		airplaneY = 8;

// This is code we did in class to create the cube grid.
		for (int y = 0; y < 9; y++) {
			for (int x = 0; x < 16; x++) {
				cubePosition = new Vector3 (x * 2 - 15, y * 2 - 8, 0);
				clickedCube = Instantiate (cubePrefab, cubePosition, Quaternion.identity);
				//clickedCube.GetComponent<CubeBehavior> (). x = x;
				//clickedCube.GetComponent<CubeBehavior> (). y = y;
				clickedCube.GetComponent<Renderer> ().material.color = Color.white;

// Making the cube at the coordinates into an inactive airplane
				if (x == airplaneX && y == airplaneY) {
					clickedCube.GetComponent<CubeBehavior> ().airPlane = true;
					clickedCube.GetComponent<Renderer> ().material.color = Color.red;
				}
			}
		}
	}
	// This is a function. No more confusion between functions and variables! When the mouse is clicked on a cube (checked by the script on each cube) the following will happen in order
	public static void ProcessClick (GameObject clickedCube) {
// This is where the magic happens.  If the clicked cube is an airplane (bool) but it is not an activeAirPlane (bool) it'll change into an active one. It turns green and gets a little bigger.
// Red is stop. Green is go. Airplane cube is assigned to clicked cube so it still functions with OnMouseDown (I had a lot of troubleshooting with this).
		if (clickedCube.GetComponent<CubeBehavior> ().airPlane == true && clickedCube.GetComponent<CubeBehavior> ().activeAirPlane == false) {
			clickedCube.GetComponent<Renderer> ().material.color = Color.green;
// Transform.localScale, from unity documentation.
			clickedCube.transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
			clickedCube.GetComponent<CubeBehavior> ().activeAirPlane = true;
			airplaneCube = clickedCube;
		}
// If the cube clicked is an airPlane && it is an activeAirPlane, it will turn lose activeAirPlane, turn red, and shrink back to normal size. Indicating it is off. It can no longer move.
		else if (clickedCube.GetComponent<CubeBehavior> ().airPlane == true && clickedCube.GetComponent<CubeBehavior> ().activeAirPlane == true) {
			airplaneCube.GetComponent<CubeBehavior> ().activeAirPlane = false;
			airplaneCube.GetComponent<Renderer> ().material.color = Color.red;
			airplaneCube.transform.localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
			airplaneCube = null;
		}
// If the cube is not an airplane (and not an active one!) it must be a skycube that was clicked and so if the airplane cube is not null, the current airplane will be "transformed" into a skyCube and the the clicked skyCube will "transform" into the activeAirPlane!
		else if (clickedCube.GetComponent<CubeBehavior> ().airPlane == false && clickedCube.GetComponent<CubeBehavior> ().activeAirPlane == false) {

			if (airplaneCube != null) {
				airplaneCube.GetComponent<Renderer> ().material.color = Color.white;
				airplaneCube.transform.localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				airplaneCube.GetComponent<CubeBehavior> ().airPlane = false;
				airplaneCube.GetComponent<CubeBehavior> ().activeAirPlane = false;
				airplaneCube = null;

				clickedCube.GetComponent<Renderer> ().material.color = Color.green;
				clickedCube.transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
				clickedCube.GetComponent<CubeBehavior> ().airPlane = true;
				clickedCube.GetComponent<CubeBehavior> ().activeAirPlane = true;
				airplaneCube = clickedCube;
		}
	}
	}
// this is a built in function that checks for updates every frame. No need for it at this stage of the project.
   void Update () {
		}
}