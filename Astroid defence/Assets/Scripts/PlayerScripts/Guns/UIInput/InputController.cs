using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Game.Guns;

public class InputController : MonoBehaviour {

	public static InputController instance;

	GunPlacment gunPlacmentScript;

	Selectable selectable;

	void Awake () {
		instance = this;
		gunPlacmentScript = GetComponent<GunPlacment> ();
	}
	void Update () {
		if (OnCanvasButton ()) {
			gunPlacmentScript.Unvisualize ();
			return;
		}
		gunPlacmentScript.UpdateVisualizer ();

		if (Input.GetAxisRaw ("Fire1") == 1) {
			RaycastHit hit;
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
				Transform superParent = hit.transform;
				while (superParent.parent != null)
					superParent = superParent.parent;
				CanvasButtons.buyableGun (superParent.GetComponentInChildren<BuyableGun> ());
			} else {
				CanvasButtons.buyableGun (null);
			}
		}
		if (Input.GetAxisRaw ("Fire2") == 1) {
			gunPlacmentScript.PlaceTurret ();
		}
	}
	void TrySelection () {

	}
	bool OnCanvasButton () {
		PointerEventData pe = new PointerEventData (EventSystem.current);
		pe.position = Input.mousePosition;
		
		List<RaycastResult> hits = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (pe, hits);

		return hits.Count != 0;
	}
}
