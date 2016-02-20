using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class CameraScript : MonoBehaviour {

	[SerializeField ] public Transform relativeTo;

	[SerializeField] public Vector3 relativePosition;

	[SerializeField] public Vector3 rotationOffset;

	[SerializeField] public float rotateBy = 5;

	void Update () {
		transform.position = relativeTo.TransformPoint (relativePosition);
		Vector3 relativeUp = relativeTo.up;
		relativeUp.z = 0;
		transform.LookAt (relativeTo.position + rotationOffset, relativeUp);
	}
}
