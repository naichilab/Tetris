using UnityEngine;
using System.Collections;

public struct Point
{
	public int X;
	public int Y;

	public Point (int x, int y)
	{
		this.X = x;
		this.Y = y;
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
