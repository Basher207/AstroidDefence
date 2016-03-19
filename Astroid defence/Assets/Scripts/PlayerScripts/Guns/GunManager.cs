using UnityEngine;
using System.Collections;

namespace Game.Guns {
	public enum GunType {
		BasicGun  = 0,
		IonCannon = 1
	}
	public class Gun {
		public GameObject prefab;
		public Mesh previewMesh;
		public float cost;
		public Gun (GameObject prefab) {
			this.prefab = prefab;
			this.cost = prefab.GetComponent<BuyableGun> ().gunCost;
			previewMesh = new Mesh ();
			Math.CombineMeshes (prefab, previewMesh);
		}
	}
	public class GunManager : MonoBehaviour {
		private static Gun [] guns;
	
		public static Gun GetGun (GunType gunType) {
			if (guns == null) 
				Intialize ();
			return guns[(int)gunType];
		}
		public static void Intialize () {
			guns = new Gun[] {
				new Gun (Resources.Load <GameObject> ("Prefabs/GunPrefabs/MountedGun")),
				new Gun (Resources.Load <GameObject> ("Prefabs/GunPrefabs/IonCannon" ))
			};
		}
	}
}