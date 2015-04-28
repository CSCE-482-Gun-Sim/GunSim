using UnityEngine;
using System.Collections;

public abstract class AbstractGun : Pickupable
{
	public Vector3 barrel_offset;
	GameObject player;
	public GunSlide gunSlide;
	Pickup_Object playerFunctions;
	public Transform bulletHole;
	bool TriggerPulled;
	ParticleSystem flash;
	public Magazine loadedMagazine;
	public bool bulletInChamber;
	GameObject safety;
	bool safetyOn;
	bool Shoot;
	Color safetyOffColor = new Color (.1f, .1f, .1f);
	Hand rightHand;
	Hand leftHand;
	AudioSource gunSound;
	AudioSource dryFire;
	int cooldown;

	// Use this for initialization
	protected void Start ()
	{
		cooldown = 0;
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

		AudioSource[] audioSources = GetComponents<AudioSource> ();
		gunSound = audioSources [0];
		dryFire = audioSources [1];

		//DEBUG
		//loadedMagazine = GameObject.FindWithTag ("Magazine").GetComponent<Magazine> ();
	}

	public override void carryUpdate (Hand hand)
	{
		//if gun is shot
		if (cooldown != 0) {
			cooldown = cooldown - 1;
			return;
		}

		if (hand.rightHand) {
			if ((SixenseInput.Controllers [1].Trigger > .8) || Input.GetMouseButton (0)) {
				if (!TriggerPulled) {
					Shoot = true;
				}
				TriggerPulled = true;
			} else {
				TriggerPulled = false;
			}
		}
		if (!hand.rightHand) {
			if ((SixenseInput.Controllers [0].Trigger > .8) || Input.GetMouseButton (1)) {
				if (!TriggerPulled) {
					Shoot = true;
				}
				TriggerPulled = true;
			} else {
				TriggerPulled = false;
			}
		}
		    
		if (bulletInChamber && Shoot) {
			if (!safetyOn) {
				//bulletInChamber = false;
				if (ScreenText.warning == ScreenText.Warning.FireMessage) {
					ScreenText.warning = ScreenText.Warning.None;
				}
				cooldown = 20;
				ScreenText.firedOnce = true;
				gunSlide.slideFire ();
				gunSound.Play ();
				showMuzzleFlash ();
				Vector3 fwd = -this.transform.right;
				Ray gunDirection = new Ray (GameObject.Find ("MuzzlePoint").transform.position, fwd * 50);
				LayerMask layerMask = 1 << LayerMask.NameToLayer ("SafetyLayer"); //This ignores the SafetyMonitor, meaning the raycast will ignore the range plane when shooting
								
				Debug.DrawRay (GameObject.Find ("MuzzlePoint").transform.position, fwd * 100, Color.green, 100);

				int i = 0;
				RaycastHit[] hits;
				hits = Physics.RaycastAll (GameObject.Find ("MuzzlePoint").transform.position, fwd, 50);
				while (i < hits.Length) {
					if (hits [i].collider.gameObject.tag == "Target") {	
						Debug.Log ("HIT");
						Quaternion bulletHoleRotation = Quaternion.FromToRotation (Vector3.up, hits [i].normal);
						Instantiate (bulletHole, hits [i].point, bulletHoleRotation);
						
					}
					i++;
				}
			}
		} else {
			if (bulletInChamber == false && gunSlide.target == GunSlide.SlideTarget.Ready && Shoot) {
				dryFire.Play ();
				if (!ScreenText.shownEmptyMessage) {
					ScreenText.shownEmptyMessage = true;
					ScreenText.warning = ScreenText.Warning.EmptyClip;
				}
			} else if (bulletInChamber == false && Shoot) {
				if (!ScreenText.shownEmptyMessage) {
					ScreenText.shownEmptyMessage = true;
					ScreenText.warning = ScreenText.Warning.EmptyClip;
				}
			}
		}
		Shoot = false;

		if (safetyOn) {
			safety.GetComponent<Renderer> ().material.color = safetyOffColor;
		} else {
			safety.GetComponent<Renderer> ().material.color = Color.red;
			if (ScreenText.warning == ScreenText.Warning.ShortSafetyMessage) {
				ScreenText.warning = ScreenText.Warning.None;
			} else if (ScreenText.warning == ScreenText.Warning.LongSafetyMessage) {
				ScreenText.warning = ScreenText.Warning.PullTheSlide;
			}
		}
	}

	public void SafteyToggle ()
	{
		safetyOn = !safetyOn;
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
			
			if (currentVelocity.y <= 0f) { 
				return;
			}
			
			currentVelocity.y = 0f;
			GetComponent<Rigidbody> ().velocity = currentVelocity;
		} catch (UnityException e) {

		}

	}

	private void OnTriggerStay (Collider col)
	{
		string name = col.GetComponent<Collider> ().gameObject.name;
		if (FloatingText.attached == FloatingText.AttachPoint.None && 
			((name == "LeftHand" && leftHand.carriedObject == null) ||
			(name == "RightHand" && rightHand.carriedObject == null))) {
			FloatingText.attached = FloatingText.AttachPoint.Gun;
		} else if (FloatingText.attached == FloatingText.AttachPoint.Gun && 
			((name == "LeftHand" && leftHand == null && leftHand.carriedObject.GetType () == typeof(Pistol)) ||
			(name == "RightHand" && rightHand == null && rightHand.carriedObject.GetType () == typeof(Pistol)))) {
			FloatingText.attached = FloatingText.AttachPoint.None;
		}
	}
	
	private void OnTriggerExit (Collider col)
	{
		if (FloatingText.attached == FloatingText.AttachPoint.Gun) {
			FloatingText.attached = FloatingText.AttachPoint.None;
		}
	}

	public void Eject ()
	{
		if (loadedMagazine != null) {
			loadedMagazine.Eject ();
		}
	}
}
