using UnityEngine;
using System.Collections;

public class SafetyMonitor : MonoBehaviour {
	GameObject player;
	FloatingText text;
	AbstractGun pistol;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		pistol = (AbstractGun)GameObject.FindGameObjectWithTag ("Pistol").GetComponent(typeof(AbstractGun));
	}
	
	// Update is called once per frame
	void Update () {

		pointDownRange ();

	}

	void pointDownRange(){


		Hand rHand = GameObject.FindWithTag("RightHand").GetComponent("Hand") as Hand;
		Hand lHand = GameObject.FindWithTag("LeftHand").GetComponent("Hand") as Hand;

		AbstractGun rGun = null; AbstractGun lGun = null;

		if (rHand.carrying) {
			rGun = rHand.carriedObject as AbstractGun;
		}

		if (lHand.carrying) {
			lGun = lHand.carriedObject as AbstractGun;
		}

		if (rGun != null) {
			checkPointingDownRange(rGun);
		}
		if (lGun != null) {
			checkPointingDownRange(lGun);
		}
		if(rGun == null && lGun == null && AbstractGun.beenLoadedBefore){
			ScreenText.warning = ScreenText.Warning.None;
		}

	}

	void checkPointingDownRange(AbstractGun gun){

		Ray gunDirection = new Ray (gun.gameObject.transform.position + gun.gameObject.transform.TransformDirection (gun.barrel_offset), 
		                            -gun.gameObject.transform.right * 50);
		
		LayerMask layerMask = 1 << 8; //This ignores every layer except the SafetyMonitor layer, meaning the raycast will only be able to collide with the Range Plane or other Safety Layer Colliders
		
		RaycastHit hit;
		if (Physics.Raycast (gunDirection, out hit, layerMask)) {	//Debug.Log(hit.collider.gameObject.tag);
			if (hit.collider.gameObject.tag != "RangePlane" &&
			    pistol.loadedMagazine != null) {//Might want to use collider filters so that you dont get bugs when shooting	

				ScreenText.warning = ScreenText.Warning.PointDownRange;
			} else if (AbstractGun.beenLoadedBefore){
				ScreenText.warning = ScreenText.Warning.None;
			}
		}
	}
}
