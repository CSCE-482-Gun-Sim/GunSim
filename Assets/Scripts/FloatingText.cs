using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {

	private string text;
	public enum AttachPoint{
		None,
		Gun,
		Magazine
	};
	public static AttachPoint attached;
	public static GameObject magazine;

	public void setText(string t)
	{
		this.GetComponent<TextMesh>().text = t;
	}

	// Use this for initialization
	void Start () {
		text = this.GetComponent<TextMesh> ().text;
		attached = AttachPoint.None;	
		this.gameObject.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		//when C is pressed, cycle through text locations
		if (Input.GetKeyDown (KeyCode.T)) {
			if (attached == AttachPoint.None){
				attached = AttachPoint.Gun;
			} else if (attached == AttachPoint.Gun){
				attached = AttachPoint.Magazine;
			} else if (attached == AttachPoint.Magazine){
				attached = AttachPoint.None;
			}
		}
		
		if(attached == AttachPoint.None) {
			this.GetComponent<TextMesh> ().text = "";
			this.gameObject.GetComponent<Renderer>().enabled = false;
		} else if(attached == AttachPoint.Gun) {
			this.GetComponent<TextMesh> ().text = "Press the primary button to pick up the gun.";
			this.gameObject.GetComponent<Renderer>().enabled = true;
		} else if(attached == AttachPoint.Magazine) {
			this.GetComponent<TextMesh> ().text = "Press the primary button to pick up the magazine.";
			this.gameObject.GetComponent<Renderer>().enabled = true;
		}
		
		//snap to object
		if (this.gameObject.GetComponent<Renderer>().enabled) {
			Transform snapTransform = null;
			if (attached == AttachPoint.Gun) {
				GameObject pistol = GameObject.FindGameObjectWithTag ("Pistol");
				snapTransform = pistol.transform;
			} else if (attached == AttachPoint.Magazine) {
				snapTransform = magazine.transform;
			} 

			Vector3 snapPosition = new Vector3(snapTransform.position.x + 0.4f, snapTransform.position.y, snapTransform.position.z);

			this.transform.position = new Vector3 (snapPosition.x - 0.5f, snapPosition.y, snapPosition.z);
			
		} 
		else {
			//Debug.Log ("Attaching to: (none)");
		}

		//rotate to player
		this.transform.rotation = Quaternion.LookRotation(this.transform.position - GameObject.FindGameObjectWithTag("FloatingTextFollower").transform.position);
	}
}
