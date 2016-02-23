using UnityEngine;
using System.Collections;
[RequireComponent (typeof (MoveScript))]
public class PlayerControlScript : MonoBehaviour {

	MoveScript moveScript;

	public KeyCode correctRotationKey = KeyCode.C;

	void Awake () {
		moveScript = GetComponent<MoveScript> ();
	}
	void Update () {
		if (Input.GetKeyDown (correctRotationKey)) {
			transform.eulerAngles = Vector3.zero;
		}
	}
	void FixedUpdate () {
		Vector2 arrow = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		float horizontal 	= Input.GetAxis ("Horizontal");
		float vertical 	 	= Input.GetAxis ("Vertical");
		float perpendicular = Input.GetAxis ("UpDown");

		moveScript.FixedMove (transform.forward * vertical + transform.right * horizontal, arrow.x, arrow.y, perpendicular, Input.GetKey (KeyCode.LeftShift));
	}
}
