using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TorusNavigator : MonoBehaviour {
	#region DataTypes
	public enum Direction {
		UnChecked,
		Target,
		Blocked,
		Up,
		Down,
		Left,
		Right,
	}
	public static Direction OppositeDirection (Direction direction) {
		switch (direction) {
			case Direction.Right: return Direction.Left;
			case Direction.Left	: return Direction.Right;
			case Direction.Up	: return Direction.Down;
			case Direction.Down	: return Direction.Up;
		}
		return direction;
	}
	public struct GridVector {
		public int x, y;
		public GridVector (int x, int y) {
			this.x = x;
			this.y = y;
		}
	}
	public static List <Searcher> searchers;
	public struct Searcher {
		GridVector gridVector;
		public Searcher (Direction direction, int x, int y) {
			TorusNavigator.direction [x,y] = OppositeDirection (direction);
			gridVector = new GridVector (x,y);
		}
		void Update () {
			TorusNavigator.CheckUp 	(gridVector.x, gridVector.y);
			TorusNavigator.CheckDown 	(gridVector.x, gridVector.y);
			TorusNavigator.CheckRight 	(gridVector.x, gridVector.y);
			TorusNavigator.CheckLeft 	(gridVector.x, gridVector.y);
		}
	}
	public static Direction [,] direction;

	#endregion
	#region fieldAndProperties
	static int vertexWidth, vertexHeight;


	static MeshFilter meshFilter {
		get {
			return instance.GetComponent<MeshFilter> ();
		}
	}
	public static TorusGenerator torus {
		get {
			return instance.GetComponent<TorusGenerator> ();
		}
	}

	#endregion

	public static TorusNavigator instance;
	public static Vector3 [] verts;
	void Awake () {
		instance = this;
		searchers = new List<Searcher> ();
	}
	void Start () {
		if (torus == null) {
			Destroy (this);
			return;
		}
		vertexWidth  = torus.widthVeritcies;
		vertexHeight = torus.heightVeritcies;
		verts 		 = meshFilter.mesh.vertices;
		direction 	 = new Direction[vertexWidth, vertexHeight];
	}
	#region Helper functions
	public static Direction TriangleIndexToDirection (int index) {
		int widthPos = index / vertexWidth;
		int heightPos = index % vertexHeight;
		return direction [widthPos, heightPos];
	}
	public static GridVector TriangleIndexToGridVector (int index) {
		index %= verts.Length;
		int widthPos = index / (torus.heightVeritcies * 6);
		index -= index % 6;
		int heightPos = (index % (torus.heightVeritcies * 6));
		return new GridVector (widthPos, heightPos);
	}
	public static Vector3 TriangleIndexToPosition (int index) {
		index = index % verts.Length;
		if (index < 0)
			index = verts.Length + index;
		int widthPos = index / (torus.heightVeritcies * 6);
		index -= index % 6;
		int heightPos = (index % (torus.heightVeritcies * 6));

		Vector3 firstPos  = verts [widthPos * (torus.heightVeritcies * 6) + heightPos + 1];
		Vector3 secondPos = verts [widthPos * (torus.heightVeritcies * 6) + heightPos + 2];
		Vector3 delta = secondPos - firstPos;

		return instance.transform.TransformPoint (firstPos += delta / 2);
	}
	public static Ray TriangleIndexToRay (int index) {
		index = index % verts.Length;
		if (index < 0)
			index = verts.Length + index;
		int widthPos = index / (torus.heightVeritcies * 6);
		index -= index % 6;
		int heightPos = (index % (torus.heightVeritcies * 6));
		
		Vector3 firstPos  = verts [widthPos * (torus.heightVeritcies * 6) + heightPos + 1];
		Vector3 secondPos = verts [widthPos * (torus.heightVeritcies * 6) + heightPos + 2];
		Vector3 delta = secondPos - firstPos;
		Vector3 pos = instance.transform.TransformPoint (firstPos += delta / 2);
		Ray ray = new Ray (pos, (pos - AttractTo (pos)).normalized);
		return ray;
	}
	public static Vector3 AttractTo (Vector3 fromPosition) {
		float radius = torus.radius;
		fromPosition = instance.transform.InverseTransformPoint (fromPosition);
		fromPosition.y = 0;
		fromPosition = fromPosition.normalized * radius;
		return instance.transform.TransformPoint (fromPosition);
	}
	public static Vector3 tangentAtPoint (Vector3 point) {
		float radius = torus.radius;
		point 	= instance.transform.InverseTransformPoint (point);
		point.y = 0;
		point = Quaternion.AngleAxis (90, Vector3.up) * point;

		Vector3 tangnet = instance.transform.InverseTransformDirection (point).normalized;
		return tangnet;
	}
	#endregion
	void UpdateDirectionsFrom (int x, int y) {
		ClearDirections ();
		//if (
		CheckUp 	(x, y);
		CheckDown 	(x, y);
		CheckRight 	(x, y);
		CheckLeft 	(x, y);
	}
	static void CheckUp (int x, int y) {
		y++;
		y = Mathf.Abs (y % direction.GetLength (0));
		if (direction [x, y] == Direction.UnChecked) {
			searchers.Add (new Searcher (Direction.Up, x, y));
		}
	}
	static void CheckDown (int x, int y) {
		y--;
		y = Mathf.Abs (y % direction.GetLength (0));
		if (direction [x, y] == Direction.UnChecked) {
			searchers.Add (new Searcher (Direction.Down, x, y));
		}
	}
	static void CheckLeft (int x, int y) {
		x--;
		x = Mathf.Abs (x % direction.Length);
		if (direction [x, y] == Direction.UnChecked) {
			searchers.Add (new Searcher (Direction.Left, x, y));
		}
	}
	static void CheckRight (int x, int y) {
		x++;
		x = Mathf.Abs (x % direction.Length);
		if (direction [x, y] == Direction.UnChecked) {
			searchers.Add (new Searcher (Direction.Right, x, y));
		}
	}
	static void ClearDirections () {
		for (int x = 0; x < vertexWidth; x++) {
			for (int y = 0; y < vertexHeight; y++) {
				if (direction[x,y] != Direction.Blocked && direction[x,y] != Direction.Target)
					direction[x,y] = Direction.UnChecked;
			}
		}
	}
	//void OnDrawGizmos () {
	//	Gizmos.color = Color.green;
	//	Gizmos.DrawWireSphere (TriangleIndexToPosition (index), 4);
	//}
}
