using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class TetriminoCube : MonoBehaviour,ITetrimino
{
	/// <summary>
	/// 親となるTetrimino
	/// </summary>
	private Tetrimino Parent;

	/// <summary>
	/// テトリミノの一部かどうか
	/// </summary>
	public bool IsPartOfTetrimino{ get { return this.Parent != null; } }


	public Point Point;



	private Point AbsoluteCenterPoint = Point.Zero;

	public void SetParentTetrimino (Tetrimino parent)
	{
		this.Parent = parent;
	}

	/// <summary>
	/// 親Tetriminoと切り離す
	/// </summary>
	public void Purge ()
	{
		this.Parent = null;
	}

	void Update ()
	{
//		this.transform.position = this.AbsoluteCenterPoint.Vector2;
	}

	/// <summary>
	/// 中心絶対座標をセット
	/// </summary>
	public void SetAbsoluteCenterPoint (Point p)
	{
		this.AbsoluteCenterPoint = p;
		this.transform.position = this.AbsoluteCenterPoint.Vector2;
	}

	/// <summary>
	/// 中心絶対となるCubeの絶対座標
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
		yield return Point.Zero;
	}

	/// <summary>
	/// テトリミノを構成するCubeそれぞれの絶対座標
	/// </summary>
	public IEnumerable<Point> GetAbsolutePoints ()
	{
		yield return this.AbsoluteCenterPoint;
	}

	/// <summary>
	/// 移動後の座標を取得する
	/// </summary>
	public IEnumerable<Point> GetMovedAbsolutePoints (MoveAmount moveAmount)
	{
		yield return this.AbsoluteCenterPoint + moveAmount.Offset;
	}


	/// <summary>
	/// 移動する
	/// </summary>
	/// <param name="moveAmount">移動量</param>
	public void Move (MoveAmount moveAmount)
	{
		//平行移動
		if (!moveAmount.Offset.IsZero) {
			var offset = moveAmount.Offset;
			this.Point.Move (offset.X, offset.Y);
		}

		//回転
		if (moveAmount.Rotate != MoveAmount.RotateDir.None) {
			if (moveAmount.Rotate == MoveAmount.RotateDir.Clockwise) {
				this.Point.RotateClockwise (Point.Zero);
			}
			if (moveAmount.Rotate == MoveAmount.RotateDir.CounterClockwise) {
				this.Point.RotateCounterClockwise (Point.Zero);
			}
		}
	}


	#if UNITY_EDITOR
	[CustomEditor (typeof(TetriminoCube))]
	public class TetriminoCubeEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			TetriminoCube tetriminoCube = target as TetriminoCube;
			EditorGUILayout.LabelField ("表示座標", tetriminoCube.GetAbsolutePoints ().First ().ToString ());
		}
	}
	#endif
}
