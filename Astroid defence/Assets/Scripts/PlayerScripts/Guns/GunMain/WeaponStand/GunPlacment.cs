using UnityEngine;
using System.Collections;
using Game.Resource;
using Game.Guns;

public class GunPlacment : MonoBehaviour {

	public static GunPlacment instance;

	public static Gun weaponToPlace {
		get {
			return instance.ToPlace;
		}
	}
	public class GunInstance {
		public Gun gun;
		public GameObject instance;
		public GunInstance (Gun gun, GameObject instance) {
			this.gun = gun;
			this.instance = instance;
		}
	}
	[SerializeField] public KeyCode placmentKey = KeyCode.Mouse0;
	[SerializeField] public KeyCode removeKey 	= KeyCode.Mouse1;

	[SerializeField] public LayerMask standLayer;

	[HideInInspector]public int currentSelectedIndex;
	
	public static GunInstance [,] turrets;

	public static MeshFilter visualizer;

	public Gun ToPlace {
		get {
			return GunManager.GetGun ((GunType)currentSelectedIndex);
		}
	}

	public Vector3 viewPortPoint = new Vector3 (0.5f,0.7f,0);
	void Awake () {
		instance = this;
		ToggleMouse ();
		if (visualizer == null) {
			Material mat = Resources.Load <Material> ("Materials/Guns/Visualization");
			visualizer 	 = new GameObject ("visualizer").AddComponent<MeshFilter> ();
			visualizer.gameObject.AddComponent<MeshRenderer> ().sharedMaterial = mat;
		}
		turrets = new GunInstance[TorusNavigator.direction.GetLength (0),TorusNavigator.direction.GetLength (1)];
	}
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ViewportPointToRay (viewPortPoint), out hit, Mathf.Infinity, standLayer)) {
			Vector3 placmentPos = TorusNavigator.TriangleIndexToPosition (hit.triangleIndex * 3);
			TorusNavigator.GridVector gridVector = TorusNavigator.TriangleIndexToGridVector (hit.triangleIndex * 3);
			TorusNavigator.Direction direc = TorusNavigator.direction [gridVector.x, gridVector.y];

			if (Input.GetKey (placmentKey)) {
				if (direc != TorusNavigator.Direction.Target && direc != TorusNavigator.Direction.Blocked && GameResources.UseIron(ToPlace.cost)) {
					TorusNavigator.direction[gridVector.x,gridVector.y] = TorusNavigator.Direction.Blocked;
					TorusNavigator.RecalculateDirections ();
					Vector3 normal = hit.normal;
					Quaternion lookDirection = Quaternion.LookRotation (TorusNavigator.tangentAtPoint (placmentPos), normal);
					turrets[gridVector.x,gridVector.y] = new GunInstance (ToPlace, Instantiate (ToPlace.prefab, placmentPos + normal, lookDirection) as GameObject);
				}
			} else if (Input.GetKeyDown (removeKey)) {
				if (turrets[gridVector.x, gridVector.y] != null) {
					Destroy (turrets[gridVector.x, gridVector.y].instance);
					GameResources.GetIron (turrets[gridVector.x, gridVector.y].gun.cost / 2);
					TorusNavigator.direction[gridVector.x,gridVector.y] = TorusNavigator.Direction.UnChecked;
					TorusNavigator.RecalculateDirections ();
				}
			}
			if (direc != TorusNavigator.Direction.Target && direc != TorusNavigator.Direction.Blocked) {
				Vector3 normal = hit.normal;
				Quaternion lookDirection = Quaternion.LookRotation (TorusNavigator.tangentAtPoint (placmentPos), normal);
				visualizer.mesh = ToPlace.previewMesh;
				visualizer.transform.position = placmentPos + normal;
				visualizer.transform.rotation = lookDirection;
			} else {
				visualizer.mesh = null;
			}
		} else {
			visualizer.mesh = null;
		}
		string inputString = Input.inputString;
		foreach (char inputChar in inputString) {
			if (inputChar > '0'&& inputChar <= '9') {
				int index = (int) (inputChar - '0' - 1);
				if (index < 2f)
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