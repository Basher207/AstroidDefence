using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuCanvas : MonoBehaviour {


	public Canvas credits;
	public Button playButton, creditsButton;


	// Use this for initialization
	void Start () {
		credits.enabled = false;
	}

	public void pressCredits()
	{
		//toggles credits on and off
		if (!credits.enabled)
			credits.enabled = true;
		else
			credits.enabled = false;
	}

	public void pressPlay()//starts the game
	{
		SceneManager.LoadScene ("BashScene 2");
	}

}
