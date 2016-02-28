using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Resource;
using Game.Guns;

public class GunFire : MonoBehaviour {

	public static GameObject laserBullet;

	[SerializeField] public List<Rigidbody> targetRigidBodies;

	public float fireSpeed;
	public float offset = 1.6f;

	public float energyPerShot = 4;
	public float coolDown = 0.2f;

	[HideInInspector] public float timeToAllowShooting;

	public LayerMask ship;
	public LayerMask enemies;
	void Awake () {
		if (laserBullet == null)
			laserBullet = Resources.Load <GameObject> ("Prefabs/Guns/GunBullet");
		Collider [] colls = Physics.OverlapSphere (transform.position, GetComponent<SphereCollider> ().radius, enemies);
		targetRigidBodies = new List<Rigidbody> ();
		for (int i = colls.Length - 1; i >= 0; i--)
			targetRigidBodies.Add (colls[i].attachedRigidbody);
	}
	void FixedUpdate () {
		if (Time.time > timeToAllowShooting && targetRigidBodies.Count != 0) {
			if (!GameResources.UseEnergy (energyPerShot))
				return;

			Rigidbody nextTarget = NextTarget ();
			if (nextTarget == null)
				return;
			timeToAllowShooting = Time.time + coolDown;

			transform.parent.rotation = Quaternion.FromToRotation (Vector3.forward, Math.VectorToIntercept (nextTarget, transform.position, fireSpeed));
			GameObject bullet = Instantiate (laserBullet) as GameObject;
			bullet.transform.SetParent (transform, false);
			bullet.transform.SetParent (null, true);
			GameMang.bulletsShot();
			Destroy (bullet, 6f);
			bullet.GetComponent<Rigidbody> ().velocity = transform.forward * fireSpeed;
		}
	}
	public bool CanSeeTarget (Rigidbody target) {
		if (target == null)
			return false;
		return !Physics.Linecast (transform.position, target.transform.position, ship);
	}
	public Rigidbody NextTarget () {
		if (targetRigidBodies.Count == 0)
			return null;
		for (int i = 0; i < targetRigidBodies.Count; i++) {
			if (targetRigidBodies [i] == null) {
				targetRigidBodies.RemoveAt (i);
				i--;
				continue;
			}
			Rigidbody rigidBod = targetRigidBodies[i];
			if (CanSeeTarget (rigidBod))
				return rigidBod;
		}
		return null;
	}
	void OnTriggerEnter (Collider coll) {
		if (coll.CompareTag ("Astroid")) {
			targetRigidBodies.Add (coll.GetComponent<Rigidbody> ());
		}
	}
	void OnTriggerExit (Collider coll) {
		if (coll.CompareTag ("Astroid")) {
			targetRigidBodies.Remove (coll.GetComponent<Rigidbody> ());
		}
	}
}
