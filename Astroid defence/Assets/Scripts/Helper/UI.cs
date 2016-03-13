using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game.Resource;

public class UI : MonoBehaviour {

	public GameObject ironGO, energyGO, healthGO;//text game objects
	Text ironText, energyText, healthText;
	// Use this for initialization
	void Start () {
		ironText = ironGO.GetComponent<Text>();
		energyText = energyGO.GetComponent<Text> ();
		healthText = healthGO.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		ironText.text = "Iron: " + GameResources.showIron();
		energyText.text = "Energy: " + GameResources.showEnergy ();
		healthText.text = "Health: " + GameResources.showHealth ();
	}
}
