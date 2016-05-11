using UnityEngine;
using System.Collections;


/// <summary>
/// テトリスフィールドのセル１つ１つを表すクラス
/// </summary>
public class Cell
{
	public enum Content
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
		Wall,
		/// <summary>
		/// 天井
		/// </summary>
		Ceil,
		/// <summary>
		/// このEnumの要素数
		/// </summary>
		Count
	}

	/// <summary>
	/// このセルの内容物
	/// </summary>
	public Content Contents{ get; private set; }

	/// <summary>
	/// コンストラクタ
	/// </summary>
	/// <param name="c">内容物</param>
	public Cell (Content c)
	{
		this.Contents = c;
	}

	/// <summary>
	/// 壁かどうか
	/// </summary>
	public bool IsWall{ get { return this.Contents == Content.Wall; } }


	/// <summary>
	/// 天井かどうか
	/// </summary>
	public bool IsCeil{ get { return this.Contents == Content.Ceil; } }

}
