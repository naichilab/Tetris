using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 入力インターフェースみたいなクラス
/// </summary>
public abstract class InputBase:MonoBehaviour
{
	public event TetriminoOperationEventHandler KeyPressed;

	protected void OnKeyPressed (TetriminoOperation op)
	{
		if (this.KeyPressed != null) {
			this.KeyPressed (this, new TetriminoOperationEventArgs (op));
		}		
	}

}
