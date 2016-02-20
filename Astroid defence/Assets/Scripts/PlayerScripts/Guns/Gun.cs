using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public Transform target;
	public float maxRotationChange;

	public void RotateTo (Quaternion RotateTo) {
		if (target != null) {
			//Quaternion idealRotation = Quaternion.LookRotation (target.position - transform.position, transform.parent.forward);
			//transform.rotation = Quaternion.Slerp (transform.rotation, idealRotation, maxRotationChange);
			transform.rotation = RotateTo;
		}
	}
}
