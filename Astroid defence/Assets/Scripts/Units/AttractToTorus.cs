using UnityEngine;
using System.Collections;

public class AttractToTorus : MonoBehaviour {

	[SerializeField] public float gravity = 9.8f;
	[SerializeField] public float moveForce;

	[SerializeField] public LayerMask shipLayer;
	[SerializeField] public float distanceToMove;
	[HideInInspector] public Rigidbody rigidBody;

	Vector3 movementVector;
	void Awake () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	void FixedUpdate () {
		Vector3 attractionVector = TorusNavigator.AttractTo (transform.position) - transform.position;

		RaycastHit hit;
		if (Physics.Raycast (transform.position, attractionVector, out hit, distanceToMove, shipLayer)) {
			movementVector = TorusNavigator.TriangleIndexToMovementVector (hit.triangleIndex * 3);
			rigidBody.AddForce (movementVector.normalized * moveForce);
		}
		rigidBody.AddForce (attractionVector.normalized * gravity);
	}
}
