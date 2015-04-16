using UnityEngine;
using System.Collections;

public class StandAtRange : MonoBehaviour
{
		bool triggered = false;
	
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
				if ( !triggered && ScreenText.dontShowFirstMessage ) {
						triggered = true;
						ScreenText.warning = ScreenText.Warning.None;
				}
		}

		void OnTriggerExit(Collider col)
		{
				if ( ScreenText.dontShowFirstMessage ) {
						triggered = false;
						ScreenText.warning = ScreenText.Warning.StandAtRange;
				}
		}
}
