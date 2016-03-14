using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Collider))]
public class NormalGunFireScript : MonoBehaviour {

	List <Collider> toFireAt;
	GameObject shipBullet;

	void Start () {
		toFireAt = new List<Collider> ();
		toFireAt.Add (GameObject.Find ("Main Camera").GetComponentInChildren <Collider> ());
	}
	void Update () {

	}
	void Fire () {
		//transform.LookAt (toFireAt [0].transform.position);
	}
	void OnTriggerEnter (Collider coll) {

	}
}
