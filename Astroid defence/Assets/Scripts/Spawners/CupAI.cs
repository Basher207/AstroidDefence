using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent (typeof (Rigidbody))]
public class CupAI : MonoBehaviour {

	public static GameObject prefab;

	public static void SendRigidBodies (Rigidbody [] bodies) {
		if (prefab == null) {
			prefab = Resources.Load <GameObject> ("Prefabs/Spawners/CupAI");
		}
		GameObject newCup = Instantiate (prefab) as GameObject;
		//Index of movingCup;
		newCup = newCup.transform.GetChild (2).gameObject;
		foreach (Rigidbody rigid in bodies) {
			rigid.transform.SetParent (newCup.transform);
			rigid.transform.localPosition = Random.onUnitSphere * 4f;
			rigid.isKinematic = true;
		}
	}
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
		if (distance < 400f) {
			Quaternion rotateTowards = Quaternion.FromToRotation (transform.forward, waypoints [currentIndex].forward);
			float angle;
			Vector3 axis;
			rotateTowards.ToAngleAxis (out angle, out axis);
			if (distance < ((dropIndex == currentIndex) ? 150f : 500f)) {
				rigidBody.AddTorque (axis * angle * rotationForce);
			}
			if (currentIndex == dropIndex && angle < 70f) {
				Rigidbody [] childrenRigidBodies = GetComponentsInChildren <Rigidbody> ();
				foreach (Rigidbody rigid in childrenRigidBodies) {
					rigid.isKinematic = false;
					rigid.velocity = rigidBody.velocity;
					rigid.transform.SetParent (null);
				}
			}
			if (distance < 40f) {
				currentIndex++;
				if (currentIndex == waypoints.Count) {
					Destroy (gameObject);
				}
			}
		}
	}
}
