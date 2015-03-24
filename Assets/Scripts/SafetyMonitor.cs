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
		GameObject.Find ("WarningText").GetComponent<Renderer>().enabled = false;

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

			Ray gunDirection = new Ray(rGun.gameObject.transform.position + rGun.gameObject.transform.TransformDirection(rGun.barrel_offset), 
			                           rGun.gameObject.transform.forward*50);

			LayerMask layerMask = 1 << 8; //This ignores every layer except the SafetyMonitor layer, meaning the raycast will only be able to collide with the Range Plane or other Safety Layer Colliders

			RaycastHit hit;
			if(Physics.Raycast (gunDirection, out hit, layerMask))
			{	//Debug.Log(hit.collider.gameObject.tag);
				if(hit.collider.gameObject.tag != "RangePlane")//Might want to use collider filters so that you dont get bugs when shooting
				{	
					GameObject.Find ("WarningText").GetComponent<FloatingText>().attached = FloatingText.AttachPoint.Camera;
					GameObject.Find ("WarningText").GetComponent<FloatingText>().setText("Please point down range");
					Debug.Log("Not Pointing The Right Gun Down Range");
					GameObject.Find ("WarningText").GetComponent<Renderer>().enabled = true;
				}
			}
		}



		if (lGun != null) {

			Ray gunDirection = new Ray(lGun.gameObject.transform.position + lGun.gameObject.transform.TransformDirection(lGun.barrel_offset), 
			                           lGun.gameObject.transform.forward*50);
			
			RaycastHit hit;
			if(Physics.Raycast (gunDirection, out hit))
			{
				if(hit.collider.gameObject.tag != "RangePlane")//Might want to use collider filters so you dont get bugs when shooting at targets, might not register on target
				{	
					GameObject.Find ("WarningText").GetComponent<FloatingText>().attached = FloatingText.AttachPoint.Camera;
					GameObject.Find ("WarningText").GetComponent<FloatingText>().setText("Please point down range");
					Debug.Log("Not Pointing The Left Gun Down Range");
					GameObject.Find ("WarningText").GetComponent<Renderer>().enabled = true;
					
				}
			}
		}

	}
}
