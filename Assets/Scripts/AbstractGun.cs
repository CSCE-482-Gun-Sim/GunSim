﻿using UnityEngine;
using System.Collections;

public abstract class AbstractGun : MonoBehaviour {
	protected Vector3 barrel_offset;
	GameObject player;
	Pickup_Object playerFunctions;
	public float gunHeight;							//used for making top of gun at center of screen for shooting
	abstract protected void defineWeaponProperties();	//required by inherited classes

	// Use this for initialization
	protected void Start () {
		player = GameObject.FindWithTag("Player");

		Debug.Log(player.transform.position);

		playerFunctions = player.GetComponent<Pickup_Object> ();
	}

	//Need to make barrel position relative to the gun position, add them relative to gun axis not the world axis
	protected void Update () {

		Debug.Log ("gun is " + playerFunctions.carriedObject);
		if (playerFunctions.carriedObject != null) {
		
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
						hit.collider.gameObject.renderer.material.color = new Color(Random.Range(0.0f, 1.0f), 
						                                                            Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
					}
				}
			}
		}
	}
}
