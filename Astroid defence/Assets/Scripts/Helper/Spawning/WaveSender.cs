using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSender : MonoBehaviour {

	[System.Serializable]
	public struct WaveSquad  {
		public GameObject prefab;
		public int numberOfDrops;
		public int numberPerDrop;
		public float totalTime;
	}
	[System.Serializable]
	public struct Wave {
		public List <WaveSquad> squads;
		public float waitTillNextWave;
	}
	[SerializeField] public List <Wave> waves;

	private float 		timeOfWaveSquadSend;
	private int 		numberOfDropsSpawned;

	private float 		waitTill;

	void Start () {
		StartWave (waves [0]);
	}
	void Update () {
		if (waves.Count == 0) {
			Destroy (this);
			return;
		} else if (waves[0].squads.Count == 0) {
			if (waves.Count == 0) {
				Destroy (this);
				return;
			} else if (waitTill > Time.time) {
				return;
			}
			waves.RemoveAt (0);
			if (waves.Count == 0) {
				Destroy (this);
				return;
			}
			StartWave (waves[0]);
		}
		WaveSquad squad = waves [0].squads [0];

		float normalizedTime = (Time.time - timeOfWaveSquadSend) / squad.totalTime;

		if (normalizedTime > 1f) {
			normalizedTime = 1f;
			waves[0].squads.RemoveAt (0);
			if (waves[0].squads.Count != 0)
				StartWaveSquad (waves[0].squads[0]);
		}
		int numberOfDrops = (int) (squad.numberOfDrops * normalizedTime);

		for (;numberOfDropsSpawned < numberOfDrops; numberOfDropsSpawned++) { 
			SendObjectWave (squad.prefab, squad.numberPerDrop);
		}
	}
	void SendObjectWave (GameObject prefab, int number) {
		Rigidbody [] rigidBodies = new Rigidbody[number];
		for (int i = 0; i < number; i++) {
			rigidBodies [i] = ((GameObject)Instantiate (prefab)).GetComponent<Rigidbody> ();
		}
		CupAI.SendRigidBodies (rigidBodies);
	}
	void SpawnGameobjects (GameObject spawnObject, int numberOfTimes) {
		for (int i = 0; i < numberOfTimes; i++)
			Instantiate (spawnObject, transform.position, Quaternion.identity);
	}
	void StartWaveSquad (WaveSquad waveSquad) {
		timeOfWaveSquadSend = Time.time;
		numberOfDropsSpawned = 0;
	}
	void StartWave (Wave wave) {
		float totalTime = wave.waitTillNextWave + Time.time;
		for (int i = 0; i < wave.squads.Count; i++) {
			totalTime += wave.squads[i].totalTime;
		}
		waitTill = totalTime;
		StartWaveSquad (wave.squads [0]);
	}
}
