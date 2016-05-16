using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Row
{
	private readonly int Width;

	public List<Cell> Cells{ get; private set; }

	public Row ()
	{
		this.Cells = new List<Cell> ();
	}

}
