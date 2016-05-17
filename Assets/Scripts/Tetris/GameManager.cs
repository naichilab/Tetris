using UnityEngine;
using System.Collections.Generic;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public bool IsDebugMode;


	[SerializeField]
	private TetriminoGenerator Generator;

	[SerializeField]
	private Field Field;

	[SerializeField]
	private TetrisLogic Logic;

	[SerializeField]
	private float Interval = 1.0f;

	[SerializeField]
	private InputBase UserInput;

	/// <summary>
	/// ミノが最後に動いた時間
	/// </summary>
	private float LastUpdated;


	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public void Start ()
	{
		this.Logic.SetTetriminoGenerator (this.Generator);
		this.Logic.SetField (this.Field);

		this.UserInput.LeftKeyPressed += (sender, e) => {
			if (this.Logic.CanMove (TetriminoOperation.MoveLeft)) {
				this.Logic.Move (TetriminoOperation.MoveLeft);
			}
		};
		this.UserInput.RightKeyPressed += (sender, e) => {
			if (this.Logic.CanMove (TetriminoOperation.MoveRight)) {
				this.Logic.Move (TetriminoOperation.MoveRight);
			}

		};
		this.UserInput.DownKeyPressed += (sender, e) => {
			if (this.Logic.CanMove (TetriminoOperation.MoveDown)) {
				this.Logic.Move (TetriminoOperation.MoveDown);
			} else {
				this.FixMino ();
			}
		};
		this.UserInput.HardDropKeyPressed += (sender, e) => {
			while (this.Logic.CanMove (TetriminoOperation.MoveDown)) {
				this.Logic.Move (TetriminoOperation.MoveDown);
			}
			this.FixMino ();
		};
		this.UserInput.RotateClockwiseKeyPressed += (sender, e) => {
			if (this.Logic.CanMove (TetriminoOperation.RotateClockwise)) {
				this.Logic.Move (TetriminoOperation.RotateClockwise);
			}
		};
		this.UserInput.RotateCounterClockwiseKeyPressed += (sender, e) => {
			if (this.Logic.CanMove (TetriminoOperation.RotateCounterClockwise)) {
				this.Logic.Move (TetriminoOperation.RotateCounterClockwise);
			}
		};

		//GameStart
		this.Logic.CreateMino ();
	}

	public void Update ()
	{
		if (!this.Logic.IsGameOver) {
			//自動落下
			if (this.LastUpdated + this.Interval < Time.time) {
				if (this.Logic.CanMove (TetriminoOperation.MoveDown)) {
					this.Logic.Move (TetriminoOperation.MoveDown);
				} else {
					this.FixMino ();
				}
				this.LastUpdated = Time.time;
			}
		}
	}

	private void FixMino ()
	{
		this.Logic.FixMino ();

		this.Logic.ClearLines ();

		if (this.Logic.IsGameOver) {
			Debug.Log ("Game Over!!!!!");
		} else {
			this.Logic.CreateMino ();
		}
	}

}