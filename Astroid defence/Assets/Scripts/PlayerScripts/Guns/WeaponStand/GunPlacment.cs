﻿using UnityEngine;
using System.Collections;

public class GunPlacment : MonoBehaviour {

	public static GunPlacment instance;

	public static GameObject weaponToPlace {
		get {
			return instance.ToPlace;
		}
	}

	public KeyCode placmentKey;
	public LayerMask standLayer;
	[SerializeField] public GameObject ToPlace;

	public Vector3 viewPortPoint = new Vector3 (0.5f,0.7f,0);

	void Awake () {
		instance = this;
	}
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ViewportPointToRay (viewPortPoint), out hit, Mathf.Infinity, standLayer)) {
			if (Input.GetKeyDown (placmentKey)) {
				Vector3 placmentPos = TorusNavigator.TriangleIndexToPosition (hit.triangleIndex * 3);
				Vector3 normal = hit.normal;
				Quaternion lookDirection = Quaternion.LookRotation (TorusNavigator.tangentAtPoint (placmentPos), normal);
				Instantiate (ToPlace, placmentPos + normal, lookDirection);
			}
		}
	}
}