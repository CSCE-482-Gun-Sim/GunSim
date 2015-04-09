using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public bool carrying;
    public Pickupable carriedObject;
    public bool rightHand;
	int cooldown = 0;
	public string collidingWith;

    // Use this for initialization
    void Start()
    {
        carrying = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (cooldown != 0)
			cooldown = cooldown -1;
        if (carrying)
        {
			carriedObject.carryUpdate(this);
			bool LeftBumper = SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.BUMPER);
			bool RightBumper = SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.BUMPER);

			if (carrying && cooldown == 0 && (rightHand && (Input.GetKeyDown(KeyCode.E) || RightBumper) || (!rightHand && (Input.GetKeyDown(KeyCode.Q) ||LeftBumper))))
            {
                carrying = false;
                carriedObject.GetComponent<Rigidbody>().isKinematic = false;
                carriedObject.transform.parent = null;
				carriedObject.GetComponent<Collider>().isTrigger = false;
				carriedObject.pickedUp = false;
                carriedObject = null;
				cooldown = 50;

                print("Dropping");
            }
        }
    }

	private void OnTriggerStay(Collider col)
	{

		bool LeftBumper = SixenseInput.Controllers [0].GetButton (SixenseButtons.BUMPER);
		bool RightBumper = SixenseInput.Controllers [1].GetButton (SixenseButtons.BUMPER);
		//.Log ("right hand: " + rightHand + " pressing Q: " + Input.GetKeyDown (KeyCode.Q) + " carrying: " + carrying);

		if (((rightHand && (Input.GetKeyDown(KeyCode.E)||RightBumper)) || (!rightHand && (Input.GetKeyDown(KeyCode.Q)||LeftBumper))) && !carrying)
		{
			//print ("okay");
			//Pickupable p = col.collider.gameObject as Pickupable;
			Pickupable p = col.GetComponent<Collider>().gameObject.GetComponent<Pickupable>();
			if (p != null && cooldown == 0 && !p.pickedUp)
			{
				print ("Picking up");
				carrying = true;
				carriedObject = p;
				carriedObject.pickedUp = true;
				carriedObject.GetComponent<Rigidbody>().isKinematic = true;
				carriedObject.transform.parent = this.gameObject.transform;
				carriedObject.transform.localPosition = new Vector3(carriedObject.handPositionOffset.x, carriedObject.handPositionOffset.y, carriedObject.handPositionOffset.z);
				carriedObject.transform.localRotation = Quaternion.Euler(carriedObject.handRotationOffset.x, carriedObject.handRotationOffset.y, carriedObject.handRotationOffset.z);
				carriedObject.GetComponent<Collider>().isTrigger = true;

				cooldown = 50;

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
		string name = col.GetComponent<Collider> ().gameObject.name;
		if (name == "HammerPoint" || name == "MagEntryPoint" || name == "SideOfGunPoint" || name == "SlidePoint") {
			name = collidingWith;

			FloatingText ft = GameObject.FindGameObjectWithTag ("FloatingText").GetComponent<FloatingText> ();
			ft.GetComponent<Renderer> ().enabled = true;
			ft.transform.position = this.transform.position * 2;

			ft.setText(name);
		}

		//Debug.Log ("Name: " + name);
		//floating text for picking up logic NOW IN MAGAZINE AND ABSTRACTGUN SCRIPTS
		/*if (name == "Magazine" && !carrying) {
			FloatingText.magazine = col.gameObject;
			FloatingText.attached = FloatingText.AttachPoint.Magazine;
		} else if (name == "sigsauer" && !carrying){
			FloatingText.attached = FloatingText.AttachPoint.Gun;
		} else if (carrying){
			FloatingText.attached = FloatingText.AttachPoint.None;
		}*/
	}
}
