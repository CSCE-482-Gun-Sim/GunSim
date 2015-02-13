using UnityEngine;
using System.Collections;

public class Pistol : AbstractGun {

	// Use this for initialization
	void Start () {
		defineWeaponProperties();
		base.Start ();
	}

	//required of any class inheriting from AbstractGun
	protected override void defineWeaponProperties(){
		this.gunHeight = 0.14f;
		this.barrel_offset = new Vector3 (0, 0.0521f, 0.1702f);
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}
}
