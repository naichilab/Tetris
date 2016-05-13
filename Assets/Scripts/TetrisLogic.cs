using UnityEngine;
using System.Collections;

public class TetrisLogic : MonoBehaviour
{
	public enum Direction
	{
		Right,
		Left,
		Bottom
	}

	public Tetrimino CurrentMino = null;

	private TetriminoGenerator Generator;

	private Field Field;

	public void SetTetriminoGenerator (TetriminoGenerator gen)
	{
		this.Generator = gen;
	}

	public void SetField (Field f)
	{
		this.Field = f;
		f.Reset ();
	}

	public bool HasCurrentMino {
		get{ return this.CurrentMino != null; }
	}


	void ClearField ()
	{

	}

	public bool CanMove (Direction dir)
	{
		if (this.CurrentMino == null) {
			return false;
		}
		
		switch (dir) {
		case Direction.Left:
			return this.Field.Placeable (this.CurrentMino, new MoveAmount (-1, 0));
		case Direction.Right:
			return this.Field.Placeable (this.CurrentMino, new MoveAmount (1, 0));
		case Direction.Bottom:
			return this.Field.Placeable (this.CurrentMino, new MoveAmount (0, -1));
		}

		return false;
	}

	public bool Placeable (MoveAmount moveAmount)
	{
		return this.Field.Placeable (this.CurrentMino, moveAmount);
	}


	public void Move (Direction dir)
	{
		if (this.CurrentMino == null) {
			return;
		}

		switch (dir) {
		case Direction.Left:
			this.CurrentMino.SetAbsoluteCenterPoint (this.CurrentMino.GetAbsoluteCenterPoint () + new Point (-1, 0));
			break;
		case Direction.Right:
			this.CurrentMino.SetAbsoluteCenterPoint (this.CurrentMino.GetAbsoluteCenterPoint () + new Point (1, 0));
			break;
		case Direction.Bottom:
			this.CurrentMino.SetAbsoluteCenterPoint (this.CurrentMino.GetAbsoluteCenterPoint () + new Point (0, -1));
			break;
		}

	}

	public void CreateMino ()
	{
		if (this.CurrentMino != null) {
			Debug.LogError ("Current Tetrimino is exists");
		}

		this.CurrentMino = this.Generator.Generate ();
	}

	public void FixMino ()
	{
		if (this.CurrentMino == null) {
			Debug.LogError ("Missing current tetrimino.");
		}

		this.Field.FixTetrimino (this.CurrentMino);
		this.CurrentMino = null;
	}

}
