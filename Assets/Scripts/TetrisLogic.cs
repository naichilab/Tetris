using UnityEngine;
using System.Collections;

public class TetrisLogic : MonoBehaviour
{


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



	public bool CanMove (TetriminoOperation op)
	{
		if (this.CurrentMino == null) {
			return false;
		}
		
		switch (op) {
		case TetriminoOperation.MoveLeft:
			return this.Field.Placeable (this.CurrentMino, new MoveAmount (-1, 0));
		case TetriminoOperation.MoveRight:
			return this.Field.Placeable (this.CurrentMino, new MoveAmount (1, 0));
		case TetriminoOperation.MoveDown:
			return this.Field.Placeable (this.CurrentMino, new MoveAmount (0, -1));
		case TetriminoOperation.RotateClockwise:
			return this.Field.Placeable (this.CurrentMino, new MoveAmount (MoveAmount.RotateDir.Clockwise));
		case TetriminoOperation.RotateCounterClockwise:
			return this.Field.Placeable (this.CurrentMino, new MoveAmount (MoveAmount.RotateDir.CounterClockwise));
		}

		return false;
	}

	public bool Placeable (MoveAmount moveAmount)
	{
		return this.Field.Placeable (this.CurrentMino, moveAmount);
	}


	public void Move (TetriminoOperation op)
	{
		if (this.CurrentMino == null) {
			return;
		}

		switch (op) {
		case TetriminoOperation.MoveLeft:
			this.CurrentMino.SetAbsoluteCenterPoint (this.CurrentMino.GetAbsoluteCenterPoint () + new Point (-1, 0));
			break;
		case TetriminoOperation.MoveRight:
			this.CurrentMino.SetAbsoluteCenterPoint (this.CurrentMino.GetAbsoluteCenterPoint () + new Point (1, 0));
			break;
		case TetriminoOperation.MoveDown:
			this.CurrentMino.SetAbsoluteCenterPoint (this.CurrentMino.GetAbsoluteCenterPoint () + new Point (0, -1));
			break;
		case TetriminoOperation.RotateClockwise:
			this.CurrentMino.Move (new MoveAmount (MoveAmount.RotateDir.Clockwise));
			break;
		case TetriminoOperation.RotateCounterClockwise:
			this.CurrentMino.Move (new MoveAmount (MoveAmount.RotateDir.CounterClockwise));
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
