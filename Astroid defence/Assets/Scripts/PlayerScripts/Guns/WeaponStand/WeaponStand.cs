using UnityEngine;
using System.Collections;

public class WeaponStand : MonoBehaviour {

	[HideInInspector] public GunFire gunFire;

	void Start () {
		gunFire = GetComponentInChildren<GunFire> ();
		SetGunFire (false);
		SetChildrenActive (false);
	}
	public void SetChildrenActive (bool enable) {
		transform.GetChild(0).gameObject.SetActive (enable);
	}
	public void SetGunFire (bool enable) {
		gunFire.enabled = enable;
	}
	public void OnPreview (bool enter) {
		SetChildrenActive (enter);
	}
	public void OnBuy () {
		SetChildrenActive (true);
		SetGunFire (true);
		Destroy (this);
	}
}
