﻿using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// テトリミノに対する操作
/// </summary>
public enum TetriminoOperation
{
	/// <summary>
	/// 移動なし
	/// </summary>
	None,
	/// <summary>
	/// 右移動
	/// </summary>
	MoveRight,
	/// <summary>
	/// 左移動
	/// </summary>
	MoveLeft,
	/// <summary>
	/// 下移動
	/// </summary>
	MoveDown,
	/// <summary>
	/// 一番下まで落下
	/// </summary>
	HardDrop,
	/// <summary>
	/// 時計回りに回転
	/// </summary>
	RotateClockwise,
	/// <summary>
	/// 反時計回りに回転
	/// </summary>
	RotateCounterClockwise
}

public class TetriminoOperationEventArgs:EventArgs
{
	public TetriminoOperation Operation{ get; set; }

	public TetriminoOperationEventArgs (TetriminoOperation op)
	{
		this.Operation = op;
	}
}

public delegate void TetriminoOperationEventHandler (object sender, TetriminoOperationEventArgs e);
