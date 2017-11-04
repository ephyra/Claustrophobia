using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingScript : MonoBehaviour {

	public Light myLight;
	public float angleInitialDecrement;
	public float angleDecrementRamping;
	public float intensityDecrement;


	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light> ();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void shiftLight(float amt, string direction) {
		switch (direction)
		{
		case "Left":
			transform.localPosition += new Vector3(amt, 0, 0);
			break;
		case "Right":
			transform.localPosition -= new Vector3(amt, 0, 0);
			break;
		case "Bottom":
			transform.localPosition += new Vector3(0, amt, 0);
			break;
		case "Top":
			transform.localPosition -= new Vector3(0, amt, 0);
			break;
		default:
			break;

		}
		myLight.spotAngle -= angleInitialDecrement;
		angleInitialDecrement += angleDecrementRamping;
		myLight.intensity -= intensityDecrement;
	
	}

}
