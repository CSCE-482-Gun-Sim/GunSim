using UnityEngine;
using System.Collections;

public class Magazine : MonoBehaviour {
	private string text;
	enum AttachPoint{
		None,
		Hand,
		Gun
	};
	AttachPoint attached;
	
	// Use this for initialization
	void Start () {
		attached = AttachPoint.None;	
	}

	// Update is called once per frame
	void Update () {

		//when C is pressed, cycle through text locations
		if (Input.GetKeyDown (KeyCode.V)) {
			if (attached == AttachPoint.Hand){
				attached = AttachPoint.Gun;
			} else if (attached == AttachPoint.Gun){
				attached = AttachPoint.None;
			} else {
				attached = AttachPoint.Hand;
			}
		}
		
		Transform snapTransform = null;
		if (attached == AttachPoint.Hand) {
			GameObject hand = GameObject.FindGameObjectWithTag ("LeftHand");
			snapTransform = hand.transform;
		} else if (attached == AttachPoint.Gun) {
			GameObject pistol = GameObject.FindGameObjectWithTag ("Pistol");
			snapTransform = pistol.transform;
		}
		//Debug.Log ("Attaching to: " + snapTransform.gameObject.name);

		if (snapTransform != null) {
			Vector3 snapPosition = snapTransform.position;
			Debug.Log (snapPosition.x + " , " + snapPosition.z);
			this.transform.position = new Vector3 (snapPosition.x, snapPosition.y, snapPosition.z);
		}
	}
}
