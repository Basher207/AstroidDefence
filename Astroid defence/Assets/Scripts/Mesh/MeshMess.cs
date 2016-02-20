using UnityEngine;
using System.Collections;
[RequireComponent (typeof (MeshFilter))]
[ExecuteInEditMode]
public class MeshMess : MonoBehaviour {

	Mesh mesh;
	Mesh intialMesh;

	MeshFilter filter;

	public int seed;

	public float fuzzRange;

	void Start () {
		filter = GetComponent<MeshFilter> ();
		intialMesh = filter.mesh;

		mesh = Instantiate (filter.mesh) as Mesh;
		filter.mesh = mesh;
	}
	void Update () {
		Vector3 [] verts = intialMesh.vertices;
		
		Random.seed = seed;

		for (int i = 0; i < verts.Length; i++) {
			verts[i] += verts[i].normalized * verts[i].magnitude * Random.Range(-fuzzRange, fuzzRange);
		}

		mesh.vertices = verts;
	}
}
