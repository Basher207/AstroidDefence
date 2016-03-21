using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {


	public AudioClip buildSound, sellSound, upgradeSound, shotSound, hvyShotSound;
	public static AudioManager instance;

	void Awake()
	{
		instance = this;
	}

	//methods to call sound, creates sound on specified position
	public void playBuildSound(Vector3 position)
	{
		AudioSource.PlayClipAtPoint (buildSound, position);
	}

	public void playSellSound(Vector3 position)
	{
		AudioSource.PlayClipAtPoint (sellSound, position);
	}
	public void playShotSound(Vector3 position)
	{
		AudioSource.PlayClipAtPoint (shotSound, position, 0.5f);
	}

	public void playHvyShotSound(Vector3 position)
	{
		AudioSource.PlayClipAtPoint (hvyShotSound, position);
	}
	public void playUpgradeSound(Vector3 position)
	{
		AudioSource.PlayClipAtPoint (upgradeSound, position);
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
