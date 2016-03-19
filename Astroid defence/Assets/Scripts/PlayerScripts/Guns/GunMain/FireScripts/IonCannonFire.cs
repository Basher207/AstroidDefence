using UnityEngine;
using System.Collections;

public class IonCannonFire : MonoBehaviour {

	[SerializeField] public float coolDown 	= 5f;
	[SerializeField] public float offset 	= 1f;

	[SerializeField] public float speed 	= 20f;
	[SerializeField] public float destroyAfterTime = 7f;
	[SerializeField] public float maxRotationAngle = 40f;
	[SerializeField] public GameObject bullet;

	[SerializeField] public Transform yRotation;

	[SerializeField] public Vector3 point;
	[SerializeField] public LayerMask layerMask;
	[HideInInspector] public float timeToFire;

	void Awake () {
		originalNormal  = transform.up;
		point = transform.position + originalNormal;
	}
	void Start () {
		timeToFire = Time.time + coolDown;
	}
	void Update () {
		if (Input.GetKey (KeyCode.P)) {
			RaycastHit hit;
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, layerMask)) {
				Vector3 delta = hit.point - transform.position;
				if (Vector3.Angle (delta, originalNormal) < maxRotationAngle)
					point = hit.point;
			}
		}
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

	void SetDirection () {
		Vector3 delta = (point - transform.position).normalized;
		Vector3 mainDelta = delta;
		float yDot = Vector3.Dot (originalNormal, delta);
		delta -= originalNormal * yDot;
		delta.Normalize ();
		yRotation.LookAt (transform.position + delta, originalNormal);
		transform.LookAt (yRotation.right + yRotation.position, mainDelta);
	}
}
