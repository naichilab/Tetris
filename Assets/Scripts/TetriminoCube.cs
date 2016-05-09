using UnityEngine;
using System.Collections;

public class TetriminoCube : MonoBehaviour
{
	public Vector2 Point;

	private Tetrimino Parent;

	void Awake ()
	{
	}

	public void Move ()
	{
		if (this.Parent == null) {
			this.Parent = GetComponentInParent<Tetrimino> ();
		}

		this.transform.position = this.Parent.AbsoluteCenterLocation + this.Point;
	}

}
