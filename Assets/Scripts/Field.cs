using System.Collections;
using UnityEngine;

public class Field
{
	const int EMPTY = 0;
	const int WALL = -1;
	const int CEIL = -2;
	const int FIELD_WIDTH = 10;
	const int FIELD_HEIGHT = 20;

	int[,] field = null;

	public void Reset ()
	{
		this.field = new int[FIELD_WIDTH + 2, FIELD_HEIGHT + 2];

		for (int col = 0; col < FIELD_WIDTH + 1; col++) {
			for (int row = 0; row < FIELD_HEIGHT + 1; row++) {
				int cell = EMPTY;
				if (col == 0 || col == FIELD_WIDTH + 1 || row == 0) {
					//左右と下の壁
					cell = WALL;
				} else if (row == FIELD_HEIGHT + 1) {
					//天井
					cell = CEIL;
				}
				this.field [col, row] = cell;
			}
		}
	}


	public bool Placeable (Tetrimino mino, Vector2 pos)
	{
		
		
		
		
		return true;
		
	}

}
