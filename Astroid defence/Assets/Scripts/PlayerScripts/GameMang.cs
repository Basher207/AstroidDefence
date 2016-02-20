using UnityEngine;
using System.Collections;

public class GameMang : MonoBehaviour {

	public static void bulletsShot () {
		instance._bulletShot++;
	}
	public static void astroidDestroyed () {
		instance._astroidDestroyed++;
	}
	public static GameMang instance;

	public int _bulletShot 		 = 0;
	public int _astroidDestroyed = 0;

	void Awake () {
		instance = this;
	}
}
