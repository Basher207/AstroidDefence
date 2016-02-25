﻿using UnityEngine;
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
			TorusNavigator.direction [x,y] = direction;
			gridVector = new GridVector (x,y);
		}
		public void Update () {
			TorusNavigator.searchers.Remove (this);
			TorusNavigator.CheckUp 		(gridVector.x, gridVector.y);
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
	public static int	  [] tris;
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
		tris 		 = meshFilter.mesh.triangles;
		direction 	 = new Direction[vertexWidth, vertexHeight];
		UpdateDirectionsFrom (0, 0);
	}
	#region Helper functions
	public static Direction TriangleIndexToDirection (int index) {

		GridVector gridVector = TriangleIndexToGridVector (index);
		return direction [gridVector.x, gridVector.y];
	}
	public static GridVector TriangleIndexToGridVector (int index) {
		index /= 6;
		int widthPos 	= index / (torus.heightVeritcies);
		int heightPos 	= index % (torus.heightVeritcies);

		widthPos 	= WrapNumber (widthPos, vertexWidth);
		heightPos 	= WrapNumber (heightPos, vertexHeight);

		return new GridVector (widthPos, heightPos);
	}
	public static Vector3 GridVectorToPosition (GridVector gridVector) {
		Vector3 firstPos  = verts [gridVector.x * (torus.heightVeritcies * 6) + gridVector.y * 6 + 1];
		Vector3 secondPos = verts [gridVector.x * (torus.heightVeritcies * 6) + gridVector.y * 6 + 2];
		Vector3 delta = secondPos - firstPos;
		
		return instance.transform.TransformPoint (firstPos + delta / 2);
	}
	public static Vector3 TriangleIndexToPosition (int index) {
		index = Mathf.Abs (index % verts.Length);
		int widthPos = index / (torus.heightVeritcies * 6);
		index -= index % 6;
		int heightPos = (index % (torus.heightVeritcies * 6));

		Vector3 firstPos  = verts [widthPos * (torus.heightVeritcies * 6) + heightPos + 1];
		Vector3 secondPos = verts [widthPos * (torus.heightVeritcies * 6) + heightPos + 2];
		Vector3 delta = secondPos - firstPos;

		return instance.transform.TransformPoint (firstPos + delta / 2);
	}
	public static Ray TriangleIndexToRay (int index) {
		index = WrapNumber (index, verts.Length);

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
	public static void UpdateDirectionsFrom (int x, int y) {
		ClearDirections ();
		if (direction [x, y] == Direction.Blocked) {
			Debug.LogWarning ("Update position Blocked");
			return;
		}
		direction [x,y] = Direction.Target;
		CheckUp 	(x, y);
		CheckDown 	(x, y);
		CheckRight 	(x, y);
		CheckLeft 	(x, y);

		int i = searchers.Count - 1;
		while (i >= 0) {
			for (; i >= 0; i--)
				searchers[i].Update ();
			i = searchers.Count - 1;
		}
		SetUvsToDirections ();
	}
	public static void SetUvsToDirections () {
		Vector2 [] uvs = new Vector2[verts.Length];
		for (int i = 0; i < uvs.Length - 5; i+= 6) {
			GridVector gridVec = TriangleIndexToGridVector (i);

			Direction direc = OppositeDirection (TriangleIndexToDirection (i));// direction [gridVec.x, gridVec.y];
			
			Vector2 topLeft, topRight, botLeft, botRight;
			if (direc == Direction.Blocked || direc == Direction.Target || direc == Direction.UnChecked) {
				topLeft  = Vector2.zero;
				topRight = Vector2.zero;
				botLeft  = Vector2.zero;
				botRight = Vector2.zero;
			} else if (direc == Direction.Right) {
				topLeft  = new Vector2 (0.5f, 1.0f);
				topRight = new Vector2 (1.0f, 1.0f);
				botLeft  = new Vector2 (0.5f, 0.5f);
				botRight = new Vector2 (1.0f, 0.5f);
			} else if (direc == Direction.Left) {
				topLeft  = new Vector2 (0.0f, 1.0f);
				topRight = new Vector2 (0.5f, 1.0f);
				botLeft  = new Vector2 (0.0f, 0.5f);
				botRight = new Vector2 (0.5f, 0.5f);
			} else if (direc == Direction.Down) {
				topLeft  = new Vector2 (0.0f, 0.5f);
				topRight = new Vector2 (0.5f, 0.5f);
				botLeft  = new Vector2 (0.0f, 0.0f);
				botRight = new Vector2 (0.5f, 0.0f);
			} else {
				topLeft  = new Vector2 (0.5f, 0.5f);
				topRight = new Vector2 (1.0f, 0.5f);
				botLeft  = new Vector2 (0.5f, 0.0f);
				botRight = new Vector2 (1.0f, 0.0f);
			}
			uvs[i + 0] = topRight;
			uvs[i + 1] = botRight;
			uvs[i + 2] = topLeft;
			uvs[i + 3] = botRight;
			uvs[i + 4] = botLeft;
			uvs[i + 5] = topLeft;
		}
		meshFilter.sharedMesh.uv = uvs;
	}
	static void CheckUp (int x, int y) {
		y++;
		y = WrapNumber (y, direction.GetLength (1));
		if (direction [x, y] == Direction.UnChecked) {
			searchers.Add (new Searcher (Direction.Down, x, y));
		}
	}
	static void CheckDown (int x, int y) {
		y--;
		y = WrapNumber (y, direction.GetLength (1));
		if (direction [x, y] == Direction.UnChecked) {
			searchers.Add (new Searcher (Direction.Up, x, y));
		}
	}
	static void CheckLeft (int x, int y) {
		x--;
		x = WrapNumber (x, direction.GetLength (0));
		if (direction [x, y] == Direction.UnChecked) {
			searchers.Add (new Searcher (Direction.Right, x, y));
		}
	}
	static void CheckRight (int x, int y) {
		x++;
		x = WrapNumber (x, direction.GetLength (0));
		if (direction [x, y] == Direction.UnChecked) {
			searchers.Add (new Searcher (Direction.Left, x, y));
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
	public static int WrapNumber (int num, int max) {
		num %= max;
		if (num < 0)
			num = max + num;
		return num;
	}
	[SerializeField] public int x, y;
	[SerializeField] public int index;
	[SerializeField] public Direction d;
	void OnDrawGizmos () {
		if (!Application.isPlaying)
			return;
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.TransformPoint (verts[index]), 4);
		GridVector gridV = TriangleIndexToGridVector (index);
		x = gridV.x;
		y = gridV.y;
		d = direction [x,y];
	}
}