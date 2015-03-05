using UnityEngine;
using System.Collections;

public class Pistol : AbstractGun {
	//public Vector3 handRotationOffset;

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
