using UnityEngine;
using System.Collections.Generic;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	public bool IsDebugMode;

	[SerializeField]
	private TetrisLogic Logic;

	[SerializeField]
	private InputBase UserInput;


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
				this.Logic.FixMino ();
			}
		};
		this.UserInput.HardDropKeyPressed += (sender, e) => {
			while (this.Logic.CanMove (TetriminoOperation.MoveDown)) {
				this.Logic.Move (TetriminoOperation.MoveDown, 1);
			}
			this.Logic.FixMino ();
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
	}



}