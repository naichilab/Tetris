using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
		this.transform.position = (this.AbsoluteCenterPoint + this.Point).Vector2;
	}

	/// <summary>
	/// 中心絶対座標をセット
	/// </summary>
	public void SetAbsoluteCenterPoint (Point p)
	{
		this.AbsoluteCenterPoint = p;
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
	/// 平行移動する
	/// </summary>
	public void Move (int x, int y)
	{
		this.Point.Move (x, y);
	}

	/// <summary>
	/// 右回転
	/// </summary>
	public void RotateClockwise ()
	{
		// Nothing to do.
	}

	/// <summary>
	/// 左回転
	/// </summary>
	public void RotateCounterClockwise ()
	{
		// Nothing to do.
	}

}
