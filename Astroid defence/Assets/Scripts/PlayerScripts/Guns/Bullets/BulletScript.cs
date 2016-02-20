using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	void OnCollisionEnter (Collision coll) {
		if (coll.transform.CompareTag ("Astroid") || coll.transform.CompareTag ("TargetedAstroid")) {

			GameMang.astroidDestroyed ();

			Destroy (coll.transform.gameObject);
			Destroy (gameObject);
		}
	}
}
