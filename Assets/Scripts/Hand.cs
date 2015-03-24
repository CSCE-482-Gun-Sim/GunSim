using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public bool carrying;
    public Pickupable carriedObject;
    bool pickupframe;
    public bool rightHand;

    // Use this for initialization
    void Start()
    {
        carrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (carrying)
        {
			carriedObject.carryUpdate(this);

            if (((rightHand && Input.GetKeyDown(KeyCode.E)) || (!rightHand && Input.GetKeyDown(KeyCode.Q))) && !pickupframe)
            {
                carrying = false;
                carriedObject.GetComponent<Rigidbody>().isKinematic = false;
                carriedObject.transform.parent = null;
				carriedObject.GetComponent<Collider>().isTrigger = false;
                carriedObject = null;
                CrossHair.drawCrosshair = true;

                print("Dropping");
            }
        }
        pickupframe = false;
    }

	private void OnTriggerStay(Collider col)
	{
		if (((rightHand && Input.GetKeyDown(KeyCode.E)) || (!rightHand && Input.GetKeyDown(KeyCode.Q))) && !carrying)
		{
			//print ("okay");
			//Pickupable p = col.collider.gameObject as Pickupable;
			Pickupable p = col.GetComponent<Collider>().gameObject.GetComponent<Pickupable>();
			if (p != null)
			{
				carrying = true;
				carriedObject = p;
				carriedObject.GetComponent<Rigidbody>().isKinematic = true;
				carriedObject.transform.parent = this.gameObject.transform;
				carriedObject.transform.localPosition = Vector3.zero; 
				carriedObject.transform.localRotation = Quaternion.Euler(carriedObject.handRotationOffset.x, carriedObject.handRotationOffset.y, carriedObject.handRotationOffset.z);
				carriedObject.GetComponent<Collider>().isTrigger = true;

				pickupframe = true;
				
				CrossHair.drawCrosshair = false;
			}
		}
	}
}
