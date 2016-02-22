using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RayCastFromBottom : MonoBehaviour {

	public LayerMask layerMask = int.MaxValue;
	public Vector3 castVector = new Vector3 (0,-1);
	public Vector3 vectorUp = new Vector3 (1,0);
	public Vector3 offset;

	public bool ToBottom;
	void Start () {
		if (Application.isPlaying) {
			Destroy (this);
		}
	}
	void Update () {
		if (ToBottom) {
			ToBottom = false;
			RaycastHit [] hit = Physics.RaycastAll (transform.position, transform.TransformVector (castVector), float.MaxValue, layerMask);
			for (int i = 0; i < hit.Length; i++) {
				if (hit[i].transform != transform) {
					transform.position = hit[i].point + transform.rotation * offset;
					transform.LookAt (transform.position + hit[i].normal, vectorUp);
				}
			}
		}
		ToBottom = false;
	}
}
