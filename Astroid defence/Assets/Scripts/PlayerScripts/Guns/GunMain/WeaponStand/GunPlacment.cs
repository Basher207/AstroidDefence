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
	[SerializeField] public LayerMask standLayer;

	[HideInInspector]public int currentSelectedIndex;
	
	public static GunInstance [,] turrets;

	public static MeshFilter visualizer;

	public Gun ToPlace {
		get {
			return GunManager.GetGun ((GunType)currentSelectedIndex);
		}
	}
	void Awake () {
		instance = this;
		if (visualizer == null) {
			Material mat = Resources.Load <Material> ("Materials/Guns/Visualization");
			visualizer 	 = new GameObject ("visualizer").AddComponent<MeshFilter> ();
			visualizer.gameObject.AddComponent<MeshRenderer> ().sharedMaterial = mat;
		}
		turrets = new GunInstance[TorusNavigator.direction.GetLength (0),TorusNavigator.direction.GetLength (1)];
	}
	void Update () {
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
	public void UpdateVisualizer () {
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, standLayer)) {
			Vector3 placmentPos = TorusNavigator.TriangleIndexToPosition (hit.triangleIndex * 3);
			TorusNavigator.GridVector gridVector = TorusNavigator.TriangleIndexToGridVector (hit.triangleIndex * 3);
			TorusNavigator.Direction direc = TorusNavigator.direction [gridVector.x, gridVector.y];

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
	}
	public void PlaceTurret () {
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, standLayer)) {
			Vector3 placmentPos = TorusNavigator.TriangleIndexToPosition (hit.triangleIndex * 3);
			TorusNavigator.GridVector gridVector = TorusNavigator.TriangleIndexToGridVector (hit.triangleIndex * 3);
			TorusNavigator.Direction direc = TorusNavigator.direction [gridVector.x, gridVector.y];

			if (direc != TorusNavigator.Direction.Target && direc != TorusNavigator.Direction.Blocked && GameResources.UseIron (ToPlace.cost)) {
				TorusNavigator.direction [gridVector.x, gridVector.y] = TorusNavigator.Direction.Blocked;
				TorusNavigator.RecalculateDirections ();
				Vector3 normal = hit.normal;
				Quaternion lookDirection = Quaternion.LookRotation (TorusNavigator.tangentAtPoint (placmentPos), normal);

				turrets [gridVector.x, gridVector.y] = new GunInstance (ToPlace, Instantiate (ToPlace.prefab, placmentPos + normal, lookDirection) as GameObject);
				turrets [gridVector.x, gridVector.y].instance.GetComponent<BuyableGun> ().gridVec = gridVector;
			}
		}
	}
	public void Unvisualize () {
		visualizer.mesh = null;
	}
}