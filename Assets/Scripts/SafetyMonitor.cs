using UnityEngine;
using System.Collections;

public class SafetyMonitor : MonoBehaviour {
	GameObject player;
	FloatingText text;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		text = GameObject.FindWithTag ("FloatingText").GetComponent<FloatingText>();
		//Debug.Log(player.transform.position);
		
	
	}
	
	// Update is called once per frame
	void Update () {

		
	
	}

	void pointDownRange(){
		Hand rHand = GameObject.FindWithTag("RightHand").GetComponent("Hand") as Hand;
		Hand lHand = GameObject.FindWithTag("LeftHand").GetComponent("Hand") as Hand;

		if (rHand.carriedObject is AbstractGun) {
			AbstractGun rGun = rHand.carriedObject as AbstractGun;
			Ray gunDirection = new Ray(rGun.gameObject.transform.position + rGun.gameObject.transform.TransformDirection(rGun.barrel_offset), 
			                           rGun.gameObject.transform.TransformDirection(Vector3.forward)*50);
			
			RaycastHit hit;
			if(Physics.Raycast (gunDirection, out hit))
			{
				if(hit.collider.gameObject.tag != "RangePlane")//Might want to use collider filters so that you dont get bugs when shooting
				{	
					text.attached = FloatingText.AttachPoint.Camera;
					text.setText("Please Face the Range");
				}
			}
		}

		if (lHand.carriedObject is AbstractGun) {
			AbstractGun lGun = lHand.carriedObject as AbstractGun;
			Ray gunDirection = new Ray(lGun.gameObject.transform.position + lGun.gameObject.transform.TransformDirection(lGun.barrel_offset), 
			                           lGun.gameObject.transform.TransformDirection(Vector3.forward)*50);
			
			RaycastHit hit;
			if(Physics.Raycast (gunDirection, out hit))
			{
				if(hit.collider.gameObject.tag != "RangePlane")//Might want to use collider filters so you dont get bugs when shooting at targets, might not register on target
				{	
					text.attached = FloatingText.AttachPoint.Camera;
					text.setText("Please Face the Range");
				}
			}
		}
	}
}
