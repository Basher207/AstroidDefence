using UnityEngine;
using System.Collections;

public class Pickups : MonoBehaviour {

	public float minDistance;
	public float speed;
	GameObject player;
	public bool magnet;
	public float ironValue, goldValue;


	// Use this for initialization
	void Start () {
		minDistance = 2.0f;
		speed = 20.0f;
		magnet = false;
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log (col.tag);
		if (col.CompareTag ("Player"))
		{
			player = col.gameObject;
			magnet = true;//goes towards player
		}
	}

	void getItem()
	{
		//checks the tag, gets the player something depending on it, and destroys GO
		switch (gameObject.tag) {
		case "Iron":
			//player.getIron(ironValue);
			Debug.Log("Got some iron");
			break;
		case "Gold":
			//player.getGold(goldValue);
			Debug.Log("Got some gold");
			break;
		default:
			break;
		}
			
		Destroy (gameObject);
	}

	void attract()
	{

		//if the player is close enough, move towards them
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
		if (Vector3.Distance (transform.position, player.transform.position) < minDistance)
		{
			Debug.Log ("Close enough");
			getItem ();
		}
			
	}

	// Update is called once per frame
	void Update () {
		if (magnet) {
			attract ();
		}
	}
}
