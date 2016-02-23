using UnityEngine;
using System.Collections;

public class TorusNavHelp : MonoBehaviour {

	[HideInInspector] public MeshFilter meshFilter;
	[SerializeField ] public int meshPos;
	[SerializeField ] public int meshLength;
	void Awake () {
		meshFilter = GetComponent<MeshFilter> ();
	}
	void OnDrawGizmos () {
		Gizmos.DrawWireSphere ((transform.TransformPoint (GetComponent<MeshFilter> ().sharedMesh.vertices [Mathf.Abs (meshPos)])), 8);
		meshLength = GetComponent<MeshFilter> ().sharedMesh.vertexCount;
	}
}
