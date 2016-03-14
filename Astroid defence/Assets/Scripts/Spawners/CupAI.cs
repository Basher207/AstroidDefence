using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent (typeof (Rigidbody))]
public class CupAI : MonoBehaviour {

	public static GameObject prefab;

	[SerializeField]  public List <Transform> waypoints;
	[SerializeField]  public int dropIndex;
	[SerializeField]  public int hideIndex;
	[HideInInspector] public int currentIndex;

	[SerializeField] public float aclDistanceFactor;
	[SerializeField] public float minForce;
	[SerializeField] public float rotationForce = 5f;
	[HideInInspector] public Rigidbody rigidBody;

	void Awake () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void Update () {
		Vector3 delta = waypoints [currentIndex].position - transform.position;
		delta *= aclDistanceFactor;
		if (delta.magnitude < minForce) {
			delta = Math.SetMag (delta, minForce);
		}
		rigidBody.AddForce (delta);
		float distance = Vector3.Distance (waypoints [currentIndex].position, transform.position);
		if (distance < 200f) {
			Quaternion rotateTowards = Quaternion.FromToRotation (transform.forward, waypoints [currentIndex].forward);
			float angle;
			Vector3 axis;
			rotateTowards.ToAngleAxis (out angle, out axis);
			rigidBody.AddTorque (axis * angle * rotationForce);
			if (currentIndex == dropIndex) {
				Rigidbody [] childrenRigidBodies = GetComponentsInChildren <Rigidbody> ();
				foreach (Rigidbody rigid in childrenRigidBodies) {
					rigid.isKinematic = false;
				}
			}
			if (distance < 2f) {
				currentIndex++;
				if (currentIndex == waypoints.Count) {
					Destroy (gameObject);
				}
			}
		}
	}
}
