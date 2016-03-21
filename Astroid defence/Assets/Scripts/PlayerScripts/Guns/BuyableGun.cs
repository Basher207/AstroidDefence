using UnityEngine;
using System.Collections;
using Game.Resource;

namespace Game.Guns {
	public class BuyableGun : MonoBehaviour {
	

		public float gunCost;
		public float upgradeCost;

		public float uiDistanceAbove = 20f;
		public Vector3 uiPoint {
			get {
				return transform.position + transform.up * uiDistanceAbove;
			}
		}
		public GameObject upgrade;

		[HideInInspector] public TorusNavigator.GridVector gridVec;

		void Start () {
			if (upgrade != null)
				upgradeCost = upgrade.GetComponent<BuyableGun> ().gunCost;
		}
		public bool Upgrade () {
			if (upgrade == null)
				return false;
			if (GameResources.UseIron (upgradeCost)) {
				AudioManager.instance.playUpgradeSound (transform.position);
				Instantiate (upgrade, transform.position, transform.rotation);
				Destroy (gameObject);
				return true;
			}
			return false;
		}
		public void Sell () {
			GameResources.GetIron (gunCost / 2f);

			TorusNavigator.direction[gridVec.x,gridVec.y] = TorusNavigator.Direction.UnChecked;
			TorusNavigator.RecalculateDirections ();
	
			AudioManager.instance.playSellSound (gameObject.transform.position);
			Destroy (gameObject);
		}
	}
}
