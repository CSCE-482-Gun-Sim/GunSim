﻿using UnityEngine;
using System.Collections;

public abstract class AbstractGun : Pickupable
{
		public Vector3 barrel_offset;
		GameObject player;
		GunSlide gunSlide;
		Pickup_Object playerFunctions;
		public Transform bulletHole;
		bool TriggerPulled;
		ParticleSystem flash;
		public Magazine loadedMagazine;
		GameObject safety;
		bool safetyOn;
		bool Shoot;
		Color safetyOffColor = new Color (.1f, .1f, .1f);

		// Use this for initialization
		protected void Start ()
		{
				player = GameObject.FindWithTag ("Player");
				gunSlide = (GunSlide)GameObject.FindWithTag ("GunSlide").GetComponent (typeof(GunSlide));
				playerFunctions = player.GetComponent<Pickup_Object> ();
				flash = (ParticleSystem)GameObject.FindWithTag ("MuzzleFlash").GetComponent (typeof(ParticleSystem));
				loadedMagazine = null;
				safety = GameObject.FindWithTag ("Safety");
				safety.GetComponent<Renderer> ().material.color = safetyOffColor;
				safetyOn = true;
		}

		public override void carryUpdate (Hand hand)
		{

				//if gun is shot
	

				if (hand.rightHand) {
						if ((SixenseInput.Controllers [1].Trigger > .8) || Input.GetMouseButton(0)) {
								if (!TriggerPulled)
										Shoot = true;
								TriggerPulled = true;
						} else {
								TriggerPulled = false;
						}
				}
				if (!hand.rightHand) {
						if ((SixenseInput.Controllers [0].Trigger > .8)  || Input.GetMouseButton(1)){
								if (!TriggerPulled)
										Shoot = true;
								TriggerPulled = true;
						} else {
								TriggerPulled = false;
						}
				}

				if (loadedMagazine != null && loadedMagazine.ammo > 0) {
						if (Shoot && !safetyOn) {
								loadedMagazine.ammo--;
								//TriggerPulled = true;
								//Debug.Log(playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward));
								gunSlide.slideAction ();
								GetComponent<AudioSource> ().Play ();
								showMuzzleFlash ();
								Vector3 fwd =  -this.transform.right;
								Ray gunDirection = new Ray (this.gameObject.transform.position, fwd * 50);
								LayerMask layerMask = 1 << LayerMask.NameToLayer("SafetyLayer"); //This ignores the SafetyMonitor, meaning the raycast will ignore the range plane when shooting
								
								Debug.DrawRay (this.gameObject.transform.position, fwd * 10, Color.green, 100);

								int i = 0;
								RaycastHit[] hits;
								hits = Physics.RaycastAll(this.gameObject.transform.position, fwd, 50);
								while(i < hits.Length) {
										
					
										if (hits[i].collider.gameObject.tag == "Target") {	
												Debug.Log ("HIT");
												Quaternion bulletHoleRotation = Quaternion.FromToRotation (Vector3.up, hits[i].normal);
												Instantiate (bulletHole, hits[i].point, bulletHoleRotation);
						
										}
								i++;
								}
						}
				}
				Shoot = false;


				//SAFETY
				if (Input.GetKeyDown (KeyCode.C)) {
						safetyOn = !safetyOn;
				}
				if (safetyOn)
						safety.GetComponent<Renderer> ().material.color = safetyOffColor;
				else
						safety.GetComponent<Renderer> ().material.color = Color.red;
		}

		void showMuzzleFlash ()
		{
				flash.Play ();
		}


		protected void Update ()
		{
		
		}

		//makes object not bounce
		protected void FixedUpdate ()
		{
				try {
						var currentVelocity = GetComponent<Rigidbody> ().velocity;
			
						if (currentVelocity.y <= 0f) 
								return;
			
						currentVelocity.y = 0f;
						GetComponent<Rigidbody> ().velocity = currentVelocity;
				} catch (UnityException e) {

				}

		}
}
