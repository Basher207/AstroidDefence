using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game.Resource;

public class UI : MonoBehaviour {

	public GameObject ironGO, energyGO, healthGO;//text game objects
	Text ironText, energyText, healthText;
	public bool shipDestroyed;
	public Slider healthBar;

	//custom health bar
	//public Vector2 pos = 





	public void OnGUI()
	{
		if (shipDestroyed)
			GUI.Label(new Rect(Screen.width/2-40,Screen.height/2-20,100,20), "GAME OVER");
	}

	public void shipDown()
	{
		shipDestroyed = true;
	}

	// Use this for initialization
	void Start () {
		//GameOverGO.GetComponent<Text> ().enabled = false;
		ironText = ironGO.GetComponent<Text>();
		energyText = energyGO.GetComponent<Text> ();
		healthText = healthGO.GetComponent<Text> ();
		shipDestroyed = false;
	}
	
	// Update is called once per frame
	void Update () {
		//check for ship destroyed
		ironText.text = "Iron: " + GameResources.showIron();
		energyText.text = "Energy: " + GameResources.showEnergy ();
		healthBar.value = GameResources.showHealth ();
		healthText.text = "Health: " + GameResources.showHealth ();
	}
}
