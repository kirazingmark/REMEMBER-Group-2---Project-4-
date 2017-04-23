using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : PickUpable {
	public PickUpObject player;
	public PhasingScript ps;

	// Use this for initialization
	void Start () {
		rb.constraints = RigidbodyConstraints.FreezeAll;

	}

	// Update is called once per frame
	void Update () {
		if (rb.isKinematic == true) {
			this.gameObject.SetActive (false);
			ps.getMirror = true;
			player.isCarrying = false;
			player.carriedObject = null;
		}
	}
}
