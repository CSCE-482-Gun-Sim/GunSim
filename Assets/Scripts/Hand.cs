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
			bool LeftBumper = SixenseInput.Controllers[0].GetButton(SixenseButtons.BUMPER);
			bool RightBumper = SixenseInput.Controllers[1].GetButton(SixenseButtons.BUMPER);

			if (!pickupframe && (rightHand && (Input.GetKeyDown(KeyCode.E) || RightBumper) || (!rightHand && (Input.GetKeyDown(KeyCode.Q) ||LeftBumper))))
            {
                carrying = false;
                carriedObject.GetComponent<Rigidbody>().isKinematic = false;
                carriedObject.transform.parent = null;
				carriedObject.GetComponent<Collider>().isTrigger = false;
                carriedObject = null;

                print("Dropping");
            }
        }
        pickupframe = false;
    }

	private void OnTriggerStay(Collider col)
	{
		bool LeftBumper = SixenseInput.Controllers[0].GetButton(SixenseButtons.BUMPER);
		bool RightBumper = SixenseInput.Controllers[1].GetButton(SixenseButtons.BUMPER);

		//.Log ("right hand: " + rightHand + " pressing Q: " + Input.GetKeyDown (KeyCode.Q) + " carrying: " + carrying);

		if (((rightHand && (Input.GetKeyDown(KeyCode.E)||RightBumper)) || (!rightHand && (Input.GetKeyDown(KeyCode.Q)||LeftBumper))) && !carrying)
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
				carriedObject.transform.localPosition = new Vector3(carriedObject.handPositionOffset.x, carriedObject.handPositionOffset.y, carriedObject.handPositionOffset.z);
				carriedObject.transform.localRotation = Quaternion.Euler(carriedObject.handRotationOffset.x, carriedObject.handRotationOffset.y, carriedObject.handRotationOffset.z);
				carriedObject.GetComponent<Collider>().isTrigger = true;

				pickupframe = true;

				Magazine mag = col.GetComponent<Collider>().gameObject.GetComponent<Magazine>();
				if(mag != null){
					mag.attached = Magazine.AttachPoint.Hand;
					mag.checkFirstCheckpointComplete();
				} else {	//if didn't just pick up a magazine
					Magazine mag2 = GameObject.FindWithTag("Magazine").GetComponent<Magazine>();
					mag2.checkFirstCheckpointComplete();
				}
			}
		}
	}
}
