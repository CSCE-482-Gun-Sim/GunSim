using UnityEngine;
using System.Collections;

public abstract class Pickupable : MonoBehaviour {

	public Vector3 handRotationOffset;
	public Vector3 handPositionOffset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void carryUpdate(Hand hand)
	{
	}
}
