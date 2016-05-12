using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Field
{
	const int FIELD_WIDTH = 10;
	const int FIELD_HEIGHT = 20;

	Cell[,] field = null;

	public void Reset ()
	{
		//左右の壁、床、天井を含めたサイズで初期化
		this.field = new Cell[FIELD_WIDTH + 2, FIELD_HEIGHT + 2];

		for (int col = 0; col < FIELD_WIDTH + 1; col++) {
			for (int row = 0; row < FIELD_HEIGHT + 1; row++) {
				bool leftWall = col == 0;
				bool rightWall = col == FIELD_WIDTH + 1;
				bool floor = row == 0;
				bool ceil = row == FIELD_HEIGHT + 1;

				Cell cell = null;
				if (leftWall || rightWall || floor) {
					cell = new Cell (Cell.Content.Wall);
				} else if (ceil) {
					cell = new Cell (Cell.Content.Ceil);
				} else {
					cell = new Cell (Cell.Content.Empty);
				}
				this.field [col, row] = cell;
			}
		}
	}

	public bool Placeable (ITetrimino mino, MoveAmount moveAmount)
	{	
		var movedAbsolutePoints = mino.GetMovedAbsolutePoints (moveAmount);

		return movedAbsolutePoints.All (p => this.field [p.X, p.Y].IsEmpty);
	}

}
