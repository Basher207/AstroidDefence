using UnityEngine;
using System.Collections;

public class SetInitialVel : MonoBehaviour {

	public Vector3 intialRigidBodyVel;

	void Awake () {
		GetComponent<Rigidbody> ().velocity = intialRigidBodyVel;
	}
}
