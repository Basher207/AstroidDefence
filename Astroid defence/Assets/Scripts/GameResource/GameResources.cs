using UnityEngine;
using System.Collections;

namespace Game.Resource {
	interface EnergyGenerator {
		float GenerateEnergy ();
	}
	public class GameResources : MonoBehaviour {
		private static GameResources instance;

		//public static GameObject UIGO;



		#region static 
		public static float showIron()//returns iron value
		{
			return instance.iron;
		}
		public static void GetIron (float amount) {
			instance.iron += amount;
		}
		public static bool UseIron (float amount) {
			if (instance.iron < amount)
				return false;
			instance.iron -= amount;
			return true;
		}

		public static float showEnergy()
		{
			return instance.energy;
		}
		public static bool UseEnergy (float amount) {
			if (instance.energy < amount) 
				return false;
			instance.energy -= amount;
			return true;
		}

		public static float showHealth()
		{
			return instance.health;
		}
		public static void damageShip(float amount)
		{
			instance.health -= amount;
			if (instance.health <= 0) {
				//ship be dead, game be lost
				instance.health=0;
				GameObject.Find("BrianCanvas").GetComponent<UI>().shipDown();// I know it's not perfect, fix it later
			}
		}


		#endregion
	
		[SerializeField] public float iron = 500;
		[SerializeField] public float energy = 10000;
		[SerializeField] public float health = 100;//big ship's life
	
		void Awake () {
			instance = this;
		}
		void FixedUpdate () {
	
		}
	}
}