using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tetrimino : MonoBehaviour
{
	/// <summary>
	/// テトリミノの形状
	/// 参考：http://livedoor.4.blogimg.jp/mkomiz/imgs/f/f/ff82b30d.gif
	/// </summary>
	public enum Shapes
	{
		I = 0,
		O,
		T,
		J,
		L,
		S,
		Z
	}

	/// <summary>
	/// 形状
	/// </summary>
	[SerializeField]
	private Shapes Shape;

	[SerializeField]
	private GameObject CenterCubePrefab;

	[SerializeField]
	private GameObject CubePrefab;



	private List<TetriminoCube> Cubes = new List<TetriminoCube> ();

	private Point AbsoluteCenterPoint = Point.Zero;


	public static Shapes GetRandomShape ()
	{
		var shapeId = UnityEngine.Random.Range (0, Enum.GetValues (typeof(Tetrimino.Shapes)).Length);
		return (Shapes)(shapeId);
	}

	void Awake ()
	{
		//中心キューブ
		var centerCubePrefab = GameManager.Instance.IsDebugMode ? this.CenterCubePrefab : this.CubePrefab;
		this.AddCube (centerCubePrefab, new Point (0, 0));

		switch (this.Shape) {
		case Shapes.I:
			this.AddCube (this.CubePrefab, new Point (-1, 0));
			this.AddCube (this.CubePrefab, new Point (1, 0));
			this.AddCube (this.CubePrefab, new Point (2, 0));
			break;
		case Shapes.O:
			this.AddCube (this.CubePrefab, new Point (1, 0));
			this.AddCube (this.CubePrefab, new Point (0, 1));
			this.AddCube (this.CubePrefab, new Point (1, 1));
			break;
		case Shapes.T:
			this.AddCube (this.CubePrefab, new Point (0, 1));
			this.AddCube (this.CubePrefab, new Point (-1, 0));
			this.AddCube (this.CubePrefab, new Point (1, 0));
			break;
		case Shapes.J:
			this.AddCube (this.CubePrefab, new Point (-1, 1));
			this.AddCube (this.CubePrefab, new Point (-1, 0));
			this.AddCube (this.CubePrefab, new Point (1, 0));
			break;
		case Shapes.L:
			this.AddCube (this.CubePrefab, new Point (1, 1));
			this.AddCube (this.CubePrefab, new Point (-1, 0));
			this.AddCube (this.CubePrefab, new Point (1, 0));
			break;
		case Shapes.S:
			this.AddCube (this.CubePrefab, new Point (-1, 0));
			this.AddCube (this.CubePrefab, new Point (0, 1));
			this.AddCube (this.CubePrefab, new Point (1, 1));
			break;
		case Shapes.Z:
			this.AddCube (this.CubePrefab, new Point (-1, 1));
			this.AddCube (this.CubePrefab, new Point (0, 1));
			this.AddCube (this.CubePrefab, new Point (1, 0));
			break;
		}
	}

	/// <summary>
	/// 立方体を１つ生成する
	/// </summary>
	/// <param name="prefab">立方体のプレハブ</param>
	/// <param name="p">立方体を生成する場所(中心からの相対座標)</param>
	void AddCube (GameObject prefab, Point p)
	{
		var cube = Instantiate (prefab) as GameObject;
		cube.transform.parent = this.transform;
		cube.transform.localPosition = p.ToVector2;
		cube.transform.localRotation = Quaternion.identity;

		var cubeComponent = cube.GetComponent<TetriminoCube> ();
		cubeComponent.DistanceFromTetriminoCenter = p;

		this.Cubes.Add (cubeComponent);
	}

	public void SetAbsoluteCenterPoint (Point p)
	{
		this.AbsoluteCenterPoint = p;
		this.transform.position = this.AbsoluteCenterPoint.ToVector2;
		this.Cubes.ForEach (c => c.transform.position = (p + c.DistanceFromTetriminoCenter).ToVector2);
	}

	/// <summary>
	/// テトリミノの中心となるCubeの絶対座標
	/// </summary>
	public Point GetAbsoluteCenterPoint ()
	{
		return this.AbsoluteCenterPoint;
	}

	/// <summary>
	/// テトリミノを構成するCubeそれぞれの絶対座標
	/// </summary>
	public IEnumerable<Point> GetAbsolutePoints ()
	{
		return this.Cubes.Select (c => this.AbsoluteCenterPoint + c.DistanceFromTetriminoCenter);
	}

	//	/// <summary>
	//	/// 移動後の座標を取得する
	//	/// </summary>
	//	public IEnumerable<Point> GetMovedAbsolutePoints (MoveAmount moveAmount)
	//	{
	//		return this.GetAbsolutePoints ().Select (p => {
	//			p.Move (moveAmount.Offset);
	//			if (moveAmount.Rotate == RotateDirection.Clockwise) {
	//				p.Rotate (RotateDirection.Clockwise, this.AbsoluteCenterPoint);
	//			}
	//			if (moveAmount.Rotate == RotateDirection.CounterClockwise) {
	//				p.Rotate (RotateDirection.CounterClockwise, this.AbsoluteCenterPoint);
	//			}
	//			return p;
	//		});
	//	}


	/// <summary>
	/// 平行移動後の絶対座標を取得
	/// </summary>
	public IEnumerable<Point> GetMovedAbsolutePoints (Point offset)
	{
		return this.GetAbsolutePoints ()
			.Select (p => {
			p.Move (offset);
			return p;
		});
	}

	/// <summary>
	/// 回転後の絶対座標を取得
	/// </summary>
	public IEnumerable<Point> GetRotatedAbsolutePoints (RotateDirection dir)
	{
		return this.GetAbsolutePoints ()
			.Select (p => {
			p.Rotate (dir, this.AbsoluteCenterPoint);
			return p;
		});
	}



	/// <summary>
	/// 回転移動する
	/// </summary>
	public void Rotate (RotateDirection rotationDirection)
	{
		this.Cubes.ForEach (c => c.Rotate (rotationDirection));
	}

	/// <summary>
	/// 移動する
	/// </summary>
	/// <param name="moveAmount">移動量</param>
	public void Move (Point offset)
	{
		this.AbsoluteCenterPoint.Move (offset);
		this.transform.position = this.AbsoluteCenterPoint.ToVector2;
	}


	#if UNITY_EDITOR
	[CustomEditor (typeof(Tetrimino))]
	public class TetriminoEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			Tetrimino tetrimino = target as Tetrimino;

			EditorGUILayout.LabelField ("表示座標");
			EditorGUI.indentLevel++;
			foreach (var c in tetrimino.GetAbsolutePoints()) {
				EditorGUILayout.LabelField (c.ToString ());
			}
			EditorGUI.indentLevel--;

			EditorGUILayout.LabelField ("左移動座標");
			EditorGUI.indentLevel++;
			foreach (var c in tetrimino.GetMovedAbsolutePoints(new Point(-1,0))) {
				EditorGUILayout.LabelField (c.ToString ());
			}
			EditorGUI.indentLevel--;

			EditorGUILayout.LabelField ("右回転座標");
			EditorGUI.indentLevel++;
			foreach (var c in tetrimino.GetRotatedAbsolutePoints(RotateDirection.Clockwise)) {
				EditorGUILayout.LabelField (c.ToString ());
			}
			EditorGUI.indentLevel--;


		}
	}
	#endif
}
