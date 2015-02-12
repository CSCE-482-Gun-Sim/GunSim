private var motor : CharacterMotor;

// Use this for initialization
function Awake () {
	motor = GetComponent(CharacterMotor);
}

// Update is called once per frame
function Update () {
	// Get the input vector from keyboard or analog stick
	var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

	if (directionVector != Vector3.zero) {
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength = directionVector.magnitude;
		directionVector = directionVector / directionLength;
		
		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1, directionLength);
		
		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;
		
		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
	}

	//Shooting ray, has some level of hit detection, will draw a line representing the ray
	if (Input.GetMouseButtonDown(0)) {
		var gunObject = GameObject.Find("1911_Pistol_01");

		var fwd = 10 * transform.TransformDirection (Vector3.forward);
		var hit : RaycastHit;
		if (Physics.Raycast (gunObject.transform.position, fwd, hit)) {
			//print ("There is something in front of the object!");
			Debug.DrawRay (gunObject.transform.position, fwd, Color.green, 100);
			//print('Collision at point ' + hit.point);
			if(hit.collider.gameObject.name == 'Target')
				hit.collider.gameObject.renderer.material.color = Color(Random.Range(0.0,1.0),Random.Range(0.0,1.0),Random.Range(0.0,1.0));
		}
		

		/*var rayOrigin = Camera.main.ScreenToWorldPoint();
		var fwd = transform.TransformDirection (Vector3.forward);
		var hit : RaycastHit;
		if (Physics.Raycast (rayOrigin, fwd, 100)) {
			print("HIT SOMETHING");

			//Debug.DrawLine (rayOrigin, hit.point);
		}*/
	}


	// Apply the direction to the CharacterMotor
	motor.inputMoveDirection = transform.rotation * directionVector;
	motor.inputJump = Input.GetButton("Jump");
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
@script AddComponentMenu ("Character/FPS Input Controller")
