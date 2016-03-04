using UnityEngine;
using System.Collections;

public class SetTorusNavigationTarget : MonoBehaviour {

	[SerializeField] public LayerMask torusLayerMask;

	void Start () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit, Mathf.Infinity, torusLayerMask)) {
			TorusNavigator.GridVector gridVec = TorusNavigator.TriangleIndexToGridVector (hit.triangleIndex * 3);
			TorusNavigator.searchStartX = gridVec.x;
			TorusNavigator.searchStartY = gridVec.y;
			TorusNavigator.RecalculateDirections ();
		}
	}
}
