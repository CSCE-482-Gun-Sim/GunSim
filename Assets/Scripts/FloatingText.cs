using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {
	public string text;
	bool textOn = false;


	// Use this for initialization
	void Start () {
		text = this.GetComponent<TextMesh> ().text;
	}

	void initialize(string t)
	{
		text = t;
		this.GetComponent<TextMesh> ().text = text;
	}
	
	// Update is called once per frame
	void Update () {

		//turns displayed text on and off
		if (Input.GetKeyDown (KeyCode.C)) {
			textOn = !textOn;
			if(textOn) {
				this.GetComponent<TextMesh> ().text = text;
			}
			else {
				this.GetComponent<TextMesh> ().text = "";
			}
		}

	}
}
