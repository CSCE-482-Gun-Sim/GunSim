using UnityEngine;
using System.Collections;

public class Pickup_Object : MonoBehaviour
{
    GameObject mainCamera;
    bool carrying;
    public GameObject carriedObject;
    public float distance;
    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");

		carrying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (carrying)
        {
			//if this carried object is a gun
			if(carriedObject.GetComponent("AbstractGun") as AbstractGun != null){
				AbstractGun thisGun = carriedObject.GetComponent("AbstractGun") as AbstractGun;
				Vector3 gunHeightCorrection = new Vector3(0.0f, -thisGun.gunHeight, 0.0f);
				carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, mainCamera.transform.position + gunHeightCorrection + mainCamera.transform.forward * distance, Time.deltaTime * 12f);
				carriedObject.transform.rotation = mainCamera.transform.rotation;
			}
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                carrying = false;
                carriedObject.rigidbody.isKinematic = false;
                carriedObject = null;
            }
        }
        else
        {
            pickup();
        }
    }


    void pickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            Ray ray = mainCamera.camera.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Pickupable p = hit.collider.GetComponent<Pickupable>();
                if (p != null)
                {
                    carrying = true;
                    carriedObject = p.gameObject;
                    carriedObject.rigidbody.isKinematic = true;
                }

            }
        }
    }
}
