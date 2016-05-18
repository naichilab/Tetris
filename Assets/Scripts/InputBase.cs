using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 入力インターフェースみたいなクラス
/// </summary>
public abstract class InputBase:MonoBehaviour
{

	public event EventHandler DownKeyPressed;
	public event EventHandler LeftKeyPressed;
	public event EventHandler RightKeyPressed;
	public event EventHandler HardDropKeyPressed;
	public event EventHandler RotateClockwiseKeyPressed;
	public event EventHandler RotateCounterClockwiseKeyPressed;

	protected void OnBottomKeyPressed ()
	{
		if (this.DownKeyPressed != null)
			this.DownKeyPressed (this, EventArgs.Empty);
	}

	protected void OnLeftKeyPressed ()
	{
		if (this.LeftKeyPressed != null)
			this.LeftKeyPressed (this, EventArgs.Empty);
	}

	protected void OnRightKeyPressed ()
	{
		if (this.RightKeyPressed != null)
			this.RightKeyPressed (this, EventArgs.Empty);
	}

	protected void OnHardDropKeyPressed ()
	{
		if (this.HardDropKeyPressed != null)
			this.HardDropKeyPressed (this, EventArgs.Empty);
	}


	protected void OnRotateClockwiseKeyPressed ()
	{
		if (this.RotateClockwiseKeyPressed != null)
			this.RotateClockwiseKeyPressed (this, EventArgs.Empty);
	}

	protected void OnRotateCounterClockwiseKeyPressed ()
	{
		if (this.RotateCounterClockwiseKeyPressed != null)
			this.RotateCounterClockwiseKeyPressed (this, EventArgs.Empty);
	}

}
