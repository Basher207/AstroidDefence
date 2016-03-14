using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent (typeof (Rigidbody))]
public class CupAI : MonoBehaviour {

	[SerializeField]  public List <Transform> waypoints;
	[SerializeField]  public int indexForDrop;
	[HideInInspector] public int currentIndex;
	void Update () {
		//if (Vector3.Distance (waypoints [currentIndex], transform.position) < 5f) {
		//
		//}
	}
}
