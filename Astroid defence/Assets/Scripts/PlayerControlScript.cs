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
	}
	void FixedUpdate () {
		Vector2 arrow = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		float horizontal 	= Input.GetAxis ("Horizontal");
		float vertical 	 	= Input.GetAxis ("Vertical");
		float perpendicular = Input.GetAxis ("UpDown");

		moveScript.FixedMove (transform.forward * vertical + transform.right * horizontal, arrow.x, arrow.y, perpendicular, Input.GetKey (KeyCode.LeftShift));
	}
	void OnDrawGizmos () {
		Vector3 targetRight = transform.right;
		targetRight.y = 0;
		targetRight.Normalize ();
		Quaternion rotateBy = Quaternion.FromToRotation (transform.right, targetRight);
		Gizmos.DrawLine (transform.position + transform.right * 5f, transform.position);
		Gizmos.DrawLine (transform.position + targetRight * 5f, transform.position);
	}
}
