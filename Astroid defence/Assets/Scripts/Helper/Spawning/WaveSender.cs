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
	private WaveSquad 	squad;
	private int 		alreadySpawned;

	private float 		waitTill;

	void Update () {

	}
	void SpawnGameobjects (GameObject spawnObject, int numberOfTimes) {
		for (int i = 0; i < numberOfTimes; i++)
			Instantiate (spawnObject, transform.position, Quaternion.identity);
	}
	void StartWaveSquad (WaveSquad waveSquad) {
		squad = waveSquad;
		timeOfWaveSquadSend = Time.time;
		alreadySpawned = 0;
	}
}
