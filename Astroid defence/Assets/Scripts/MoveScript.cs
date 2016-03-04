using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Rigidbody))]
public class MoveScript : MonoBehaviour {

	public float force = 100;
	public float turboForce = 400;
	public float torqueForce = 280;

	public float correctionForce = 20f;

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

		Vector3 xTorque = Mathf.Clamp (xRotation, -1, 1) * transform.right * torqueForce;
		Vector3 yTorque = Mathf.Clamp (yRotation, -1, 1) * Vector3.up 	   * torqueForce;


		Vector3 angularVelocity  = yTorque * Time.deltaTime;

		Vector3 euler = Vector3.zero;
		euler.y += yRotation * Time.deltaTime * torqueForce;
		euler.x += xRotation * Time.deltaTime *-torqueForce;
		
		Quaternion rotation = transform.rotation * Quaternion.Euler (euler);


		rotation = Quaternion.RotateTowards (rotation, Quaternion.LookRotation (rotation * Vector3.forward, Vector3.up), correctionForce * Time.deltaTime);

		rigid.MoveRotation (rotation);
		rigid.velocity = vel;
		
	}
}
