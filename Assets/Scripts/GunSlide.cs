using UnityEngine;
using System.Collections;

public class GunSlide : MonoBehaviour
{
		public enum SlideTarget
		{
				Ready,
				Back,
				Forwards,
				Backwards
	}
		;
		public Transform emptyBulletShell;
		public Transform ejectedBullet;
		public SlideTarget target;
		AbstractGun G;
		Vector3 startingPosition;
		Vector3 finalKickPosition;
		Vector3 targetPosition;
		float speed = 20.0f;

		// Use this for initialization
		void Start()
		{
				targetPosition = this.transform.localPosition;
				target = SlideTarget.Ready;	
				startingPosition = this.transform.localPosition;
				finalKickPosition = new Vector3 (startingPosition.x + 0.8f, startingPosition.y, startingPosition.z); 
				G = GameObject.FindGameObjectWithTag ("Pistol").GetComponent<AbstractGun> ();
		}
	
		// Update is called once per frame
		void Update()
		{
		//print (target.ToString());
				if ( target != SlideTarget.Ready || target != SlideTarget.Back) {
						float step = speed * Time.deltaTime;
						this.transform.localPosition = Vector3.MoveTowards (this.transform.localPosition, targetPosition, step);
				}

				if ( target == SlideTarget.Backwards ) {
						//slide has reached final kick position, go forwards
						if ( this.transform.localPosition == finalKickPosition ) {
								if ( G.loadedMagazine != null && G.loadedMagazine.ammo > 0 ) {
										SetForward ();
								} else {
										target = SlideTarget.Back;
								}
						}
				} else if ( target == SlideTarget.Forwards ) {
								if ( this.transform.localPosition == startingPosition ) {
										target = SlideTarget.Ready;
								}
						}
		}

		public void SetForward()
		{
				target = SlideTarget.Forwards;
				targetPosition = startingPosition;

				if ( G.loadedMagazine != null && G.loadedMagazine.ammo > 0 ) {
						G.bulletInChamber = true;
						G.loadedMagazine.ammo--;

			Object g = Instantiate (ejectedBullet, this.gameObject.transform.position, Quaternion.identity);
				}
		}

		public void SetBack()
		{
		target = SlideTarget.Backwards;
				targetPosition = finalKickPosition;
		}

		public void slideFire()
		{
				SetBack ();
		}
}
