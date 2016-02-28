using UnityEngine;
using System.Collections;

public class GameMang : MonoBehaviour {

	public static GameObject iron;
	public static GameObject gold;
	static GameObject possibleDrop;

	public static GameObject drop()
	{
		float r = Random.Range (0.0f, 100.0f);
		if (r < 50.0) {
			return iron;
		} 
		else if (r > 80.0) 
		{
			return gold;
		} 
		else
			return null;
	}

	public static void bulletsShot () {
		instance._bulletShot++;
	}
	public static void astroidDestroyed () {
		instance._astroidDestroyed++;
		//drop iron and/or gold
		possibleDrop = drop();
		if (possibleDrop != null) 
		{
			Instantiate (possibleDrop, new Vector3 (0, 0, 0), Quaternion.identity);//bash will kill me for messing with his code ;D
		}

	}
	public static GameMang instance;

	public int _bulletShot 		 = 0;
	public int _astroidDestroyed = 0;

	void Awake () {
		instance = this;
	}
}
