﻿using UnityEngine;
using System.Collections;

public abstract class AbstractGun : Pickupable {
	public Vector3 barrel_offset;
	GameObject player;
	GunSlide gunSlide;
	Pickup_Object playerFunctions;
	public Transform bulletHole;

	// Use this for initialization
	protected void Start () {
		player = GameObject.FindWithTag("Player");
		gunSlide = (GunSlide)GameObject.FindWithTag ("GunSlide").GetComponent(typeof(GunSlide));
		playerFunctions = player.GetComponent<Pickup_Object> ();
	}

	public override void carryUpdate(Hand hand)
	{

		//if gun is shot
		if(((hand.rightHand && Input.GetMouseButtonDown(0)) || (!hand.rightHand && Input.GetMouseButtonDown(1))))
		{
				//Debug.Log(playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward));
			gunSlide.slideAction();
			GetComponent<AudioSource>().Play ();
			Vector3 fwd = /*Quaternion.Euler (this.handRotationOffset) * */ -this.transform.right;
			Ray gunDirection = new Ray(this.gameObject.transform.position /*+ this.gameObject.transform.TransformDirection(barrel_offset)*/, fwd * 50);
			LayerMask layerMask = 1 << 1; //This ignores the SafetyMonitor, meaning the raycast will ignore the range plane when shooting
			RaycastHit hit;

			if(Physics.Raycast (gunDirection, out hit, layerMask))
				{
				Debug.DrawRay(this.gameObject.transform.position /*+ this.gameObject.transform.TransformDirection (barrel_offset)*/, fwd * 10, Color.green, 100);
					
					if(hit.collider.gameObject.tag == "Target")
					{	
						Debug.Log("HIT");
						Quaternion bulletHoleRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
						Instantiate(bulletHole, hit.point, bulletHoleRotation);

					}
				}
			}
	}


	protected void Update () {
		
	}

	//makes object not bounce
	protected void FixedUpdate() {
		try{
			var currentVelocity = GetComponent<Rigidbody>().velocity;
			
			if (currentVelocity.y <= 0f) 
				return;
			
			currentVelocity.y = 0f;
			GetComponent<Rigidbody>().velocity = currentVelocity;
		} catch (UnityException e){

		}

	}
}
