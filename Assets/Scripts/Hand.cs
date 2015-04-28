using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
	public bool carrying;
	public Pickupable carriedObject;
	public bool rightHand;
	int cooldown = 0;

	// Use this for initialization
	void Start ()
	{
		carrying = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (cooldown != 0) {
			cooldown = cooldown - 1;
		}
		if (carrying) {
			carriedObject.carryUpdate (this);
			bool LeftBumper = SixenseInput.Controllers [0].GetButtonDown (SixenseButtons.BUMPER);
			bool RightBumper = SixenseInput.Controllers [1].GetButtonDown (SixenseButtons.BUMPER);

			if (carrying && cooldown == 0 && (rightHand && (Input.GetKeyDown (KeyCode.E) || RightBumper) || (!rightHand && (Input.GetKeyDown (KeyCode.Q) || LeftBumper)))) {
				carrying = false;
				carriedObject.GetComponent<Rigidbody> ().isKinematic = false;
				carriedObject.transform.parent = null;
				carriedObject.GetComponent<Collider> ().isTrigger = false;
				carriedObject.pickedUp = false;
				carriedObject = null;
				cooldown = 50;

				print ("Dropping");
			}
		}
	}

	private void OnTriggerStay (Collider col)
	{
		string name = col.GetComponent<Collider> ().gameObject.name;
		bool LeftBumper = SixenseInput.Controllers [0].GetButton (SixenseButtons.BUMPER);
		bool RightBumper = SixenseInput.Controllers [1].GetButton (SixenseButtons.BUMPER);
		//.Log ("right hand: " + rightHand + " pressing Q: " + Input.GetKeyDown (KeyCode.Q) + " carrying: " + carrying);
	

		if (cooldown == 0) {
			//Check if its a pickupable
			Pickupable p = col.GetComponent<Collider> ().gameObject.GetComponent<Pickupable> ();
			if (p != null) {
				if ((rightHand && RightBumper) || (!rightHand && LeftBumper)) {
					if (!p.pickedUp && !carrying) {
						print ("Picking up");
						carrying = true;
						carriedObject = p;
						carriedObject.pickedUp = true;
						carriedObject.GetComponent<Rigidbody> ().isKinematic = true;
						carriedObject.transform.parent = this.gameObject.transform;
						carriedObject.transform.localPosition = new Vector3 (carriedObject.handPositionOffset.x, carriedObject.handPositionOffset.y, carriedObject.handPositionOffset.z);
						carriedObject.transform.localRotation = Quaternion.Euler (carriedObject.handRotationOffset.x, carriedObject.handRotationOffset.y, carriedObject.handRotationOffset.z);
						carriedObject.GetComponent<Collider> ().isTrigger = true;

						cooldown = 50;

						//What is this
						Magazine mag = col.GetComponent<Collider> ().gameObject.GetComponent<Magazine> ();
						if (mag != null) {
							mag.attached = Magazine.AttachPoint.Hand;
							mag.checkFirstCheckpointComplete ();
						} else {	//if didn't just pick up a magazine
							Magazine mag2 = GameObject.FindWithTag ("Magazine").GetComponent<Magazine> ();
							mag2.checkFirstCheckpointComplete ();
						}
					}
				}
				return;
			} else {
				if (name == "HammerPoint") {
					
				}
				if (name == "MagEntryPoint") {
					if (carriedObject is Magazine) {
						Magazine m = carriedObject.GetComponent<Magazine> ();
						if (m != null) {
							if ((rightHand && (SixenseInput.Controllers [1].Trigger > .8)) || (!rightHand && (SixenseInput.Controllers [0].Trigger > .8))) {
								m.Insert (this);
								cooldown = 50;
								print ("ENTRY GOOD");
							}
						} 
					} else if (carriedObject == null && ((rightHand && (SixenseInput.Controllers [1].Trigger > .8)) || (!rightHand && (SixenseInput.Controllers [0].Trigger > .8)))) {
						AbstractGun G = GameObject.FindGameObjectWithTag ("Pistol").GetComponent<AbstractGun> ();
						G.Eject ();
						cooldown = 50;
						print ("EJECT GOOD");
					}
				}
				if (name == "SideOfGunPoint") {
					if (carriedObject == null) {
												
						if ((rightHand && (SixenseInput.Controllers [1].Trigger > .8)) || (!rightHand && (SixenseInput.Controllers [0].Trigger > .8))) {
							AbstractGun G = GameObject.FindGameObjectWithTag ("Pistol").GetComponent<AbstractGun> ();
							G.SafteyToggle ();
							print ("SAFTEY GOOD");
							cooldown = 50;
						}
					}
				}
				if (name == "SlidePoint") {
					if (carriedObject == null) {
						if ((rightHand && RightBumper) || (!rightHand && LeftBumper)) {
							AbstractGun G = GameObject.FindGameObjectWithTag ("Pistol").GetComponent<AbstractGun> ();
							G.gunSlide.SetBack ();
							cooldown = 50;
							print ("BackGood GOOD");

							if ( ScreenText.warning == ScreenText.Warning.PullTheSlide ) {
								ScreenText.warning = ScreenText.Warning.FireMessage;
							}
						}
					}
				}
			}
		}

		checkPickupText (name, col);
	}

	private void checkPickupText (string name, Collider col)
	{
		if (name == "Magazine" && !carrying) {
			FloatingText.magazine = col.gameObject;
			FloatingText.attached = FloatingText.AttachPoint.Magazine;
		} else if (name == "sigsauer" && !carrying) {
			FloatingText.attached = FloatingText.AttachPoint.Gun;
		} else if (showTextForLeftHandMagazine(name, col)) {
			FloatingText.attached = FloatingText.AttachPoint.LeftHandMagazine;
		} else if (showTextForRightHandMagazine(name, col)) {
			FloatingText.attached = FloatingText.AttachPoint.RightHandMagazine;
		} else if (carrying) {
			FloatingText.attached = FloatingText.AttachPoint.None;
		}
	}

	private bool showTextForLeftHandMagazine(string name, Collider col){
		AbstractGun pistol = GameObject.FindGameObjectWithTag ("Pistol").GetComponent<AbstractGun> ();
		//Debug.Log ((!rightHand && name == "MagEntryPoint") + " " + (carriedObject != null) + " " + (carriedObject.GetType () == typeof(Magazine)) + " " + (pistol.loadedMagazine == null)); 
		if (!rightHand && name == "MagEntryPoint" && carriedObject != null && 
		    carriedObject.GetType () == typeof(Magazine) && pistol.loadedMagazine == null) {
			return true;
		}

		return false;
	}

	private bool showTextForRightHandMagazine(string name, Collider col){
		AbstractGun pistol = GameObject.FindGameObjectWithTag ("Pistol").GetComponent<AbstractGun> ();
		//Debug.Log ((rightHand && name == "MagEntryPoint") + " " + (carriedObject != null) + " " + (carriedObject.GetType () == typeof(Magazine)) + " " + (pistol.loadedMagazine == null)); 
		if (rightHand && name == "MagEntryPoint" && carriedObject != null && 
		    carriedObject.GetType () == typeof(Magazine) && pistol.loadedMagazine == null) {
			return true;
		}
		
		return false;
	}
}
