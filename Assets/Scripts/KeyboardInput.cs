using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class KeyboardInput : InputBase
{

	public void Update ()
	{
		
		bool left = Input.GetKeyDown (KeyCode.LeftArrow);
		bool right = Input.GetKeyDown (KeyCode.RightArrow);
		bool down = Input.GetKeyDown (KeyCode.DownArrow);
		bool up = Input.GetKeyDown (KeyCode.UpArrow);
		bool z = Input.GetKeyDown (KeyCode.Z);
		bool x = Input.GetKeyDown (KeyCode.X);

		if (left && right) {
			//無視
		} else {
			if (left) {
				this.OnKeyPressed (TetriminoOperation.MoveLeft);
			}
			if (right) {
				this.OnKeyPressed (TetriminoOperation.MoveRight);
			}
		}
		if (down) {
			this.OnKeyPressed (TetriminoOperation.MoveDown);
		} else if (up) {
			this.OnKeyPressed (TetriminoOperation.HardDrop);
		}

		if (z && x) {
			//無視
		} else {
			if (z) {
				this.OnKeyPressed (TetriminoOperation.RotateClockwise);
			}
			if (x) {
				this.OnKeyPressed (TetriminoOperation.RotateCounterClockwise);
			}
		}
	}

}
