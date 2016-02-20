using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public GameObject toSpawn;
	public int numberToSpawn;

	public Vector3 velocityDirection;
	public float speed;

	
	public float minAsteroidScale = 2;
	public float maxAsteroidScale = 1;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Vector3 max = transform.position + transform.localScale;
			Vector3 min = transform.position - transform.localScale;
			for (int i = 0; i < numberToSpawn; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range (min.z, max.z));
				GameObject objectSpawned = Instantiate (toSpawn, spawnPosition, Quaternion.identity) as GameObject;
				objectSpawned.GetComponent<Rigidbody> ().velocity = velocityDirection.normalized * speed;
				objectSpawned.transform.localScale *= Random.Range (minAsteroidScale, maxAsteroidScale);
			}
		}
	}
	void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, transform.localScale * 2);
		Gizmos.DrawLine (transform.position, transform.position + velocityDirection * 50);
	}
}
