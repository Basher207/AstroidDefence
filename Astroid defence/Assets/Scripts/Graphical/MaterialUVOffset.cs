using UnityEngine;
using System.Collections;

public class MaterialUVOffset : MonoBehaviour {

	public Vector2 direction;
	public float magnitude;

	[HideInInspector] public Material mat;

	void Awake () {
		mat = GetComponent<MeshRenderer> ().sharedMaterial;
	}
	void LateUpdate () {
		mat.mainTextureOffset += direction.normalized * magnitude;
	}
}
