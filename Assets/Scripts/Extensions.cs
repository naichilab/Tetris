using UnityEngine;
using System.Collections;

public static class Extensions
{

	public static Vector2 Offset (this Vector2 v, int x, int y)
	{
		return new Vector2 (v.x + x, v.y + y);
	}
}
