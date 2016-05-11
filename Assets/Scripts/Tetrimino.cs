using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tetrimino : MonoBehaviour,ITetrimino
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
		cube.transform.localPosition = p.Vector2;
		cube.transform.localRotation = Quaternion.identity;

		var cubeComponent = cube.GetComponent<TetriminoCube> ();
		cubeComponent.SetParentTetrimino (this);
		cubeComponent.Point = p;

		this.Cubes.Add (cubeComponent);
	}







	public IEnumerable<Point> GetPoints ()
	{
		return this.Cubes.Select (c => c.Point);
	}


	public void SetAbsoluteCenterPoint (Point p)
	{
		this.AbsoluteCenterPoint = p;
		this.transform.position = this.AbsoluteCenterPoint.Vector2;
		this.Cubes.ForEach (c => c.SetAbsoluteCenterPoint (p + c.Point));
	}

	/// <summary>
	/// テトリミノの中心となるCubeの絶対座標
	/// </summary>
	public Point GetAbsoluteCenterPoint ()
	{
		return this.AbsoluteCenterPoint;
	}

	/// <summary>
	/// テトリミノを構成するCubeそれぞれの中心からの座標リスト
	/// </summary>
	public IEnumerable<Point> GetRelationalPoints ()
	{
		return this.Cubes.Select (c => c.Point);
	}

	/// <summary>
	/// テトリミノを構成するCubeそれぞれの絶対座標
	/// </summary>
	public IEnumerable<Point> GetAbsolutePoints ()
	{
		return this.Cubes.Select (c => this.AbsoluteCenterPoint + c.Point);
	}

	/// <summary>
	/// 平行移動する
	/// </summary>
	public void Move (int x, int y)
	{
		this.AbsoluteCenterPoint.Move (x, y);
		this.Cubes.ForEach (c => c.Move (x, y));
	}

	/// <summary>
	/// 右回転
	/// </summary>
	public void RotateClockwise ()
	{
		this.Cubes.ForEach (c => c.RotateClockwise ());
	}

	/// <summary>
	/// 左回転
	/// </summary>
	public void RotateCounterClockwise ()
	{
		this.Cubes.ForEach (c => c.RotateCounterClockwise ());
	}



	#if UNITY_EDITOR
	[CustomEditor (typeof(Tetrimino))]
	public class TetriminoEditor : Editor           //!< Editorを継承するよ！
	{
		bool folding = false;

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			Tetrimino tetrimino = target as Tetrimino;

			EditorGUILayout.LabelField ("表示座標");
			using (new EditorIndent ()) {
				foreach (var c in tetrimino.GetAbsolutePoints()) {
					EditorGUILayout.LabelField (c.ToString ());
				}
			}
		}
	}
	#endif
}
