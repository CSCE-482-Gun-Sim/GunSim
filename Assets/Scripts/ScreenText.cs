using UnityEngine;
using System.Collections;

public class ScreenText : MonoBehaviour {
	public enum Warning{
		None,
		PointDownRange,
		PlayerEntered
	};
	public static Warning warning;

	// Use this for initialization
	void Start () {
		warning = Warning.None;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		Debug.Log ("test: " + warning);
		string warningString = "";
		if (warning == Warning.None) {
			warningString = "";
		} else if (warning == Warning.PointDownRange) {
			warningString = "Please keep the gun pointed down range.";
		} else if (warning == Warning.PlayerEntered) {
			warningString = "Place a hand near an object to pick it up. Pick up a pistol with one hand and a magazine with another.";
		} 
		GUI.Label(new Rect(Screen.width/2.0f,Screen.height/2.0f,Screen.width,Screen.height),warningString);
	}
}
