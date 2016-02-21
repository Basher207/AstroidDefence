using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Game.Resource;

public class GunFire : MonoBehaviour {

	public static GameObject laserBullet;

	[HideInInspector] public Gun gun;

	public List<Rigidbody> targetRigidBodies;

	public bool fire;
	public float fireSpeed;
	public float offset;

	public float energyPerShot = 4;

	public float coolDown = 0.2f;

	public float rangeIncreasePerSecond = 40f;
	public float rangeDecreasePerEnter = 20f;
	
	[HideInInspector] public float maxRadius;

	[HideInInspector] public SphereCollider sphereColl;

	void Awake () {
		if (laserBullet == null)
			laserBullet = Resources.Load <GameObject> ("Prefabs/Guns/GunBullet");
		InvokeRepeating ("TurnOff", coolDown, coolDown);
		gun = GetComponentInParent <Gun> ();
		sphereColl = GetComponent<SphereCollider> ();
		maxRadius = sphereColl.radius;
	}
	void FixedUpdate () {
		sphereColl.radius = Mathf.MoveTowards (sphereColl.radius, maxRadius, rangeIncreasePerSecond * Time.deltaTime);
		if (fire && targetRigidBodies.Count != 0) {
			if (GameResources.UseEnergy (energyPerShot)) {
				fire = false;
				while (targetRigidBodies.Count != 0 && targetRigidBodies[0] == null)
					targetRigidBodies.RemoveAt (0);
				gun.RotateTo (Quaternion.FromToRotation (Vector3.forward, Math.VectorToIntercept (targetRigidBodies[0], transform.position, fireSpeed)));
				targetRigidBodies.RemoveAt (0);
				GameObject bullet = Instantiate (laserBullet, transform.position + transform.forward * offset, transform.rotation) as GameObject;
				GameMang.bulletsShot();
				Destroy (bullet, 6f);
				bullet.GetComponent<Rigidbody> ().velocity = transform.forward * fireSpeed;
			}
		}
	}
	void OnTriggerEnter (Collider coll) {
		if (coll.CompareTag ("Astroid")) {
			coll.transform.tag = "TargetedAstroid";
			sphereColl.radius -= rangeDecreasePerEnter;
			targetRigidBodies.Add (coll.GetComponent<Rigidbody> ());
		}
	}
	void TurnOff () {
		fire = true;
	}
}
