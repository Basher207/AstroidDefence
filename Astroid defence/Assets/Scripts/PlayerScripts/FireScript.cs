using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

	public GameObject toFire;
	public Vector3 offset;
	public float bulletSpeed;

	public KeyCode fireButton;
	public void Update () {
		if (Input.GetKeyDown (fireButton)) {
			Fire ();
		}
	}
	public void Fire () {
		GameObject justSpawned = Instantiate (toFire, transform.TransformPoint (offset), transform.rotation) as GameObject;
		justSpawned.GetComponent<Rigidbody> ().velocity = bulletSpeed * transform.forward;
	}
}
