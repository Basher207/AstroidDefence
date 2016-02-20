using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventOnClick : MonoBehaviour {

	public UnityEvent unityEvent;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			unityEvent.Invoke ();
			Destroy (this);
		}
	}
}
