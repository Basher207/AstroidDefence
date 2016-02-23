using UnityEngine;
using System.Collections;

public class ConstantMovement : MonoBehaviour {

	public Vector3 constantMovement;

	void FixedUpdate () {
		transform.position += constantMovement * Time.deltaTime;
	}
}
