using UnityEngine;
using System.Collections;

public class Pickups : MonoBehaviour {

	public float minDistance;
	GameObject player;


	// Use this for initialization
	void Start () {
		minDistance = 3.0f;
		player = GameObject.Find ("PlayersShip");//leave me alone Bash, idk how to do it better yet
	}

	void checkDistance()
	{
		//if the player is close enough, move towards them
		if (Vector3.Distance (transform.position, player.transform.position) < minDistance) {
		}
			
	}

	// Update is called once per frame
	void Update () {
		
	}
}
