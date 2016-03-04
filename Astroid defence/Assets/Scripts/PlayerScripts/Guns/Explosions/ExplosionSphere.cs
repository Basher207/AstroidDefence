using UnityEngine;
using System.Collections;

public class ExplosionSphere : MonoBehaviour {

	public float damage = 100f;

	public float maxRadius = 60f;
	public float time 	   = 1f;

	public float explosionForce;
	public LayerMask astroids;
	[HideInInspector] public float timeOfStart;

	void Start () {
		timeOfStart = Time.time;
		Collider [] pushedColliders = Physics.OverlapSphere (transform.position, maxRadius / 2, astroids);
		foreach (Collider hit in pushedColliders) {
			hit.GetComponent<HealthScript> ().TakeDamage (damage);
			hit.GetComponent<Rigidbody> ().AddExplosionForce (explosionForce, transform.position, maxRadius / 2);
		}
	}
	void FixedUpdate () {
		float normalizedTime = (Time.time - timeOfStart) / time;

		if (normalizedTime > 1f) {
			if (normalizedTime >= 2f) {
				Destroy (gameObject);
				return;
			} else {
				normalizedTime = 1f - normalizedTime % 1f; 
			}
		}
		transform.localScale = Vector3.LerpUnclamped (Vector3.zero, Vector3.one * maxRadius, normalizedTime);
	}
}
