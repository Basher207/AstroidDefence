using UnityEngine;
using System.Collections;

public class SpawnBetweenTime : MonoBehaviour {

	[SerializeField] public GameObject objectToSpawn;

	[SerializeField] public float waitBetweenSpawns = 0.5f;
	[SerializeField] public int numberOfSpawns = 200;
	[HideInInspector] public float timeForSpawn;

	void Update () {
		if (numberOfSpawns > 0 && objectToSpawn != null && Time.time > timeForSpawn) {
			numberOfSpawns--;
			(Instantiate (objectToSpawn, transform.position, Quaternion.identity) as GameObject).transform.SetParent (transform);
			timeForSpawn = Time.time + waitBetweenSpawns;
		}
	}
}
