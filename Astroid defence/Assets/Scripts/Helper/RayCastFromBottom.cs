using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RayCastFromBottom : MonoBehaviour {

	public LayerMask layerMask = int.MaxValue;
	public Vector3 castVector = new Vector3 (0,-1);
	public Vector3 vectorUp = new Vector3 (1,0);

	public bool ToBottom;
	void Start () {
		if (Application.isPlaying) {
			Destroy (this);
		}
	}
	void Update () {
		if (ToBottom) {
			ToBottom = false;
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.TransformVector (castVector), out hit)) {
				transform.position = hit.point;
				transform.LookAt (transform.position + hit.normal, vectorUp);
			}
		}
		ToBottom = false;
	}
}
