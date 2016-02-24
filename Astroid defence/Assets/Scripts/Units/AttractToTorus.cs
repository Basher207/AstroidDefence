using UnityEngine;
using System.Collections;

public class AttractToTorus : MonoBehaviour {

	[SerializeField] public TorusNavigator navigator;
	[SerializeField] public float gravity = 9.8f;

	[HideInInspector] public Rigidbody rigidBody;

	void Awake () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	void FixedUpdate () {
		if (navigator == null)
			return;
		Vector3 attractionVector = TorusNavigator.AttractTo (transform.position) - transform.position;
		rigidBody.AddForce (attractionVector.normalized * gravity);
	}
}
