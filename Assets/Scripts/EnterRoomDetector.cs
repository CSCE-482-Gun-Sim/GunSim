using UnityEngine;
using System.Collections;

public class EnterRoomDetector : MonoBehaviour
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
				if ( !triggered ) {
						triggered = true;
						ScreenText.warning = ScreenText.Warning.PlayerEntered;
				}
		}
}
