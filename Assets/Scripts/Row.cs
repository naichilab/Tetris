using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Row
{
	private readonly int Width;

	public List<Cell> Cells{ get; private set; }

	public Cell this [int colIdx] {
		get {
			return this.Cells [colIdx];
		}
	}

	public Row ()
	{
		this.Cells = new List<Cell> ();
	}


	/// <summary>
	/// 横一列埋まっているか
	/// </summary>
	public bool IsFilled {
		get {
			if (this.Cells.All (c => c.IsWall)) {
				//床
				return false;
			}

			return this.Cells.All (c => !c.IsEmpty);
		}
	}

	public void Clear ()
	{
		this.Cells.ForEach (c => c.Clear (true));
	}

}
