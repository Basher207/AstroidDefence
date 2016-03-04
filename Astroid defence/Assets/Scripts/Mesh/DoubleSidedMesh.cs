#if UNITY_EDITOR 
using UnityEngine;
using UnityEditor;
using System.Collections;
[ExecuteInEditMode]
public class DoubleSidedMesh : MonoBehaviour {

	public bool updateMesh;
	void Update () {
		if (updateMesh) {
			updateMesh = false;
			MeshFilter meshFilter = GetComponent<MeshFilter> ();
			if (meshFilter == null) {
				Destroy (this);
				return;
			}
			Mesh mesh = Instantiate (meshFilter.sharedMesh) as Mesh;
			int [] tris = mesh.triangles;
			int [] newTris = new int[tris.Length * 2];
			int maxNewTrisIndex = newTris.Length - 1;
			for (int i = 0; i < tris.Length; i++) {
				newTris [i] = tris [i];
			}
			for (int i = tris.Length - 1; i >= 0; i--) {
				newTris [maxNewTrisIndex - i] = tris [i];
			}
			mesh.triangles = newTris;
			mesh.name = "Double sided " + mesh.name;
			AssetDatabase.CreateAsset (mesh, "Assets/" + mesh.name + ".asset");
			meshFilter.sharedMesh = mesh;
		}
	}
}
#endif
