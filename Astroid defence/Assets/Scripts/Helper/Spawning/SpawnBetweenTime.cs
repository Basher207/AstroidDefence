using UnityEngine;
using System.Collections;

public class SpawnBetweenTime : MonoBehaviour {

	[SerializeField] public GameObject objectToSpawn;

	[SerializeField] public float waitBetweenSpawns = 0.5f;
	[SerializeField] public int numberOfSpawns 		= 200;
	[SerializeField] public int spawnsPerInstance 	= 1;

	[SerializeField] public KeyCode keyToAdd = KeyCode.M;
	[SerializeField] public int numberOfAdditions = 100;
	[HideInInspector] public float timeForSpawn;

	void Update () {
		if (numberOfSpawns > 0 && objectToSpawn != null && Time.time > timeForSpawn) {
			Rigidbody [] spawns = new Rigidbody[(spawnsPerInstance < numberOfSpawns) ? spawnsPerInstance : numberOfSpawns];

			for (int i = 0; i < spawnsPerInstance && numberOfSpawns > 0; i++) {
				numberOfSpawns--;
				spawns[i] = (Instantiate (objectToSpawn, transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody> ();
				timeForSpawn = Time.time + waitBetweenSpawns;
			}
			CupAI.SendRigidBodies (spawns);
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			numberOfSpawns += numberOfAdditions;
		}
	}
}
