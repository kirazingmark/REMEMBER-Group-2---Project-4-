using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

	//basic variables
	public Light flashlight;
	public float battery = 10;

	// Use this for initialization
	void Start () {
		//starting with lights off
		flashlight.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		//turning the light on and off

		//turning on
		if (Input.GetKeyDown (KeyCode.F) && flashlight.enabled == false) {
			flashlight.enabled = true;
		} 
		else if (Input.GetKeyDown (KeyCode.F) && flashlight.enabled == true) {
			flashlight.enabled = false;
		} 
		//if oin battery decreasing 
		if (flashlight.enabled == true) {
			battery -= Time.deltaTime;
		}
		//if battery dead lights off, battery back to normal
		if (battery <= 0) {
			flashlight.enabled = false;
			battery = 10.0f;
		}
	}
}
