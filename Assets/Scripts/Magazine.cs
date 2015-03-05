using UnityEngine;
using System.Collections;

public class Magazine : MonoBehaviour {
	private string text;
	enum AttachPoint{
		None,
		Hand,
		Gun
	};
	public Vector3 handRotation;
	public Vector3 pistolRotation;
	bool needsToLerp = false;
	float timer;

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
				needsToLerp = true;
				attached = AttachPoint.Gun;
			} else if (attached == AttachPoint.Gun){
				attached = AttachPoint.None;
				this.rigidbody.isKinematic = false;
				this.transform.parent = null;
			} else {
				needsToLerp = true;
				attached = AttachPoint.Hand;
			}
		}

		//lerp to position
		//if (needsToLerp) {
			Transform snapTransform = null;
			GameObject hand = null;
			GameObject pistol = null;
			if (attached == AttachPoint.Hand) {
				hand = GameObject.FindGameObjectWithTag ("LeftHand");
				snapTransform = hand.transform;
			} else if (attached == AttachPoint.Gun) {
				pistol = GameObject.FindGameObjectWithTag ("Pistol");
				snapTransform = pistol.transform;
			}
			
			if (snapTransform != null) {
				Vector3 snapPosition = snapTransform.position;
				//Debug.Log (snapPosition.x + " , " + snapPosition.z);
				this.transform.position = new Vector3 (snapPosition.x + 0.20f, snapPosition.y, snapPosition.z);
				
				this.rigidbody.isKinematic = true;
				Vector3 rotationVec = Vector3.zero;
				if(attached == AttachPoint.Hand){
					rotationVec = handRotation;
					this.transform.parent = hand.transform;
				}else if (attached == AttachPoint.Gun) {
					rotationVec = pistolRotation;
					this.transform.parent = pistol.transform;
				}
				this.transform.localPosition = Vector3.zero; 

				if(rotationVec != Vector3.zero){
					this.transform.localRotation = Quaternion.Euler(rotationVec.x, rotationVec.y, rotationVec.z);
				}
			}
		//}

	}

	private void lerpToPosition (Vector3 startPos, Vector3 endPos, float time) {
		float i = 0.0f;
		float rate = 1.0f/time;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			this.transform.position = Vector3.Lerp(startPos, endPos, i);
		}
	}
}
