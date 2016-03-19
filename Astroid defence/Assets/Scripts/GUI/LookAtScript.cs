using UnityEngine;
using System.Collections;

public class LookAtScript : MonoBehaviour {

	void LateUpdate () {
		Vector3 delta = Camera.main.transform.forward;
		transform.rotation = Quaternion.LookRotation (delta.normalized, Camera.main.transform.up);;
	}
}
