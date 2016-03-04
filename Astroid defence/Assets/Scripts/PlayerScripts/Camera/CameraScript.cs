using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class CameraScript : MonoBehaviour {

	[SerializeField ] public Transform moveTo;


	[SerializeField] public float rotateFactor = 5;
	[SerializeField] public float moveFactor = 5;

	[HideInInspector] public Rigidbody rigidBody;

	void Awake () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void Update () {
		Vector3 axis;
		float angle;
		moveTo.rotation.ToAngleAxis (out angle, out axis);
		rigidBody.AddForce ((moveTo.position - transform.position).normalized * moveFactor * Time.deltaTime);
		rigidBody.AddTorque (axis / 180f * angle * rotateFactor);
	}
}
