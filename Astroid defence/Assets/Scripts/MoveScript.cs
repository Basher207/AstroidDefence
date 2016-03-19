using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Rigidbody))]
public class MoveScript : MonoBehaviour {

	public float force = 100;
	public float turboForce = 400;
	public float torqueForce = 280;

	public float correctionForce = 20f;
	public float angleForCorrectionScale = 180f;

	public float perpendicularTurboFactor = 1.5f;
	public float perpendicularForce = 50;
	[HideInInspector] public Rigidbody rigid;

	void Awake () {
		rigid = GetComponent<Rigidbody> ();
		rigid.maxAngularVelocity = 0;
	}

	public void FixedMove (Vector3 appliedForce, float yRotation, float xRotation, float perpendicularFactor, bool turbo = false) {
		appliedForce = Vector3.ClampMagnitude (appliedForce, 1) * (turbo ? turboForce : force);


		Vector3 vel =  transform.up * perpendicularForce * perpendicularFactor;
		if (turbo)
			vel *= perpendicularTurboFactor;
		vel += appliedForce;

		Quaternion xRotationQuat = Quaternion.AngleAxis (xRotation * Time.deltaTime *-torqueForce, Vector3.right);
		Quaternion yRotationQuat = Quaternion.AngleAxis (yRotation * Time.deltaTime * torqueForce, Vector3.up);

		Quaternion rotation = transform.rotation;
		rotation *= yRotationQuat;
		rotation *= xRotationQuat;

		Quaternion correctedQuaternion = Quaternion.LookRotation (rotation * Vector3.forward, Vector3.up);
		rotation = Quaternion.RotateTowards (rotation, correctedQuaternion, correctionForce * Time.deltaTime);

		rigid.MoveRotation (rotation);
		rigid.velocity = vel;
		
	}
}
