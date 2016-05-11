using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITetrimino
{
	/// <summary>
	/// 中心絶対座標をセット
	/// </summary>
	void SetAbsoluteCenterPoint (Point p);

	/// <summary>
	/// 中心絶対となるCubeの絶対座標
	/// </summary>
	Point GetAbsoluteCenterPoint ();

	/// <summary>
	/// テトリミノを構成するCubeそれぞれの中心からの座標リスト
	/// </summary>
	IEnumerable<Point> GetRelationalPoints ();

	/// <summary>
	/// テトリミノを構成するCubeそれぞれの絶対座標
	/// </summary>
	IEnumerable<Point> GetAbsolutePoints ();

	/// <summary>
	/// 平行移動する
	/// </summary>
	void Move (int x, int y);

	/// <summary>
	/// 右回転
	/// </summary>
	void RotateClockwise ();

	/// <summary>
	/// 左回転
	/// </summary>
	void RotateCounterClockwise ();

}
