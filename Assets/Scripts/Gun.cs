using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public Vector3 barrel_offset;
	GameObject player;
	Pickup_Object playerFunctions;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");

		Debug.Log(player.transform.position);

		playerFunctions = player.GetComponent<Pickup_Object> ();
	}
	
	// Update is called once per frame


	//Need to make barrel position relative to the gun position, add them relative to gun axis not the world axis
	void Update () {

		if (playerFunctions.carriedObject != null) {
		
			if (Input.GetMouseButtonDown(0))
			{

				Debug.Log(playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward));

				Ray gunDirection = new Ray(playerFunctions.carriedObject.transform.position + barrel_offset, playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward)*50);
				RaycastHit hit;

				if(Physics.Raycast (gunDirection, out hit))
				{

					Debug.DrawRay(playerFunctions.carriedObject.transform.position + barrel_offset, playerFunctions.carriedObject.transform.TransformDirection(Vector3.forward) * 10, Color.green, 100);

					if(hit.collider.gameObject.name == "Target")
					{	
						Debug.Log("HIT");
						hit.collider.gameObject.renderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
					}

				}
			}
			/*if (Input.GetMouseButtonDown(0)) {
				var gunObject = GameObject.Find("1911_Pistol_01");
				
				var fwd = 10 * transform.TransformDirection (Vector3.forward);
				var hit : RaycastHit;
				if (Physics.Raycast (gunObject.transform.position, fwd, hit)) {
					//print ("There is something in front of the object!");
					Debug.DrawRay (gunObject.transform.position, fwd, Color.green, 100);
					//print('Collision at point ' + hit.point);
					if(hit.collider.gameObject.name == 'Target')
						hit.collider.gameObject.renderer.material.color = Color(Random.Range(0.0,1.0),Random.Range(0.0,1.0),Random.Range(0.0,1.0));
				}
				}*/

			/*var rayOrigin = Camera.main.ScreenToWorldPoint();
		var fwd = transform.TransformDirection (Vector3.forward);
		var hit : RaycastHit;
		if (Physics.Raycast (rayOrigin, fwd, 100)) {
			print("HIT SOMETHING");

			//Debug.DrawLine (rayOrigin, hit.point);
		}*/
		}
	}
}
