using UnityEngine;
using System.Collections;

public class GunSlide : MonoBehaviour
{
		enum SlideTarget
		{
				Forwards,
				Backwards,
				None
	}
		;
		SlideTarget target;
		Vector3 startingPosition;
		Vector3 finalKickPosition;
		float speed = 20.0f;

		// Use this for initialization
		void Start()
		{
				target = SlideTarget.None;	
				startingPosition = this.transform.localPosition;
				finalKickPosition = new Vector3 (startingPosition.x + 0.8f, startingPosition.y, startingPosition.z); 
		}
	
		// Update is called once per frame
		void Update()
		{
				//slide needs to move
				if ( target != SlideTarget.None ) {
						float step = speed * Time.deltaTime;
						Vector3 targetPosition = new Vector3 ();
						if ( target == SlideTarget.Backwards ) {
								//slide has reached final kick position, go forwards
								if ( this.transform.localPosition == finalKickPosition ) {
										target = SlideTarget.Forwards;
										targetPosition = startingPosition;
								} else {
										targetPosition = finalKickPosition;
								}
						} else {	//move forwards
								if ( this.transform.localPosition == startingPosition ) {
										target = SlideTarget.None;	//done moving
								}
								targetPosition = startingPosition;
						}
						this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, targetPosition, step);
				}
		}

		public void slideAction()
		{
				target = SlideTarget.Backwards;
		}
}
