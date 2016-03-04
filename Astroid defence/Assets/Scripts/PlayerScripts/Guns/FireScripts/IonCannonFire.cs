using UnityEngine;
using System.Collections;

public class IonCannonFire : MonoBehaviour {

	[SerializeField] public float coolDown 	= 5f;
	[SerializeField] public float offset 	= 1f;

	[SerializeField] public float speed 	= 20f;
	[SerializeField] public float destroyAfterTime = 7f;
	[SerializeField] public GameObject bullet;

	[SerializeField] public Transform yRotation;

	[SerializeField] public Vector3 point;

	[HideInInspector] public float timeToFire;

	void Awake () {
		originalRight 	= transform.right;
		originalNormal  = transform.up;
	}
	void Start () {
		timeToFire = Time.time + coolDown;
	}
	void FixedUpdate () {
		if (Time.time > timeToFire) {
			timeToFire = Time.time + coolDown;
			GameObject bullet = Instantiate (this.bullet, transform.position + transform.up * offset, transform.rotation) as GameObject;
			Destroy (bullet, destroyAfterTime);
			bullet.GetComponent<Rigidbody> ().velocity = transform.up * speed;
		}
		SetDirection ();
	}

	[HideInInspector] private Vector3 originalNormal;
	[HideInInspector] private Vector3 originalRight;

	[SerializeField] public Transform target;

	void SetDirection () {
		Vector3 delta = (Camera.main.transform.position - transform.position).normalized;
		Vector3 mainDelta = delta;
		float yDot = Vector3.Dot (originalNormal, delta);
		delta -= originalNormal * yDot;
		delta.Normalize ();
		yRotation.LookAt (transform.position + delta, originalNormal);
		transform.LookAt (yRotation.right + yRotation.position, mainDelta);
	}
	void OnDrawGizmos () {
		Vector3 delta = (Camera.main.transform.position - transform.position).normalized;
		Vector3 mainDelta = delta;
		float yDot = Vector3.Dot (originalNormal, delta);
		delta -= originalNormal * yDot;
		delta.Normalize ();
		Gizmos.DrawLine (transform.position, transform.position + delta * 15f);
	}
}
