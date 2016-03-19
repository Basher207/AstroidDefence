using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game.Guns {
	public class CanvasButtons : MonoBehaviour {
		public static Vector3 farFarAway = new Vector3 (9999999f, 999999f, 999999f);

		public static BuyableGun buyableGunSelected;

		public static void buyableGun (BuyableGun select = null) {
			buyableGunSelected = select;
			if (select != null) {
				staticSellButton.text 	 = 	"Sell: " 	+ (buyableGunSelected.gunCost	 /2f).ToString ();
				staticUpgradeButton.text =  "Upgrade: " + (buyableGunSelected.upgradeCost/2f).ToString ();
			}
		}

		public static Text staticUpgradeButton;
		public static Text staticSellButton;

		public Text upgradeButton;
		public Text sellButton;

		void Awake () {
			staticSellButton 	= sellButton;
			staticUpgradeButton = upgradeButton;
		}

		void LateUpdate () {
			if (buyableGunSelected == null) {
				transform.position = farFarAway;
			} else {
				transform.position = buyableGunSelected.uiPoint;
			}
		}
		public void Sell () {
			buyableGunSelected.Sell ();
		}
		public void Upgrade () {
			buyableGunSelected.Upgrade ();
		}
	}
}
