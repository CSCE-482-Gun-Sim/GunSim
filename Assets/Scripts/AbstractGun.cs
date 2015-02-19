using UnityEngine;
using System.Collections;

public abstract class AbstractGun : Pickupable {
	protected Vector3 barrel_offset;
	GameObject player;
	Pickup_Object playerFunctions;
	public Transform bulletHole;

	// Use this for initialization
	protected void Start () {
		player = GameObject.FindWithTag("Player");

		Debug.Log(player.transform.position);

		playerFunctions = player.GetComponent<Pickup_Object> ();
	}

	public override void carryUpdate(Hand hand)
	{
		if(((hand.rightHand && Input.GetMouseButtonDown(0)) || (!hand.rightHand && Input.GetMouseButtonDown(1))))
		{
				//Debug.Log(playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward));
				
				Ray gunDirection = new Ray(this.gameObject.transform.position + barrel_offset, 
				                           this.gameObject.transform.TransformDirection(Vector3.forward)*50);
				
				RaycastHit hit;
				if(Physics.Raycast (gunDirection, out hit))
				{
					Debug.DrawRay(this.gameObject.transform.position + barrel_offset, 
					              this.gameObject.transform.TransformDirection(Vector3.forward) * 10, Color.green, 100);
					
					if(hit.collider.gameObject.tag == "Target")
					{	
						Debug.Log("HIT");
						Quaternion bulletHoleRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
						Instantiate(bulletHole, hit.point, bulletHoleRotation);
					}
				}
			}
	}

	//Need to make barrel position relative to the gun position, add them relative to gun axis not the world axis
	protected void Update () {

		/*if (playerFunctions.carriedObject != null) {
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log(playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward));

				Ray gunDirection = new Ray(playerFunctions.carriedObject.transform.position + barrel_offset, 
				                           playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward)*50);

				RaycastHit hit;
				if(Physics.Raycast (gunDirection, out hit))
				{
					Debug.DrawRay(playerFunctions.carriedObject.transform.position + barrel_offset, 
					              playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward) * 10, Color.green, 100);

					if(hit.collider.gameObject.tag == "Target")
					{	
						Debug.Log("HIT");
						Quaternion bulletHoleRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
						Instantiate(bulletHole, hit.point, bulletHoleRotation);
					}
				}
			}
		}*/
	}
}
