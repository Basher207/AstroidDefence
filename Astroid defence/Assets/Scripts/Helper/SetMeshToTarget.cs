using UnityEngine;
using System.Collections;

public class SetMeshToTarget : MonoBehaviour {

	public GameObject target;

	void Update () {
		if (target != null)
			Math.CombineMeshes (target, GetComponent<MeshFilter> ().mesh);
	}
}
