using UnityEngine;
using System.Collections;

public class TorusGridReader : MonoBehaviour {

	public LayerMask torusLayer;

	[HideInInspector] public MeshFilter meshFilter;

	void Awake () {
		meshFilter = GetComponent<MeshFilter> ();
	}
	
	void Update () {
		RaycastHit hit;
	}
}
