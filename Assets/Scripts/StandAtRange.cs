using UnityEngine;
using System.Collections;

public class StandAtRange : MonoBehaviour
{
		bool triggered = false;
		ScreenText.Warning lastWarning;
	
		// Use this for initialization
		void Start()
		{
		
		}
	
		// Update is called once per frame
		void Update()
		{
		
		}
	
		void OnTriggerEnter(Collider col)
		{
				//if ( !triggered && ScreenText.dontShowFirstMessage ) {
				//		triggered = true;
				//		if(lastWarning == null)
				//			ScreenText.warning = ScreenText.Warning.None;
				//		else
				//			ScreenText.warning = lastWarning;
				//}
		}

		void OnTriggerExit(Collider col)
		{
				//if ( ScreenText.dontShowFirstMessage ) {
				//		triggered = false;
				//		lastWarning = ScreenText.warning;
				//		ScreenText.warning = ScreenText.Warning.StandAtRange;
				//}
		}
}
