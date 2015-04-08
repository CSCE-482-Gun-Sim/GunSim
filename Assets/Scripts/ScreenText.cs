using UnityEngine;
using System.Collections;

public class ScreenText : MonoBehaviour {
	public enum Warning{
		None,
		PointDownRange,
		PlayerEntered,
		ShortSafetyMessage,
		LongSafetyMessage,
		FireMessage,
		LoadTheWeapon,
		LoadAMagazine,
		EmptyClip,
		StandAtRange
	};
	public static Warning warning;
	public static bool dontShowFirstMessage = false;
	public static bool firedOnce = false;

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

		Debug.Log ("Safety state: " + warning);
		string warningString = "";
		if (warning == Warning.None) {
			warningString = "";
		} else if (warning == Warning.StandAtRange) {
			warningString = "Please stand in front of the range.";
		} else if (warning == Warning.PointDownRange) {
			warningString = "Please keep the gun pointed down range.";
		} else if (warning == Warning.PlayerEntered) {
			warningString = "Place a hand near an object to pick it up. Pick up a pistol with one hand and a magazine with another.";
		} else if (warning == Warning.ShortSafetyMessage) {
			warningString = "Turn off the safety before firing by placing your hand next to the gun.";
		} else if (warning == Warning.LongSafetyMessage) {
			warningString = "Before firing, you need to turn off the safety. If you see a red dot on the left side of the gun, the safety is off and the gun is then ready to fire. Place your hand next to the gun to turn off the safety.";
		} else if (warning == Warning.FireMessage) {
			warningString = "With your gun-carrying hand, pull the trigger to fire the pistol.";
		} else if (warning == Warning.LoadTheWeapon) {
			warningString = "Hold the magazine under the pistol to load the weapon.";
		} else if (warning == Warning.LoadAMagazine) {
			warningString = "First load the gun with a magazine by picking one up and holding the magazine under the gun.";
		} else if (warning == Warning.EmptyClip) {
			warningString = "Your magazine is empty. Press the right primary button to drop the magazine, then pick up another one and load it in the pistol.";
		} 

		var centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		centeredStyle.fontSize = 30;
		GUI.Label(new Rect (Screen.width/2-250, Screen.height-200, 500, 500), warningString, centeredStyle);

		//GUI.Label(new Rect(Screen.width/2.0f,Screen.height/2.0f,Screen.width,Screen.height),warningString);
	}
}
