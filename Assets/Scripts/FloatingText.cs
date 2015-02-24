using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {
	private string text;
	enum AttachPoint{
		None,
		Camera,
		Gun
	};
	AttachPoint attached;

	// Use this for initialization
	void Start () {
		text = this.GetComponent<TextMesh> ().text;
		attached = AttachPoint.Camera;	
	}

//	void initialize(string t)
//	{
//		text = t;
//		this.GetComponent<TextMesh> ().text = text;
//	}
	
	// Update is called once per frame
	void Update () {

		//when C is pressed, cycle through text locations
		if (Input.GetKeyDown (KeyCode.C)) {
			if (attached == AttachPoint.Camera){
				attached = AttachPoint.Gun;
			} else if (attached == AttachPoint.Gun){
				attached = AttachPoint.None;
			} else {
				attached = AttachPoint.Camera;
			}
		}

		if(attached == AttachPoint.Camera) {
			this.GetComponent<TextMesh> ().text = "Press C to snap to Gun";
			this.gameObject.renderer.enabled = true;
		} else if(attached == AttachPoint.Gun) {
			this.GetComponent<TextMesh> ().text = "Press C to dismiss";
			this.gameObject.renderer.enabled = true;
		} else {
			this.GetComponent<TextMesh> ().text = "";
			this.gameObject.renderer.enabled = false;
		}

		//snap to object
		if (this.gameObject.renderer.enabled) {
			Transform snapTransform = null;
			if (attached == AttachPoint.Camera) {
				snapTransform = Camera.main.transform;
			} else if (attached == AttachPoint.Gun) {
				GameObject pistol = GameObject.FindGameObjectWithTag ("Pistol");
				snapTransform = pistol.transform;
			}
			//Debug.Log ("Attaching to: " + snapTransform.gameObject.name);

			Vector3 snapPosition = snapTransform.position;
			//snapPosition -= snapTransform.TransformPoint (Vector3.forward * 10);
			Debug.Log (snapPosition.x + " , " + snapPosition.z);
			//this.transform.position = new Vector3 (Camera.main.transform.position.x - 10, 0, Camera.main.transform.position.z + 0);
			this.transform.position = new Vector3 (snapPosition.x - 0.5f, snapPosition.y, snapPosition.z);
		} else {
			//Debug.Log ("Attaching to: (none)");
		}
	}
}
