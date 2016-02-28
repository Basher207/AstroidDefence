using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	[SerializeField] public float damage = 5f;

	void OnCollisionEnter (Collision coll) {
		HealthScript healthScript = coll.transform.GetComponent<HealthScript> ();
		if (healthScript)
			healthScript.TakeDamage (damage);
		Destroy (gameObject);
	}
}
