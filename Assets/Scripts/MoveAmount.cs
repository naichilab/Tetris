using UnityEngine;
using System.Collections;


/// <summary>
/// テトリミノの移動量クラス
/// </summary>
public class MoveAmount
{
	/// <summary>
	/// 回転方向
	/// </summary>
	public enum RotateDir
	{
		None,
		Clockwise,
		CounterClockwise
	}

	/// <summary>
	/// 平行移動量
	/// </summary>
	public Point Offset{ get; set; }

	/// <summary>
	/// 回転方向
	/// </summary>
	public RotateDir Rotate{ get; set; }

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="rotate">Rotate.</param>
	public MoveAmount (RotateDir rotate) : this (0, 0, rotate)
	{

	}

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="x">X軸移動量</param>
	/// <param name="y">Y軸移動量</param>
	/// <param name="rotate">回転方向</param>
	public MoveAmount (int x, int y, RotateDir rotate = RotateDir.None) : this (new Point (x, y), rotate)
	{
		
	}

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="offset">移動量</param>
	/// <param name="rotate">回転方向</param>
	public MoveAmount (Point offset, RotateDir rotate = RotateDir.None)
	{
		this.Offset = offset;
		this.Rotate = rotate;
	}



	/// <summary>
	/// 移動量なし
	/// </summary>
	public static MoveAmount Zero {
		get {
			return new MoveAmount (0, 0, RotateDir.None);
		}
	}
}
