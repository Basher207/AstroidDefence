using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public static class Math {
	#region Math
	public static Vector3 VectorToIntercept (Rigidbody toIntercept, Vector3 fromPosition, float bulletVelocity) {
		Vector3 toTarget = toIntercept.position - fromPosition;
		
		float a = Vector3.Dot (toIntercept.velocity, toIntercept.velocity) - bulletVelocity * bulletVelocity;
		float b = 2 * Vector3.Dot (toIntercept.velocity, toTarget);
		float c = Vector3.Dot (toTarget, toTarget);
		
		float positiveQuad = PositiveQuadratic (a,b,c);
		float negativeQuad = NegativeQuadratic (a,b,c);
		
		float time = positiveQuad > negativeQuad ? positiveQuad : negativeQuad;
		
		Vector3 targetPos = toIntercept.velocity;
		targetPos *= time;
		targetPos += toIntercept.position;
		
		Vector3 bulletVel = (targetPos - fromPosition).normalized;
		return bulletVel;
	}
	public static Vector3 SetMag (Vector3 vector, float magnitude) {
		return vector.normalized * magnitude;
	}
	public static float PositiveQuadratic (float a, float b, float c)  {
		return (-b + Mathf.Sqrt (b * b - 4 * a * c)) / (2 * a);
	}
	public static float NegativeQuadratic (float a, float b, float c)  {
		return (-b - Mathf.Sqrt (b * b - 4 * a * c)) / (2 * a);
	}
	public static int WrapNumber (int num, int max) {
		num %= max;
		if (num < 0)
			num = max + num;
		return num;
	}
	#endregion

	#region Mesh

	public static void CombineMeshes (GameObject parent, Mesh targetMesh) { 
		List<Vector3> verts = new List<Vector3> ();
		List<int> tris 		= new List<int> ();

		MeshFilter [] filters = parent.GetComponentsInChildren <MeshFilter> (true);
		Debug.Log (parent.transform.childCount);
		for (int i = 0; i < filters.Length; i++) {
			Vector3 [] thisVerts = filters[i].sharedMesh.vertices;
			int     [] thisTris  = filters[i].sharedMesh.triangles;

			int trisOffset = verts.Count;

			for (int j = 0; j < thisVerts.Length; j++) {
				Vector3 pointInWorldSpace = filters[i].transform.TransformPoint (thisVerts[j]);
				verts.Add (parent.transform.InverseTransformPoint (pointInWorldSpace));
			}
			for (int j = 0; j < thisTris.Length; j++) {
				tris.Add (thisTris[j] + trisOffset);
			}
		}
		targetMesh.triangles = new int[0];
		targetMesh.vertices  = verts.ToArray ();
		targetMesh.triangles = tris.ToArray ();
	}

	#endregion
}