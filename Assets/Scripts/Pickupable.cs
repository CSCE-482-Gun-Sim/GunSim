using UnityEngine;
using System.Collections;

public abstract class Pickupable : MonoBehaviour
{

		public Vector3 handRotationOffset;
		public Vector3 handPositionOffset;
		public bool pickedUp;

		// Use this for initialization
		void Start()
		{
				pickedUp = false;
		}
	
		// Update is called once per frame
		void Update()
		{
	
		}

		public virtual void carryUpdate(Hand hand)
		{
		}
}
