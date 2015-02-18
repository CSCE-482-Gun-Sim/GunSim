using UnityEngine;
using System.Collections;

public class Pickup_Object : MonoBehaviour
{
    GameObject mainCamera;
    public Hand rHand;
    public Hand lHand;
    public float distance;
    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        rHand = GameObject.FindWithTag("RightHand").GetComponent("Hand") as Hand;
        lHand = GameObject.FindWithTag("LeftHand").GetComponent("Hand") as Hand;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
