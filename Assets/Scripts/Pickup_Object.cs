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
    }

    // Update is called once per frame
    void Update()
    {
        if (carrying)
        {

            carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * 12f);
            carriedObject.transform.rotation = mainCamera.transform.rotation;
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
