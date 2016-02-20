﻿using UnityEngine;
using System.Collections;

public class GunPlacment : MonoBehaviour {

	public KeyCode placmentKey;
	public WeaponStand currentStand;
	public LayerMask standLayer;

	public Vector3 viewPortPoint = new Vector3 (0.5f,0.7f,0);
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ViewportPointToRay (viewPortPoint), out hit, standLayer)) {
			WeaponStand weaponStand = hit.transform.GetComponentInParent <WeaponStand> ();
			if (weaponStand == null) {
				if (currentStand != null)
					currentStand.OnPreview (false);
				currentStand = null;
				return;
			} else if (weaponStand != currentStand) {
				if (currentStand != null)
					currentStand.OnPreview (false);
				weaponStand.OnPreview (true);
				currentStand = weaponStand;
			}
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				weaponStand.OnBuy ();
			}
		}
	}
}