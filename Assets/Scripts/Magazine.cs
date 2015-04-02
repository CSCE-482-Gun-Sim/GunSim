using UnityEngine;
using System.Collections;

public class Magazine : Pickupable {
	private string text;
	public enum AttachPoint{
		None,
		Hand,
		Gun
	};
	Hand rightHand;
	Hand leftHand;
	GameObject pistol;
	public int ammo;

	public Vector3 handRotation;
	public Vector3 pistolRotation;
	public Vector3 handPosition;
	public Vector3 pistolPosition;
	public Vector3 lerpHandPosition;
	public Vector3 lerpPistolPosition;

	bool needsToLerp = false;
	float speed = 5.0f;
	public AttachPoint attached;
	
	// Use this for initialization
	void Start () {
		attached = AttachPoint.None;	
		rightHand = (Hand)GameObject.FindWithTag ("RightHand").GetComponent<Hand> ();
		leftHand = (Hand)GameObject.FindWithTag ("LeftHand").GetComponent<Hand> ();
		pistol = GameObject.FindGameObjectWithTag ("Pistol");
		ammo = 10;
	}

	// Update is called once per frame
	void Update () {

		//put magazine in gun or drop magazine
		if (Input.GetKeyDown (KeyCode.V)) {
			//only load if gun is in hand
			//&& rightHand.carriedObject.GetType() == typeof(Pistol)
			Debug.Log("type: " + (rightHand.carriedObject.GetType() == typeof(Pistol)));
 			if (attached == AttachPoint.Hand){
				//only put magazine in gun if gun is attached
				if ((rightHand.carriedObject != null && rightHand.carriedObject.GetType() == typeof(Pistol)) ||
				    (leftHand.carriedObject != null && leftHand.carriedObject.GetType() == typeof(Pistol))) {
					needsToLerp = true;
					attached = AttachPoint.Gun;
					this.GetComponent<Rigidbody>().isKinematic = true;
				}
			} else if (attached == AttachPoint.Gun){
				attached = AttachPoint.None;
				this.GetComponent<Rigidbody>().isKinematic = false;
				this.transform.parent = null;

				//pistol no longer has magazine
				AbstractGun pistolClass = (AbstractGun)pistol.GetComponent(typeof(AbstractGun));
				pistolClass.loadedMagazine = null;
			} //else {
				//needsToLerp = true;
				//attached = AttachPoint.Hand;
				//this.GetComponent<Rigidbody>().isKinematic = true;
			//}
		}

		Transform snapTransform = null;
		/*if (attached == AttachPoint.Hand) {
			snapTransform = hand.transform;
		} else */
		if (attached == AttachPoint.Gun) {
			snapTransform = pistol.transform;
		}

		//lerp to position
		if (needsToLerp) {
			float step = speed * Time.deltaTime;
			lerpToObject (snapTransform, step);
		}

	}

	private void lerpToObject(Transform transformMoveTo, float step){
		Vector3 positionCorrection = Vector3.zero;
		if (attached == AttachPoint.Gun) {
			positionCorrection = lerpPistolPosition;
		} else if (attached == AttachPoint.Hand) {
			positionCorrection = lerpHandPosition;
		}
		transform.position = Vector3.MoveTowards (this.transform.position, new Vector3(transformMoveTo.position.x + positionCorrection.x,
		                                                                               transformMoveTo.position.y + positionCorrection.y,
		                                                                               transformMoveTo.position.z + positionCorrection.z), step);
		//object has now moved to its destination, snap so that it is now child of parent
		if (this.transform.position == new Vector3 (transformMoveTo.position.x + positionCorrection.x,
                     transformMoveTo.position.y + positionCorrection.y,
                     transformMoveTo.position.z + positionCorrection.z)) {
			needsToLerp = false;
			snapToObject(transformMoveTo);
		}
	}

	private void snapToObject(Transform snapTransform){

		if (snapTransform != null) {
			Vector3 snapPosition = snapTransform.position;
			Vector3 positionCorrection = Vector3.zero;
			if (attached == AttachPoint.Gun) {
				positionCorrection = pistolPosition;
			} else if (attached == AttachPoint.Hand) {
				positionCorrection = handPosition;
			}
			this.transform.position = new Vector3 (snapPosition.x, 
                                       snapPosition.y, snapPosition.z);

			this.GetComponent<Rigidbody>().isKinematic = true;
			Vector3 rotationVec = Vector3.zero;
			/*if (attached == AttachPoint.Hand) {
				rotationVec = handRotation;
				this.transform.parent = hand.transform;
			} else */
			if (attached == AttachPoint.Gun) {
				rotationVec = pistolRotation;
				this.transform.parent = pistol.transform;
			}
			this.transform.localPosition = Vector3.zero; 

			if (rotationVec != Vector3.zero) {
				this.transform.localRotation = Quaternion.Euler (rotationVec.x, rotationVec.y, rotationVec.z);
			}
			if (positionCorrection != Vector3.zero) {
				this.transform.localPosition = positionCorrection;
			}
		}

		//once snapped, set the pistol's magazine to this magazine
		AbstractGun pistolClass = (AbstractGun)pistol.GetComponent(typeof(AbstractGun));
		pistolClass.loadedMagazine = this;
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
