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
		public Gun (GameObject prefab, float cost) {
			this.prefab = prefab;
			this.cost = cost;
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
				new Gun (Resources.Load <GameObject> ("Prefabs/GunPrefabs/MountedGun"), 25f),
				new Gun (Resources.Load <GameObject> ("Prefabs/GunPrefabs/IonCannon" ), 150f)
			};
		}
	}
}