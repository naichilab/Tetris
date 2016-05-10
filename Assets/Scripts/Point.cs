using UnityEngine;
using System.Collections;

public struct Point
{
	public int X;
	public int Y;

	public Vector2 Vector2 {
		get {
			return new Vector2 (this.X, this.Y);
		}
	}

	public Point (int x, int y)
	{
		this.X = x;
		this.Y = y;
	}



	public Point Offset (int x = 0, int y = 0)
	{
		return this + new Point (x, y);
	}



	public static Point Zero {
		get {
			return new Point (0, 0);
		}
	}

	public static Point operator + (Point p1, Point p2)
	{
		return new Point (p1.X + p2.X, p1.Y + p2.Y);
	}

	public static Point operator - (Point p1, Point p2)
	{
		return new Point (p1.X - p2.X, p1.Y - p2.Y);
	}


}
