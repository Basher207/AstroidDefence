using UnityEngine;
using System.Collections;

public class GunPlacment : MonoBehaviour {

	public static GunPlacment instance;

	public static GameObject weaponToPlace {
		get {
			return instance.ToPlace;
		}
	}

	[SerializeField] public KeyCode placmentKey;

	[SerializeField] public LayerMask standLayer;

	[SerializeField] public GameObject[] guns;
	[HideInInspector]public int currentSelectedIndex;

	public GameObject ToPlace {
		get {
			return guns[currentSelectedIndex];
		}
	}

	public Vector3 viewPortPoint = new Vector3 (0.5f,0.7f,0);

	void Awake () {
		instance = this;
		ToggleMouse ();
	}
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ViewportPointToRay (viewPortPoint), out hit, Mathf.Infinity, standLayer)) {
			if (Input.GetKeyDown (placmentKey)) {
				Vector3 placmentPos = TorusNavigator.TriangleIndexToPosition (hit.triangleIndex * 3);

				TorusNavigator.GridVector gridVector = TorusNavigator.TriangleIndexToGridVector (hit.triangleIndex * 3);
				TorusNavigator.Direction direc = TorusNavigator.direction [gridVector.x, gridVector.y];
				if (direc != TorusNavigator.Direction.Target && direc != TorusNavigator.Direction.Blocked) {
					TorusNavigator.direction[gridVector.x,gridVector.y] = TorusNavigator.Direction.Blocked;
					TorusNavigator.RecalculateDirections ();
					Vector3 normal = hit.normal;
					Quaternion lookDirection = Quaternion.LookRotation (TorusNavigator.tangentAtPoint (placmentPos), normal);
					Instantiate (ToPlace, placmentPos + normal, lookDirection);
				}
			}
		}
		string inputString = Input.inputString;
		foreach (char inputChar in inputString) {
			if (inputChar > '0'&& inputChar <= '9') {
				int index = (int) (inputChar - '0' - 1);
				if (index < guns.Length)
					currentSelectedIndex = index;
				return;
			}
		}
	}
	public void ToggleMouse () {
		if (Cursor.lockState == CursorLockMode.Locked)
			Cursor.lockState = CursorLockMode.None;
		else 
			Cursor.lockState = CursorLockMode.Locked;

		Cursor.visible = !Cursor.visible;
	}
}