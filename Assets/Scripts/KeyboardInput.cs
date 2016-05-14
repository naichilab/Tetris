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
				this.OnLeftKeyPressed ();
			}
			if (right) {
				this.OnRightKeyPressed ();
			}
		}
		if (down) {
			this.OnBottomKeyPressed ();
		} else if (up) {
			this.OnHardDropKeyPressed ();
		}

		if (z && x) {
			//無視
		} else {
			if (z) {
				this.OnRotateCounterClockwiseKeyPressed ();
			}
			if (x) {
				this.OnRotateClockwiseKeyPressed ();
			}
		}
	}

}
