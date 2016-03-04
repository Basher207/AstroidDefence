using UnityEngine;
using System.Collections;

public class IonBullet : MonoBehaviour {
	
	[SerializeField] public GameObject explosion;
	
	void OnCollisionEnter (Collision coll) {
		Instantiate (explosion, coll.contacts [0].point, Quaternion.identity);
		Destroy (gameObject);
	}
}
