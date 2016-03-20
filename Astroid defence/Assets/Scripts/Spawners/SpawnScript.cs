using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	public GameObject toSpawn;
	public int numberToSpawn;

	public float speed;

	
	public float minAsteroidScale = 2;
	public float maxAsteroidScale = 1;

	public float minRotation = 1;
	public float maxRotation = 2;

	public float spawnDistance;
	public Vector2 spawnExtents;

	[HideInInspector] public int checkerIndex;
	[HideInInspector] public Rigidbody [] astroids;

	void Start () {
		astroids = new Rigidbody[numberToSpawn];
		SpawnAstroids ();
	}
	void Update () {
		if (++checkerIndex >= astroids.Length) {
			checkerIndex = 0;
		}
		if (!Inside (astroids [checkerIndex].position)) {
			Vector3 scale = transform.localScale;
			Vector3 moveToPosition = transform.position - transform.forward * scale.z;

			moveToPosition += transform.up * Random.Range (-scale.y, scale.y);
			moveToPosition += transform.right * Random.Range (-scale.x, scale.x);

			astroids [checkerIndex].position = moveToPosition;
		}
	}
	void SpawnAstroids () {
		Vector3 scale 	= transform.localScale;

		for (int i = 0; i < numberToSpawn; i++) {
			Vector3 spawnPosition = transform.position + transform.right 	* Random.Range (-scale.x,scale.x) +
			                                             transform.up	  	* Random.Range (-scale.y,scale.y) +
			                                             transform.forward  * Random.Range (-scale.z,scale.z);
			GameObject objectSpawned = Instantiate (toSpawn, spawnPosition, Quaternion.identity) as GameObject;
			Rigidbody rigidBody = objectSpawned.GetComponent<Rigidbody> ();
			rigidBody.velocity = transform.forward * speed;
			rigidBody.angularVelocity = Random.onUnitSphere * Random.Range (minRotation, maxRotation);
			objectSpawned.transform.localScale *= Random.Range (minAsteroidScale, maxAsteroidScale);
			
			astroids [i] = rigidBody;
		}
	}
	bool Inside (Vector3 pos) {
		pos -= transform.position;
		
		Vector3 scale 	= transform.localScale;

		if (	   Mathf.Abs (Vector3.Dot (pos, transform.forward)) > scale.z) {
			return false;
		} else if (Mathf.Abs (Vector3.Dot (pos, transform.right)) 	> scale.x) {
			return false;
		} else if (Mathf.Abs (Vector3.Dot (pos, transform.up))	  	> scale.y) { 
			return false;
		}
		return true;
	}
	void OnDrawGizmos () {
		Vector3 scale 	= transform.localScale;
		Vector3 forward = scale.z * transform.forward;
		Vector3 up 		= scale.y * transform.up;
		Vector3 right 	= scale.x * transform.right;

		Vector3 topLeft1  	 = transform.position + forward + up - right;
		Vector3 bottomRight1 = transform.position + forward - up + right;
		Vector3 min1 	  	 = transform.position + forward - up - right;
		Vector3 max1 	 	 = transform.position + forward + up + right;

		Vector3 topLeft2  	 = transform.position - forward + up - right;
		Vector3 bottomRight2 = transform.position - forward - up + right;
		Vector3 min2 	  	 = transform.position - forward - up - right;
		Vector3 max2 	  	 = transform.position - forward + up + right;

		Gizmos.DrawLine (topLeft1	 , max1			);
		Gizmos.DrawLine (max1		 , bottomRight1	);
		Gizmos.DrawLine (bottomRight1, min1			);
		Gizmos.DrawLine (min1		 , topLeft1		);
		
		Gizmos.DrawLine (topLeft2	 , max2			);
		Gizmos.DrawLine (max2		 , bottomRight2	);
		Gizmos.DrawLine (bottomRight2, min2			);
		Gizmos.DrawLine (min2		 , topLeft2		);
		
		Gizmos.DrawLine (topLeft1	 , topLeft2		);
		Gizmos.DrawLine (max1		 , max2			);
		Gizmos.DrawLine (bottomRight1, bottomRight2	);
		Gizmos.DrawLine (min1		 , min2			);

		Vector3 centerOfSpawnArea = transform.position - transform.forward.normalized * speed;
	}
}
