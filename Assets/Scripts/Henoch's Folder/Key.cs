using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PickUpable {
	public PickUpObject player;


	// Use this for initialization
	void Start () {
		rb.constraints = RigidbodyConstraints.FreezeAll;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (rb.isKinematic == true) {
			this.gameObject.SetActive (false);
			player.haveKey = true;
			player.isCarrying = false;
			player.carriedObject = null;
		}
	}
}
