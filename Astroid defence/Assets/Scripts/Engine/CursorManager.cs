using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour {

	public Texture2D cursorTexture;
	public Vector2 offset;

	void Awake () {
		Cursor.SetCursor (cursorTexture, offset, CursorMode.Auto);
	}
	public void ToggleMouse () {
		if (Cursor.lockState == CursorLockMode.Locked)
			Cursor.lockState = CursorLockMode.None;
		else
			Cursor.lockState = CursorLockMode.Locked;
	}
}
