using UnityEngine;
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
		Hand rightHand;
		Hand leftHand;

		AudioSource gunSound;
		AudioSource dryFire;

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
				rightHand = (Hand)GameObject.FindWithTag ("RightHand").GetComponent<Hand> ();
				leftHand = (Hand)GameObject.FindWithTag ("LeftHand").GetComponent<Hand> ();

				AudioSource[] audioSources = GetComponents<AudioSource>();
				gunSound = audioSources [0];
				dryFire = audioSources [1];
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

				if (loadedMagazine != null && loadedMagazine.ammo > 0 && Shoot) {
						if (!safetyOn) {
								if(ScreenText.warning == ScreenText.Warning.FireMessage){
									ScreenText.warning = ScreenText.Warning.None;
								}
								ScreenText.firedOnce = true;
								loadedMagazine.ammo--;
								//TriggerPulled = true;
								//Debug.Log(playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward));
								gunSlide.slideAction ();
								gunSound.Play ();
								showMuzzleFlash ();
								Vector3 fwd =  -this.transform.right;
								Ray gunDirection = new Ray (GameObject.Find("MuzzlePoint").transform.position, fwd * 50);
								LayerMask layerMask = 1 << LayerMask.NameToLayer("SafetyLayer"); //This ignores the SafetyMonitor, meaning the raycast will ignore the range plane when shooting
								
								Debug.DrawRay (GameObject.Find("MuzzlePoint").transform.position, fwd * 100, Color.green, 100);

								int i = 0;
								RaycastHit[] hits;
								hits = Physics.RaycastAll(GameObject.Find("MuzzlePoint").transform.position, fwd, 50);
								while(i < hits.Length) {
										
					
										if (hits[i].collider.gameObject.tag == "Target") {	
												Debug.Log ("HIT");
												Quaternion bulletHoleRotation = Quaternion.FromToRotation (Vector3.up, hits[i].normal);
												Instantiate (bulletHole, hits[i].point, bulletHoleRotation);
						
										}
										i++;
								}
						} else {
							ScreenText.warning = ScreenText.Warning.ShortSafetyMessage;
						}
				} else if (loadedMagazine == null && Shoot){
					ScreenText.warning = ScreenText.Warning.LoadAMagazine;
					dryFire.Play ();
				} else if (loadedMagazine != null && loadedMagazine.ammo == 0 && Shoot){
					ScreenText.warning = ScreenText.Warning.EmptyClip;
					dryFire.Play ();
				}
				Shoot = false;


				//SAFETY
				if (Input.GetKeyDown (KeyCode.C)) {
						safetyOn = !safetyOn;
				}
				if (safetyOn)
					safety.GetComponent<Renderer> ().material.color = safetyOffColor;
				else {
					safety.GetComponent<Renderer> ().material.color = Color.red;
					if(ScreenText.warning == ScreenText.Warning.ShortSafetyMessage)
						ScreenText.warning = ScreenText.Warning.None;
					else if(ScreenText.warning == ScreenText.Warning.LongSafetyMessage)
						ScreenText.warning = ScreenText.Warning.FireMessage;
				}
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

	private void OnTriggerStay(Collider col)
	{
		string name = col.GetComponent<Collider> ().gameObject.name;
		if (FloatingText.attached == FloatingText.AttachPoint.None && 
		    ((name == "LeftHand" && leftHand.carriedObject == null) ||
		 (name == "RightHand" && rightHand.carriedObject == null))){
			FloatingText.attached = FloatingText.AttachPoint.Gun;
		} else if (FloatingText.attached == FloatingText.AttachPoint.Gun && 
		        ((name == "LeftHand" && leftHand.carriedObject.GetType() == typeof(Pistol)) ||
		 		(name == "RightHand" && rightHand.carriedObject.GetType() == typeof(Pistol)))){
			FloatingText.attached = FloatingText.AttachPoint.None;
		}
	}
	
	private void OnTriggerExit(Collider col){
		if (FloatingText.attached == FloatingText.AttachPoint.Gun) {
			FloatingText.attached = FloatingText.AttachPoint.None;
		}
	}
}
