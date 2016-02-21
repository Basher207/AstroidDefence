using UnityEngine;
using System.Collections;

namespace Game.Resource {
	interface EnergyGenerator {
		float GenerateEnergy ();
	}
	public class GameResources : MonoBehaviour {
		private static GameResources instance;
		
		#region static 
		public static void GetIron (float amount) {
			instance.iron += amount;
		}
		public static bool UseIron (float amount) {
			if (instance.iron < amount)
				return false;
			instance.iron -= amount;
			return true;
		}
		public static bool UseEnergy (float amount) {
			if (instance.energy < amount) 
				return false;
			instance.energy -= amount;
			return true;
		}
		#endregion
	
		[SerializeField] public float iron = 500;
		[SerializeField] public float energy = 10000;
	
		void Awake () {
			instance = this;
		}
		void FixedUpdate () {
	
		}
	}
}