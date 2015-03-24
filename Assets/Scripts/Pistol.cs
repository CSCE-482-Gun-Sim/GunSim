using UnityEngine;
using System.Collections;

public class Pistol : AbstractGun {
	//public Vector3 handRotationOffset; // <===== changed the hand rotation offset from (90,90,0) to (0,0,0) so that shooting works until we fix the issue, needed to have it working for testing new stuff
										 // We'll want to apply a rotation of (0,-90,0) to fix the issue later, dont wanna right now
	// Use this for initialization
	void Start () {
		base.Start ();
		this.renderer.material.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	void FixedUpdate() {
		base.FixedUpdate();
	}
}
