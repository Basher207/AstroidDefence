using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponStand : MonoBehaviour {

	#region static
	public const string displayMaterialPath = "Materials/Guns/Visualization";

	public static Material visualizationMaterial;
	
	public static void Intialize () {
		if (visualizationMaterial == null) {
			visualizationMaterial = Resources.Load <Material> (displayMaterialPath);
		}
	}
	#endregion

	public MeshFilter previewFilter;

	void Awake () {
		//previewFilter = transform.GetChild (0).GetComponent <MeshFilter> ();
		//previewFilter.mesh = new Mesh ();
	}
	public void OnPreview (bool on) {
		//Math.CombineMeshes (GunPlacment.weaponToPlace, previewFilter.mesh);
		//Debug.Log (previewFilter.mesh.vertexCount);
	}
	public void OnBuy () {

	}
}
