using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITetrimino
{
	/// <summary>
	/// テトリミノの中心となるCubeの絶対座標
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

}
