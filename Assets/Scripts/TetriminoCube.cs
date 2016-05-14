using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class TetriminoCube : MonoBehaviour
{
	/// <summary>
	/// 親となるTetrimino
	/// </summary>
	private Tetrimino Parent;

	/// <summary>
	/// 座標（テトリミノ中心座標からの距離）
	/// </summary>
	public Point Point;

	private Point AbsoluteCenterPoint = Point.Zero;

	public void SetParentTetrimino (Tetrimino parent)
	{
		this.Parent = parent;
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
	/// テトリミノを構成するCubeそれぞれの絶対座標
	/// </summary>
	public IEnumerable<Point> GetAbsolutePoints ()
	{
		yield return this.AbsoluteCenterPoint;
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
			EditorGUILayout.LabelField ("座標", tetriminoCube.Point.ToString ());
		}
	}
	#endif
}
