using UnityEngine;
using System.Collections;


/// <summary>
/// フィールドのセル１つ１つを表すクラス
/// </summary>
public class Cell
{

	/// <summary>
	/// セルの内容物
	/// </summary>
	public enum Contents
	{
		/// <summary>
		/// 空っぽ
		/// </summary>
		Empty,
		/// <summary>
		/// 天井
		/// </summary>
		Ceil,
		/// <summary>
		/// 壁
		/// </summary>
		Wall
	}

	/// <summary>
	/// このセルの内容物
	/// </summary>
	public Contents Content{ get; private set; }

	/// <summary>
	/// このセルにあるブロック
	/// </summary>
	/// <value>The cube.</value>
	public TetriminoCube Cube{ get; set; }

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="c">内容物</param>
	public Cell (Contents c)
	{
		this.Content = c;
	}


	public void Clear ()
	{
		if (!this.IsWall) {
			this.Cube = null;
		}
	}

	/// <summary>
	/// 壁かどうか
	/// </summary>
	public bool IsWall{ get { return this.Content == Contents.Wall; } }

	public bool IsCeil { get { return this.Content == Contents.Ceil; } }

	/// <summary>
	/// Cube設置可能か
	/// </summary>
	/// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
	public bool IsEmpty{ get { return !this.IsWall && this.Cube == null; } }

	/// <summary>
	/// Cubeが設置されているか
	/// </summary>
	/// <value><c>true</c> if this instance has cube; otherwise, <c>false</c>.</value>
	public bool HasCube{ get { return this.Cube != null; } }


}
