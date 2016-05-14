using UnityEngine;
using System.Collections;

/// <summary>
/// XY座標を扱うクラス
/// </summary>
public struct Point
{
	/// <summary>
	/// X座標
	/// </summary>
	public int X;
	/// <summary>
	/// Y座標
	/// </summary>
	public int Y;

	/// <summary>
	/// コンストラクタ
	/// </summary>
	public Point (int x, int y)
	{
		this.X = x;
		this.Y = y;
	}


	/// <summary>
	/// 平行移動
	/// </summary>
	/// <param name="offset">移動量</param>
	public void Move (Point offset)
	{
		this.X += offset.X;
		this.Y += offset.Y;
	}

	/// <summary>
	/// 原点(0,0)を中心として回転
	/// </summary>
	/// <param name="dir">回転方向</param>
	public void Rotate (RotateDirection dir)
	{
		var temp = this;
		
		switch (dir) {
		case  RotateDirection.Clockwise:
			this.X = temp.Y;
			this.Y = -temp.X;
			break;
		case RotateDirection.CounterClockwise:
			this.X = -temp.Y;
			this.Y = temp.X;
			break;
		}
	}

	/// <summary>
	/// 回転
	/// </summary>
	/// <param name="dir">回転方向</param>
	/// <param name="center">中心座標</param>
	public void Rotate (RotateDirection dir, Point center)
	{
		this.X -= center.X;
		this.Y -= center.Y;

		Rotate (dir);

		this.X += center.X;
		this.Y += center.Y;
	}



	/// <summary>
	/// Vector2へ変換
	/// </summary>
	public Vector2 ToVector2 {
		get {
			return new Vector2 (this.X, this.Y);
		}
	}


	/// <summary>
	/// 原点
	/// </summary>
	public static Point Zero {
		get {
			return new Point (0, 0);
		}
	}


	#region 演算子を定義

	public static Point operator + (Point p1, Point p2)
	{
		return new Point (p1.X + p2.X, p1.Y + p2.Y);
	}

	public static Point operator - (Point p1, Point p2)
	{
		return new Point (p1.X - p2.X, p1.Y - p2.Y);
	}

	#endregion

}
