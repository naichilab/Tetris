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
		/// ブロックあり
		/// </summary>
		Cube,
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
	/// コンストラクタ
	/// </summary>
	/// <param name="c">内容物</param>
	public Cell (Contents c)
	{
		this.Content = c;
	}

	/// <summary>
	/// Cubeをセット
	/// </summary>
	public void SetCube ()
	{
		if (this.IsWall) {
			throw new UnityException ("Cannot set the cube to the WALL");
		}
		
		this.Content = Contents.Cube;
	}

	/// <summary>
	/// 壁かどうか
	/// </summary>
	public bool IsWall{ get { return this.Content == Contents.Wall; } }


	/// <summary>
	/// 空かどうか
	/// </summary>
	public bool IsEmpty{ get { return this.Content == Contents.Empty; } }
}
