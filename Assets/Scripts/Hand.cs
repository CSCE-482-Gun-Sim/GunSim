using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{
    bool carrying;
    public GameObject carriedObject;
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
            //if this carried object is a gun
            if (carriedObject.GetComponent("AbstractGun") as AbstractGun != null)
            {

            }

            if (((rightHand && Input.GetKeyDown(KeyCode.E)) || (!rightHand && Input.GetKeyDown(KeyCode.Q))) && !pickupframe)
            {
                carrying = false;
                carriedObject.rigidbody.isKinematic = false;
                carriedObject.transform.parent = null;
                carriedObject = null;
                CrossHair.drawCrosshair = true;

                print("Dropping");
            }
        }
        pickupframe = false;
    }


    void OnCollisionEnter(Collision col)
    {
        
    }

    void OnCollisionStay(Collision col)
    {
        if (((rightHand && Input.GetKeyDown(KeyCode.E)) || (!rightHand && Input.GetKeyDown(KeyCode.Q))) && !carrying)
        {
            Pickupable p = col.collider.GetComponent<Pickupable>();
            if (p != null)
            {
                carrying = true;
                carriedObject = p.gameObject;
                carriedObject.rigidbody.isKinematic = true;
                carriedObject.transform.parent = this.gameObject.transform;
                carriedObject.transform.localPosition = Vector3.zero;
                carriedObject.transform.localRotation = Quaternion.identity;

                pickupframe = true;

                CrossHair.drawCrosshair = false;
            }
        }
    }
}
