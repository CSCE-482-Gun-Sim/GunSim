using UnityEngine;
using System.Collections;

public class SafetyMonitor : MonoBehaviour {
	GameObject player;
	FloatingText text;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");

		//Debug.Log(player.transform.position);
		
	
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
		if(rGun == null && lGun == null){
			Debug.Log ("No gun held.");
			ScreenText.warning = ScreenText.Warning.None;
		}

	}

	void checkPointingDownRange(AbstractGun gun){

		Ray gunDirection = new Ray (gun.gameObject.transform.position + gun.gameObject.transform.TransformDirection (gun.barrel_offset), 
		                            -gun.gameObject.transform.right * 50);
		
		LayerMask layerMask = 1 << 8; //This ignores every layer except the SafetyMonitor layer, meaning the raycast will only be able to collide with the Range Plane or other Safety Layer Colliders
		
		RaycastHit hit;
		if (Physics.Raycast (gunDirection, out hit, layerMask)) {	//Debug.Log(hit.collider.gameObject.tag);
			if (hit.collider.gameObject.tag != "RangePlane") {//Might want to use collider filters so that you dont get bugs when shooting	
				Debug.Log ("Not pointing correctly");
				//ScreenText text = GameObject.FindWithTag ("MainCamera").GetComponent(typeof(ScreenText));
				ScreenText.warning = ScreenText.Warning.PointDownRange;
			} else {
				Debug.Log ("Pointing correctly.");
				ScreenText.warning = ScreenText.Warning.None;
			}
		}
	}
}
