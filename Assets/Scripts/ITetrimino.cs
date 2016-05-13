using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// テトリミノ共通I/F
/// </summary>
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
	/// 移動後の座標を取得する
	/// </summary>
	IEnumerable<Point> GetMovedAbsolutePoints (MoveAmount moveAmount);

	/// <summary>
	/// 移動する
	/// </summary>
	void Move (MoveAmount moveAmount);
}
