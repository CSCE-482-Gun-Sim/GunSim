using UnityEngine;
using System.Collections;

public class ScreenText : MonoBehaviour {
	public enum Warning{
		None,
		PointDownRange,
		PlayerEntered
	};
	public static Warning warning;
	public static bool dontShowFirstMessage = false;

	// Use this for initialization
	void Start () {
		warning = Warning.None;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		//once pistol and magazine have been picked up, dont show first message anymore
		if(warning == Warning.PlayerEntered && dontShowFirstMessage){
			warning = Warning.None;
		}

		string warningString = "";
		if (warning == Warning.None) {
			warningString = "";
		} else if (warning == Warning.PointDownRange) {
			warningString = "Please keep the gun pointed down range.";
		} else if (warning == Warning.PlayerEntered) {
			warningString = "Place a hand near an object to pick it up. Pick up a pistol with one hand and a magazine with another.";
		} 

		var centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = 30;
		GUI.Label(new Rect (Screen.width/2-250, Screen.height-200, 500, 500), warningString, centeredStyle);

		//GUI.Label(new Rect(Screen.width/2.0f,Screen.height/2.0f,Screen.width,Screen.height),warningString);
	}
}
