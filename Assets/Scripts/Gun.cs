using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public Vector3 barrel_offset;
	GameObject player;
	Pickup_Object playerFunctions;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		playerFunctions = player.GetComponent<Pickup_Object> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (playerFunctions.carriedObject != null) {
			Debug.DrawRay(barrel_offset, new Vector3(0,0,0));
			Debug.Log("pizza");
			if (Input.GetMouseButtonDown(0))
			{
				//Ray ray = new Ray(barrel_offset,playerFunctions.carriedObject.transform.rotation);

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
