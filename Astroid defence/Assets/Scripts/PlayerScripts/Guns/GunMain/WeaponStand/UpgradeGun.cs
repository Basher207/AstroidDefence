using UnityEngine;
using System.Collections;
using Game.Resource;

public class UpgradeGun : MonoBehaviour {

	public GameObject upgrade;
	public float upgradeCost;

	void Upgrade () {
		if (upgrade == null)
			return;
		if (GameResources.UseIron (upgradeCost)) {
			Instantiate (upgrade, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
