using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
public class TorusGenerator : MonoBehaviour {

	[SerializeField] public bool ClickToUpdateTorus;
	[SerializeField] public bool gizmos;

	[SerializeField] public bool smooth;

	[SerializeField] public int widthVeritcies, heightVeritcies;
	[SerializeField] public float extents, radius;

	public int markIndex;
	MeshFilter meshFilter {
		get {
			return GetComponent<MeshFilter> ();
		}
	}
	MeshCollider meshCollider {
		get {
			return GetComponent<MeshCollider> ();
		}
	}
	public Vector3 [] TorusMesh (int widthVerticies, int heightVerticies, float extents, float radius) {
		Vector3 startPosition = new Vector3 ( 0, 0, radius);
		Quaternion anglePerWidth = Quaternion.AngleAxis (360f / widthVerticies, Vector3.up);

		List<Vector3> verts = new List<Vector3> ();

		for (int i = 0; i < widthVerticies; i++) {
			Vector3 heightOffset = new Vector3 (0, extents);
			Vector3 rotationHeightAngle = Quaternion.AngleAxis (90, Vector3.up) * startPosition;
			Quaternion anglePerHeight 	= Quaternion.AngleAxis (360f / heightVeritcies, rotationHeightAngle);
			for (int j = 0; j < heightVeritcies; j++) {
				verts.Add (startPosition + heightOffset);
				heightOffset = anglePerHeight * heightOffset;
			}
			startPosition = anglePerWidth * startPosition;
		}
		return verts.ToArray ();
	}
	public int [] TorusTris (int widthVerticies, int heightVeriticies) {
		List<int> tris = new List<int> ();
		
		int offset = 0;
		int x = 0;

		for (; x < widthVeritcies - 1; x++) {
			offset = x * heightVeritcies;
			for (int y = 0; y < heightVeritcies - 1; y++) {
				tris.Add (offset + y);
				tris.Add (offset + y + 1);
				tris.Add (offset + y + heightVeritcies);
				
				tris.Add (offset + y + 1);
				tris.Add (offset + y + heightVeritcies + 1);
				tris.Add (offset + y + heightVeritcies);
			}
			tris.Add (offset + heightVeritcies - 1);
			tris.Add (offset);
			tris.Add (offset + heightVeritcies - 1 + heightVeritcies);

			tris.Add (offset + heightVeritcies - 1 + heightVeritcies);
			tris.Add (offset);
			tris.Add (offset + heightVeritcies);
		}
		offset = x * heightVeritcies;
		for (int y = 0; y < heightVeritcies - 1; y++) {
			tris.Add (offset + y);
			tris.Add (offset + y + 1);
			tris.Add (y);
			
			tris.Add (offset + y + 1);
			tris.Add (y + 1);
			tris.Add (y);
		}
		tris.Add (offset + heightVeritcies - 1);
		tris.Add (offset);
		tris.Add (heightVeritcies - 1);
		
		tris.Add (heightVeritcies - 1 );
		tris.Add (offset);
		tris.Add (0);

		return tris.ToArray ();
	}

	void Update () {
		if (ClickToUpdateTorus) {
			ClickToUpdateTorus = false;

			Mesh newMesh = new Mesh ();
			Vector3 [] verts = TorusMesh (widthVeritcies, heightVeritcies, extents, radius);
			int 	[] tris = TorusTris (widthVeritcies, heightVeritcies);

			if (!smooth) {
				Vector3 [] newVerts = new Vector3[tris.Length];

				for (int i = 0; i < tris.Length; i++) {
					newVerts [i] = verts[tris[i]];
					tris 	 [i] = i;
				}
				verts = newVerts;
			}
			newMesh.vertices = verts;
			newMesh.triangles = tris;
			meshFilter.sharedMesh = newMesh;
			meshFilter.sharedMesh.RecalculateNormals ();
			meshFilter.sharedMesh.RecalculateBounds ();

			if (meshCollider != null) {
				meshCollider.sharedMesh = null;
				meshCollider.sharedMesh = meshFilter.sharedMesh;
			}
		}
	}
	void OnDrawGizmos () {
		if (!gizmos)
			return;
		Vector3 [] torusMesh = TorusMesh (widthVeritcies, heightVeritcies, extents, radius);
		Gizmos.color = Color.white;
		for (int i = 0; i < torusMesh.Length; i++) {
			Gizmos.DrawWireSphere (transform.TransformPoint (torusMesh [i]), 2);
		}
		Gizmos.color = Color.red;
		if (markIndex >= 0 && markIndex < torusMesh.Length)
			Gizmos.DrawSphere (transform.TransformPoint (torusMesh [markIndex]), 4);
	}
}
