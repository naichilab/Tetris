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

	[SerializeField]
	private Point TetriminoGenerateCenterLocation = new Point (4, 20);

	private TetriminoGenerator Generator;

	private Field field;

	public void SetTetriminoGenerator (TetriminoGenerator gen)
	{
		this.Generator = gen;
	}


	public bool HasCurrentMino {
		get{ return this.CurrentMino != null; }
	}

	void Awake ()
	{
		this.field = new Field ();
		this.field.Reset ();
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
			return this.field.Placeable (this.CurrentMino, this.CurrentMino.GetAbsoluteCenterPoint () + new Point (-1, 0));
		case Direction.Right:
			return this.field.Placeable (this.CurrentMino, this.CurrentMino.GetAbsoluteCenterPoint () + new Point (1, 0));
		case Direction.Bottom:
			return this.field.Placeable (this.CurrentMino, this.CurrentMino.GetAbsoluteCenterPoint () + new Point (0, -1));
		}

		return false;
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


	public void StepDown ()
	{
		this.CurrentMino.SetAbsoluteCenterPoint (this.CurrentMino.GetAbsoluteCenterPoint () + new Point (0, -1));
	}

	public void CreateMino ()
	{
		if (this.CurrentMino != null) {
			Debug.LogError ("Current Tetrimino is exists");
		}

		Debug.Log ("TetriminoGenerateCenterLocation = " + this.TetriminoGenerateCenterLocation.ToString ());

		this.CurrentMino = this.Generator.Generate (this.TetriminoGenerateCenterLocation);

		Debug.Log ("CurrentMino = " + this.CurrentMino.GetAbsoluteCenterPoint ().ToString ());

	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
