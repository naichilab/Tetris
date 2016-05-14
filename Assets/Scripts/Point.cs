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

	public bool IsZero{ get { return X == 0 && Y == 0; } }

	public Point (int x, int y)
	{
		this.X = x;
		this.Y = y;
	}


	public void Move (Point offset)
	{
		this.X += offset.X;
		this.Y += offset.Y;
	}

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

	public void Rotate (RotateDirection dir, Point center)
	{
		this.X -= center.X;
		this.Y -= center.Y;

		Rotate (dir);

		this.X += center.X;
		this.Y += center.Y;
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


	public override string ToString ()
	{
		return string.Format ("[Point: X={0}, Y={1}]", this.X, this.Y);
	}

}
