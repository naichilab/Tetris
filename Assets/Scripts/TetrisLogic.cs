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
	private Vector2 TetriminoGenerateCenterLocation = new Vector2 (4, 20);

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
			return this.field.Placeable (this.CurrentMino, this.CurrentMino.AbsoluteCenterLocation.Offset (-1, 0));
			break;
		case Direction.Right:
			return this.field.Placeable (this.CurrentMino, this.CurrentMino.AbsoluteCenterLocation.Offset (1, 0));
			break;
		case Direction.Bottom:
			return this.field.Placeable (this.CurrentMino, this.CurrentMino.AbsoluteCenterLocation.Offset (0, -1));
			break;
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
			this.CurrentMino.AbsoluteCenterLocation.x = this.CurrentMino.AbsoluteCenterLocation.x - 1;
			this.CurrentMino.Move ();
			break;
		case Direction.Right:
			this.CurrentMino.AbsoluteCenterLocation.x = this.CurrentMino.AbsoluteCenterLocation.x + 1;
			this.CurrentMino.Move ();
			break;
		case Direction.Bottom:
			this.CurrentMino.AbsoluteCenterLocation.y = this.CurrentMino.AbsoluteCenterLocation.y - 1;
			this.CurrentMino.Move ();
			break;
		}

	}


	public void StepDown ()
	{
		this.CurrentMino.AbsoluteCenterLocation.y = this.CurrentMino.AbsoluteCenterLocation.y - 1;
		this.CurrentMino.Move ();
	}

	public void CreateMino ()
	{
		if (this.CurrentMino != null) {
			Debug.LogError ("Current Tetrimino is exists");
		}

		this.CurrentMino = this.Generator.Generate (this.TetriminoGenerateCenterLocation);
		this.CurrentMino.AbsoluteCenterLocation = this.TetriminoGenerateCenterLocation;
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
