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
			return this.Field.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (-1, 0)));
		case TetriminoOperation.MoveRight:
			return this.Field.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (1, 0)));
		case TetriminoOperation.MoveDown:
			return this.Field.Placeable (this.CurrentMino.GetMovedAbsolutePoints (new Point (0, -1)));
		case TetriminoOperation.RotateClockwise:
			return this.Field.Placeable (this.CurrentMino.GetRotatedAbsolutePoints (RotateDirection.Clockwise));
		case TetriminoOperation.RotateCounterClockwise:
			return this.Field.Placeable (this.CurrentMino.GetRotatedAbsolutePoints (RotateDirection.CounterClockwise));
		}

		return false;
	}


	public void Move (TetriminoOperation op)
	{
		if (this.CurrentMino == null) {
			return;
		}

		switch (op) {
		case TetriminoOperation.MoveLeft:
			this.CurrentMino.Move (new Point (-1, 0));
			break;
		case TetriminoOperation.MoveRight:
			this.CurrentMino.Move (new Point (1, 0));
			break;
		case TetriminoOperation.MoveDown:
			this.CurrentMino.Move (new Point (0, -1));
			break;
		case TetriminoOperation.RotateClockwise:
			this.CurrentMino.Rotate (RotateDirection.Clockwise);
			break;
		case TetriminoOperation.RotateCounterClockwise:
			this.CurrentMino.Rotate (RotateDirection.CounterClockwise);
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

	public void ClearLines ()
	{

		this.Field.ClearLines ();
		
	}

	public bool IsGameOver 
	{ get { return this.Field.CeilReached; } }

}
