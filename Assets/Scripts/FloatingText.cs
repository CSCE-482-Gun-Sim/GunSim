using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {
	public float speed = 1.0F;
	Vector3 start;
	float startTime;
	
	private string text;
	public enum AttachPoint{
		None,
		Camera,
		Gun
	};
	public AttachPoint attached;

	public void setText(string t)
	{
		this.GetComponent<TextMesh>().text = t;
	}

	// Use this for initialization
	void Start () {
		text = this.GetComponent<TextMesh> ().text;
		attached = AttachPoint.None;	
		start = this.transform.position;
		this.gameObject.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		//when C is pressed, cycle through text locations
		/*if (Input.GetKeyDown (KeyCode.C)) {
			if (attached == AttachPoint.Camera){
				attached = AttachPoint.Gun;
			} else if (attached == AttachPoint.Gun){
				this.gameObject.renderer.enabled = false;
			} else {
				attached = AttachPoint.Camera;
				this.gameObject.renderer.enabled = true;
			}
			startTime = Time.time;
		}*/
		
		/*if(attached == AttachPoint.Camera) {
			this.GetComponent<TextMesh> ().text = "Press C to snap to Gun";
			this.gameObject.renderer.enabled = true;
		} else if(attached == AttachPoint.Gun) {
			this.GetComponent<TextMesh> ().text = "Press C to dismiss";
			this.gameObject.renderer.enabled = true;
		} else {
			this.GetComponent<TextMesh> ().text = "";
			this.gameObject.renderer.enabled = false;
		}*/
		
		//snap to object
		if (this.gameObject.GetComponent<Renderer>().enabled) {
			Transform snapTransform = null;
			if (attached == AttachPoint.Camera) {
				snapTransform = Camera.main.transform;
			} else if (attached == AttachPoint.Gun) {
				GameObject pistol = GameObject.FindGameObjectWithTag ("Pistol");
				snapTransform = pistol.transform;
			}

			Vector3 snapPosition = snapTransform.position;

			//Vector3 end = new Vector3 (snapPosition.x - 0.5f, snapPosition.y, snapPosition.z);
			
			//float journeyLength = Vector3.Distance(start, end);
			
			//float distCovered = (Time.time - startTime) * speed;
			//float fracJourney = distCovered / journeyLength;
			
			//this.transform.position = Vector3.MoveToward(start, end, fracJourney);
			
			
			this.transform.position = new Vector3 (snapPosition.x - 0.5f, snapPosition.y, snapPosition.z);
			
		} 
		else {
			//Debug.Log ("Attaching to: (none)");
		}
	}
}
