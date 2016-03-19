using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {

	[SerializeField] public float uiHeight;

	[HideInInspector] public int x, y;

	public Vector3 UIDisplayPoint {
		get {
			return transform.position + transform.up * uiHeight;
		}
	}
}
