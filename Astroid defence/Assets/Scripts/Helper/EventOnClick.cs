using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventOnClick : MonoBehaviour {

	public UnityEvent unityEvent;
	public bool DestroyOnCall = true;
	public KeyCode keyCode;
	void Update () {
		if (Input.GetKeyDown (keyCode)) {
			unityEvent.Invoke ();
			if (DestroyOnCall)
				Destroy (this);
		}
	}
}
